using System;
using System.Drawing;
using System.IO;

namespace BTZ.DataAccess
{
	public static class ImageExtension
	{
		public static byte[] ToByteArray(this Image image)
		{
			return imageToByteArray (image);
		}

		public static Image ByteToImage(this Image image ,byte[] data)
		{
			return byteArrayToImage (data);
		}

		static byte[] imageToByteArray(System.Drawing.Image imageIn)
		{
			MemoryStream ms = new MemoryStream();
			imageIn.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
			return  ms.ToArray();
		}

		static Image byteArrayToImage(byte[] byteArrayIn)
		{
			MemoryStream ms = new MemoryStream(byteArrayIn);
			Image returnImage = Image.FromStream(ms);
			return returnImage;
		}

	}
}

