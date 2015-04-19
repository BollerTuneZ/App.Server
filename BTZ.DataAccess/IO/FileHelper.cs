using System;
using BTZ.Infrastructure;
using System.IO;
using log4net;
using System.Linq;


namespace BTZ.DataAccess
{
	public class FileHelper : IFileHelper
	{
		static readonly ILog s_log = LogManager.GetLogger (typeof(FileHelper));

		public FileHelper ()
		{
		}

		#region IFileHelper implementation

		public byte[] ReadFile (string filename)
		{

			if (!File.Exists (filename)) {
				s_log.Warn (String.Format ("File {0}, does not exists", filename));
				return null;
			}

			return File.ReadAllBytes (filename);
		}

		public FileStream GetReadFileStream (string path)
		{
			if (!File.Exists (path)) {
				s_log.Warn (String.Format ("File {0}, does not exists", path));
				return null;
			}

			return new FileStream (path, FileMode.Open, FileAccess.Read);
		}

		public bool WriteFile (string path, byte[] data)
		{
			if (File.Exists (path)) {
				s_log.Warn (String.Format ("File {0}, does already exists", path));
				return false;
			}

			File.WriteAllBytes (path, data);
			return true;
		}

		public FileStream GetWriteFileStream (string path)
		{
			if (File.Exists (path)) {
				s_log.Warn (String.Format ("File {0}, does already exists", path));
				return null;
			}

			return new FileStream (path, FileMode.Open, FileAccess.Write);
		}

		public bool CreateDirectory (string path)
		{
			var dirInfo = new DirectoryInfo (path);

			if (dirInfo.Exists) {
				s_log.Warn (String.Format ("Directory {0}, already exists", path));
				return false;
			}

			dirInfo.Create ();
			return true;
		}

		public string[] ReadDirectoryFiles (string path)
		{
			var dirInfo = new DirectoryInfo (path);
			if (!dirInfo.Exists) {
				s_log.Warn (String.Format ("Directory {0}, does not exists", path));
				return null;
			}

			return dirInfo.GetFiles ().ToList ().Select (i => i.FullName).ToArray ();
		}

		public bool DeleteFile (string path)
		{
			if (!File.Exists (path)) {
				s_log.Warn (String.Format ("File {0}, does not exists", path));
				return false;
			}

			try {
				File.Delete(path);
				return true;
			} catch (Exception ex) {
				s_log.Error (String.Format("Could not delete file{0}, {1}",path,ex));
				return false;
			}
		}
		#endregion

	}
}

