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
			#endregion
			#region Mapping
			Mapper.CreateMap<WallPost,NewsfeedDto>()
				.ForMember(dest => dest.HeaderImage,opt => opt.Ignore())
				.ForMember(dest => dest.Image,opt => opt.Ignore());

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
				_requestNullMessage;
			}
			return HandleRequest (tempRequest);
		}

		#endregion

		string HandleRequest(NewsfeedRequest request)
		{
			switch (request.RequestType) {
			case RequestType.GetAll:
				return GetAllWallPosts ();
				break;
			case RequestType.GetSingle:

				break;
			default:
				return _requestNullMessage;
				break;
			}
		}

		#region Get

		string GetAllWallPosts()
		{
			int[] ids = _wallPostRepo.GetAllWallPosts ().Select (i => i.Id).ToArray ();
			return JsonConvert.SerializeObject (ids);
		}

		string GetSingle(NewsfeedRequest request)
		{
			if (request.Id == null || request.Id == 0) {
				s_log.Warn ("Request Single Newsfeed with Bad id");
				return _requestBadId;
			}

			var wallpost = _wallPostRepo.GetAllWallPosts ().FirstOrDefault (i => i.Id.Equals (request.Id));

			if (wallpost == null) {
				s_log.Warn (String.Format ("Could not find wallpost with id {0}", request.Id));
				return _requestBadId;
			}



		}

		#endregion


		NewsfeedDto ConvertWallPostToDto(WallPost post)
		{
			NewsfeedDto dto = Mapper.Map<WallPost,NewsfeedDto> (post);

			byte[] imageBytes;
			byte[] headerImageBytes;
			if (!String.IsNullOrEmpty(post.HeaderImage)) {
				headerImageBytes = _imageRepository.GetImage (post.HeaderImage);

				if (headerImageBytes != null) {
					dto.HeaderImage = headerImageBytes;
				}
			}

			if (!String.IsNullOrEmpty(post.Image)) {
				imageBytes = _imageRepository.GetImage (post.Image);

				if (imageBytes != null) {
					dto.Image = imageBytes;
				}
			}

			return dto;
		}

	}
}

