﻿using System;
using BTZ.Infrastructure;
using System.Drawing;
using System.IO;
using log4net;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BTZ.DataAccess
{
	public class ImageRepository : IImageRepository
	{
		readonly IFileHelper _fileHelper;
		string BaseImagePath;
		readonly static ILog s_log = LogManager.GetLogger (typeof(ImageRepository));

		public ImageRepository (IFileHelper _fileHelper)
		{
			this._fileHelper = _fileHelper;
			PrepareImageRepository ();
		}

		#region IImageRepository implementation

		public Tuple<bool, string> SaveImage (Image image)
		{
			string filename = CreateNewImagename ();

			if (!_fileHelper.WriteFile (filename, image.ToByteArray ())) {
				s_log.Warn ("Could not save Image");
				return new Tuple<bool, string> (false, String.Empty);
			}

			return new Tuple<bool, string> (true, filename);
		}

		public Tuple<bool, string> SaveImage (byte[] image)
		{
			string filename = CreateNewImagename ();

			Image x = (Bitmap)((new ImageConverter()).ConvertFrom(image));

			var scalled = ResizeImage (x, 848, 480);

			if (!_fileHelper.WriteFile (filename, image)) {
				s_log.Warn ("Could not save Image");
				return new Tuple<bool, string> (false, String.Empty);
			}

			return new Tuple<bool, string> (true, filename);
		}

		public Tuple<bool, string> SaveImage (Bitmap image)
		{
			string filename = CreateNewImagename ();

			if (!_fileHelper.WriteFile (filename, image.ToByteArray ())) {
				s_log.Warn ("Could not save Image");
				return new Tuple<bool, string> (false, String.Empty);
			}
			return new Tuple<bool, string> (true, filename);
		}

		public bool DeleteImage (string path)
		{
			return _fileHelper.DeleteFile (path);
		}

		public Image GetImage (string path)
		{
			var rawFile = _fileHelper.ReadFile (path);

			if (rawFile == null) {
				s_log.Warn (String.Format("Could not read Image, was null {0}",path));
				return null;
			}

			if (rawFile.Length < 1) {
				s_log.Warn (String.Format("Image {0}, is empty",path));
				return null;
			}

			return null;
		}

		byte[] IImageRepository.GetImageBytes (string path)
		{
			var rawFile = _fileHelper.ReadFile (path);

			if (rawFile == null) {
				s_log.Warn (String.Format("Could not read Image, was null {0}",path));
				return null;
			}
			return rawFile;
		}

		Bitmap IImageRepository.GetBitmap (string path)
		{
			var rawFile = _fileHelper.ReadFile (path);

			if (rawFile == null) {
				s_log.Warn (String.Format("Could not read Image, was null {0}",path));
				return null;
			}

			Bitmap bmp;
			using (var ms = new MemoryStream(rawFile))
			{
				bmp = new Bitmap(ms);
			}

			return bmp;
		}

		#endregion

		string CreateNewImagename()
		{
			return String.Format("{0}\\{1}.jpg",BaseImagePath,Guid.NewGuid ().ToString ().Replace ("-", "_"));
		}


		/// <summary>
		/// Bereitet das ImageRepo für den gebrauch vor
		/// </summary>
		void PrepareImageRepository()
		{
			BaseImagePath = System.IO.Path.Combine (Environment.CurrentDirectory, "Images");

			_fileHelper.CreateDirectory (BaseImagePath);
		}

		public static Bitmap ResizeImage(Image image, int width, int height)
		{
			var destRect = new Rectangle(0, 0, width, height);
			var destImage = new Bitmap(width, height);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using (var wrapMode = new ImageAttributes())
				{
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, destRect, 0, 0, image.Width,image.Height, GraphicsUnit.Pixel, wrapMode);
				}
			}

			return destImage;
		}

	}
}

