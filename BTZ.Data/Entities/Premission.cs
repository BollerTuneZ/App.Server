using System;

namespace BTZ.Data
{
	/// <summary>
	/// gibt die einzelnen Stufen der Berechtigungen dar
	/// Jonas Ahlf 30.03.2015 21:25:17
	/// </summary>
	public enum Premission
	{
		/// <summary>
		/// Komplettberechtigung (Super Admin)
		/// </summary>
		Super,
		/// <summary>
		/// Fast allen zugriff
		/// </summary>
		High,
		/// <summary>
		/// Auf alle nicht Fahrrelevanten eigenschaften zugriff
		/// </summary>
		Mid,
		/// <summary>
		/// Nur auf nicht wichtige inhalte zugriff
		/// </summary>
		Low
	}
}

