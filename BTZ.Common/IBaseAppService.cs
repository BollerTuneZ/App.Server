using System;

namespace BTZ.Common
{
	/// <summary>
	/// BasisService 
	/// Jonas Ahlf 17.04.2015 22:43:42
	/// </summary>
	public interface IBaseAppService
	{
		/// <summary>
		/// Registriert einen Nutzer.
		/// </summary>
		/// <param name="loginData">Login data.</param>
		string Register (string loginData);

		string Login (string loginData);

		string GetWallPosts(string token,string request);

		string SendWallPost(string token,string data);

		void Test();

	}
}

