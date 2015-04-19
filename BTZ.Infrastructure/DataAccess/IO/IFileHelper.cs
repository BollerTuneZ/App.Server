using System;
using System.IO;

namespace BTZ.Infrastructure
{
	/// <summary>
	/// Helferklasse um Dateien zu schreiben und zu lesen.
	/// Verzeichnisse erstellen usw.
	/// Jonas Ahlf 19.04.2015 22:29:30
	/// </summary>
	public interface IFileHelper
	{
		/// <summary>
		/// Liest eine Datei und gibt diese als bytearray zurück
		/// </summary>
		/// <returns>The file.</returns>
		/// <param name="filename">Filename.</param>
		byte[] ReadFile (string filename);

		/// <summary>
		/// Öffnet einen FileStream und gibt ihn zurück
		/// </summary>
		/// <returns>The file stream.</returns>
		/// <param name="path">Path.</param>
		FileStream GetReadFileStream(string path);

		/// <summary>
		/// Schreibt eine Datei
		/// </summary>
		/// <returns><c>true</c>, if file was writed, <c>false</c> otherwise.</returns>
		/// <param name="path">Path.</param>
		/// <param name="data">Data.</param>
		bool WriteFile(string path, byte[] data);

		/// <summary>
		/// Gibt einen FileStream zum schreiben einer datei zurück
		/// </summary>
		/// <returns>The write file stream.</returns>
		/// <param name="path">Path.</param>
		FileStream GetWriteFileStream(string path);

		/// <summary>
		/// Löscht eine Datei
		/// </summary>
		/// <returns><c>true</c>, if file was deleted, <c>false</c> otherwise.</returns>
		/// <param name="path">Path.</param>
		bool DeleteFile(string path);

		/// <summary>
		/// Erstellt ein verzeichnis
		/// </summary>
		/// <returns><c>true</c>, if directory was created, <c>false</c> otherwise.</returns>
		/// <param name="path">Path.</param>
		bool CreateDirectory(string path);

		/// <summary>
		/// Liest ein Verzeichnis aus und gibt die Dateien,
		/// die sich in diesem befinden, zurück
		/// </summary>
		/// <returns>The directory files.</returns>
		/// <param name="path">Path.</param>
		string[] ReadDirectoryFiles(string path);

	}
}

