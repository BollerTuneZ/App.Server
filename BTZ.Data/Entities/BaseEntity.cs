using System;
using ServiceStack.DataAnnotations;


namespace BTZ.Data
{
	/// <summary>
	/// Basisklasse aller Datenbankklassen
	/// Jonas Ahlf 30.03.2015 21:28:42
	/// </summary>
	public class BaseEntity 
	{
		/// <summary/>
		public BaseEntity ()
		{
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[AutoIncrement]
		public int Id { get; set; }
	}
}

