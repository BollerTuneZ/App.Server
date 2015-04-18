using System;
using BTZ.Infrastructure;
using Newtonsoft.Json;
using BTZ.Common;


namespace BTZ.Core
{
	public class AppService : IBaseAppService
	{
		ILogInMessageProcessor _loginMessageProcessor;

		public AppService ()
		{
			this._loginMessageProcessor = TinyIoC.TinyIoCContainer.Current.Resolve<ILogInMessageProcessor> ();
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


		public void Test ()
		{
			Console.WriteLine ("Hallo Welt");
		}

		#endregion

		string ParseObject(object obj)
		{
			return JsonConvert.SerializeObject (obj);
		}

	}
}

