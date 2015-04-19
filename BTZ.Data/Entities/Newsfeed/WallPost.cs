using System;


namespace BTZ.Data
{
	public class WallPost : BaseEntity
	{
		public WallPost ()
		{
		}

		public User Owner{ get; set; }

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
		public string HeaderImage{ get; set; }

		/// <summary>
		/// PostBild
		/// </summary>
		/// <value>The image.</value>
		public string Image{ get; set; }
	}
}

