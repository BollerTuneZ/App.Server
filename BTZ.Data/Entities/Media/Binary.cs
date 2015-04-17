using System;

namespace BTZ.Data
{
	public class Binary : BaseEntity
	{
		public Binary ()
		{
		}

		/// <summary>
		/// Gets or sets the file location.
		/// </summary>
		/// <value>The file location.</value>
		public string FileLocation{ get; set; }
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public string Type{ get; set; }

	}
}

