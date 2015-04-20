using System;
using BTZ.Infrastructure;
using BTZ.Common;
using Newtonsoft.Json;
using log4net;
using System.Linq;
using BTZ.Data;
using AutoMapper;

namespace BTZ.Core
{
	/// <summary>
	/// Jonas Ahlf 20.04.2015 00:12:11
	/// </summary>
	public class NewsfeedMessageProcessor : INewsfeedMessageProcessor
	{
		readonly IWallPostRepository _wallPostRepo;
		readonly IImageRepository _imageRepository;
		readonly ILoginManager _loginManager;
		readonly static ILog s_log = LogManager.GetLogger (typeof(NewsfeedMessageProcessor));

		string _requestNullMessage;
		string _requestBadId;
		string _requestBadAuth;


		public NewsfeedMessageProcessor (IWallPostRepository _wallPostRepo,
			IImageRepository _imageRepository, ILoginManager _loginManager)
		{
			this._wallPostRepo = _wallPostRepo;
			this._imageRepository = _imageRepository;
			this._loginManager = _loginManager;
			Initialize ();
		}


		void Initialize()
		{
			#region ErrorMessages
			_requestNullMessage = JsonConvert.SerializeObject (
				new NewsfeedDto()
				{
					Error = new ErrorMessage()
					{
						Type = DtoType.Newsfeed,
						Message = "Request was null"
					}
				}
			);

			_requestBadId = JsonConvert.SerializeObject (
				new NewsfeedDto()
				{
					Error = new ErrorMessage()
					{
						Type = DtoType.Newsfeed,
						Message = "Bad Id"
					}
				}
			);
			_requestBadAuth = JsonConvert.SerializeObject (
				new NewsfeedDto()
				{
					Error = new ErrorMessage()
					{
						Type = DtoType.Newsfeed,
						Message = "Bad Authentification"
					}
				}
			);
			#endregion
			#region Mapping
			Mapper.CreateMap<WallPost,NewsfeedDto>()
				.ForMember(dest => dest.HeaderImage,opt => opt.Ignore())
				.ForMember(dest => dest.Image,opt => opt.Ignore());
			Mapper.CreateMap<NewsfeedDto,WallPost>()
				.ForMember(d => d.Owner, s => s.Ignore());
			#endregion
		}


		#region INewsfeedMessageProcessor implementation

		public string ProcessNewsfeedRequest (string request)
		{
			if (String.IsNullOrEmpty (request)) {
				s_log.Warn ("Newsfeedrequest was null");
				return _requestNullMessage;
			}

			var tempRequest = JsonConvert.DeserializeObject<NewsfeedRequest> (request);

			if (tempRequest == null) {
				s_log.Warn ("Could not parse request");
				return _requestNullMessage;
			}
			return HandleRequest (tempRequest);
		}

		#endregion

		string HandleRequest(NewsfeedRequest request)
		{
			switch (request.RequestType) {
			case RequestType.GetAll:
				return GetAllWallPosts ();
			case RequestType.GetSingle:
				return GetSingle (request);
			case RequestType.PostSingle:
				return ProcessSinglePost (request);
			default:
				return _requestNullMessage;
			}
		}

		#region Get

		string GetAllWallPosts()
		{
			var  ids = _wallPostRepo.GetAllWallPosts ().Select (i => i.Id).ToArray ();

			return JsonConvert.SerializeObject (new ArrayMessage(){Ids = ids});
		}

		string GetSingle(NewsfeedRequest request)
		{
			if (request.Id == 0) {
				s_log.Warn ("Request Single Newsfeed with Bad id");
				return _requestBadId;
			}

			var wallpost = _wallPostRepo.GetAllWallPosts ().FirstOrDefault (i => i.Id.Equals (request.Id));

			if (wallpost == null) {
				s_log.Warn (String.Format ("Could not find wallpost with id {0}", request.Id));
				return _requestBadId;
			}

			return JsonConvert.SerializeObject (ConvertWallPostToDto (wallpost));
		}

		#endregion

		#region Post
		string ProcessSinglePost(NewsfeedRequest request)
		{
			var user = _loginManager.VerifyToken (request.Token);
			if (user == null) {
				return _requestBadAuth;
			}
			if (request.SingleDto == null) {
				return _requestNullMessage;
			}

			WallPost post = Mapper.Map<NewsfeedDto,WallPost> (request.SingleDto);
			post.Owner = user;

			if (request.SingleDto.HeaderImage != null) {
				var headerImagePath = _imageRepository.SaveImage (request.SingleDto.HeaderImage);

				if (headerImagePath.Item1) {
					post.HeaderImage = headerImagePath.Item2;
				}
			}

			if (request.SingleDto.Image != null) {
				var imagePath = _imageRepository.SaveImage (request.SingleDto.Image);
				if (imagePath.Item1) {
					post.Image = imagePath.Item2;
				}
			}

			_wallPostRepo.AddWallPost (post);
			return JsonConvert.SerializeObject (new NewsfeedDto () {
				Title = "OK"
			});
		}
		#endregion

		NewsfeedDto ConvertWallPostToDto(WallPost post)
		{
			NewsfeedDto dto = Mapper.Map<WallPost,NewsfeedDto> (post);

			byte[] imageBytes;
			byte[] headerImageBytes;
			if (!String.IsNullOrEmpty(post.HeaderImage)) {
				headerImageBytes = _imageRepository.GetImageBytes (post.HeaderImage);

				if (headerImageBytes != null) {
					dto.HeaderImage = headerImageBytes;
				}
			}

			if (!String.IsNullOrEmpty(post.Image)) {
				imageBytes = _imageRepository.GetImageBytes (post.Image);

				if (imageBytes != null) {
					dto.Image = imageBytes;
				}
			}

			return dto;
		}

	}
}

