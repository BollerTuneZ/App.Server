using System;
using System.Drawing;

namespace BTZ.Infrastructure
{
	/// <summary>
	/// Verwaltet die Speicherung und das Lesen von Bilddateien
	/// Jonas Ahlf 19.04.2015 22:38:47
	/// </summary>
	public interface IImageRepository
	{
		

		/// <summary>
		/// Speichert ein Bild ab und gibt dessen Pfad zurück
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="image">Image.</param>
		Tuple<bool,string> SaveImage(byte[] image);



		/// <summary>
		/// Löscht ein Bild
		/// </summary>
		/// <returns><c>true</c>, if image was deleted, <c>false</c> otherwise.</returns>
		/// <param name="path">Path.</param>
		bool DeleteImage(string path);

		/// <summary>
		/// Gibt ein Bild mit dem angegebenen Pfad zurück
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="path">Path.</param>
		Image GetImage(string path);
		/// <summary>
		/// Gibt ein Bild mit dem angegebenen Pfad zurück
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="path">Path.</param>
		byte[] GetImageBytes(string path);
		/// <summary>
		/// Gibt ein Bild mit dem angegebenen Pfad zurück
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="path">Path.</param>
		Bitmap GetBitmap(string path);


	}
}

