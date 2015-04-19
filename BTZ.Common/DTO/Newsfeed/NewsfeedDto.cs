using System;

namespace BTZ.Common
{
	public class NewsfeedDto
	{
		public NewsfeedDto ()
		{
		}

		public string Creator{ get; set;}

		/// <summary>
		/// Titel des Posts
		/// </summary>
		/// <value>The title.</value>
		public string Title{ get; set; }

		/// <summary>
		/// Schrifftlicher inhalt des Posts
		/// </summary>
		/// <value>The content.</value>
		public string Content{ get; set; }

		/// <summary>
		/// Kopfzeilenbild
		/// </summary>
		/// <value>The header image.</value>
		public byte[] HeaderImage{ get; set; }

		/// <summary>
		/// PostBild
		/// </summary>
		/// <value>The image.</value>
		public byte[] Image{ get; set; }
	}
}

