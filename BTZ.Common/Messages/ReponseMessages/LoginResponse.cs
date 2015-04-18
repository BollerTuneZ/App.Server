using System;
using System.Runtime.Serialization;

namespace BTZ.Common
{
	/// <summary>
	/// Wird dem Client als antwort auf einen Login zurück gesendet
	/// Jonas Ahlf 03.04.2015 22:50:04
	/// </summary>
	public class LoginResponse
	{
		public bool Success{ get; set; }

		public string Token{ get; set; }
	}
}

