using System;
using BTZ.Common;
using BTZ.Data;

namespace BTZ.Infrastructure
{
	/// <summary>
	/// Behandelt alle Login und Registrierungsanfragen
	/// Jonas Ahlf 16.04.2015 10:30:47
	/// </summary>
	public interface ILoginManager
	{
		/// <summary>
		/// Führt einen Login durch
		/// </summary>
		/// <param name="loginData">Login data.</param>
		Tuple<string,bool> Login(LoginData loginData);

		/// <summary>
		/// Verfiziert einen User anhand des Token
		/// </summary>
		/// <returns>The token.</returns>
		/// <param name="token">Token.</param>
		User VerifyToken(string token);

		/// <summary>
		/// Registriert einen Nutzer
		/// </summary>
		/// <returns>The user.</returns>
		/// <param name="loginData">Login data.</param>
		User RegisterUser(LoginData loginData);
	}
}

