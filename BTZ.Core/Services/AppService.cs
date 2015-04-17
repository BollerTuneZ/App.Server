using System;
using BTZ.Common.Communication;
using BTZ.Infrastructure;
using Newtonsoft.Json;


namespace BTZ.Core
{
	public class AppService : IBaseAppService
	{
		ILogInMessageProcessor _loginMessageProcessor;


		public AppService (ILogInMessageProcessor _loginMessageProcessor)
		{
			this._loginMessageProcessor = _loginMessageProcessor;
		}

		#region IBaseAppService implementation

		public string Register (string loginData)
		{
			return ParseObject(_loginMessageProcessor.RegisterUser (loginData));
		}

		public string Login (string loginData)
		{
			return ParseObject (_loginMessageProcessor.LoginUser (loginData));
		}

		public string GetWallPosts (string token, string request)
		{
			throw new NotImplementedException ();
		}

		public string SendWallPost (string token, string data)
		{
			throw new NotImplementedException ();
		}

		#endregion

		string ParseObject(object obj)
		{
			return JsonConvert.SerializeObject (obj);
		}

	}
}

