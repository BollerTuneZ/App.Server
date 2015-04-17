using System;

namespace BTZ.Infrastructure
{
	/// <summary>
	/// Startet und stoppt die Web Services
	/// Jonas Ahlf 16.04.2015 10:48:47
	/// </summary>
	public interface IBTZHosts
	{
		/// <summary>
		/// Startet die Services
		/// </summary>
		void Start();
		/// <summary>
		/// Stopt die Services
		/// </summary>
		void Stop();
	}
}

