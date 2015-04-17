using System;
using BTZ.Infrastructure;
using BTZ.Common;
using Newtonsoft.Json;
using log4net;

namespace BTZ.Core
{
	public class LogInMessageProcessor : ILogInMessageProcessor
	{
		ILoginManager _loginManager;

		static readonly ILog s_log = LogManager.GetLogger(typeof(LogInMessageProcessor));

		public LogInMessageProcessor (ILoginManager _loginManager)
		{
			this._loginManager = _loginManager;
		}
		

		#region ILogInMessageProcessor implementation

		public LoginResponse LoginUser (string jsonString)
		{
			var request = toRequest (jsonString);

			if (request == null) {
				return new LoginResponse (){Success = false, Token ="" };
			}

			var login = _loginManager.Login (request);

			return new LoginResponse (){ Success = login.Item2, Token = login.Item1 };
		}

		public LoginResponse RegisterUser (string jsonString)
		{
			var request = toRequest (jsonString);

			if (request == null) {
				return new LoginResponse (){Success = false, Token ="" };
			}

			var user = _loginManager.RegisterUser (request);

			return user == null ? new LoginResponse () {
				Success = false,
				Token = ""
			} : new LoginResponse () {
				Success = true,
				Token = user.Token
			};
		}

		#endregion

		LoginData toRequest(string js)
		{
			if (String.IsNullOrEmpty (js)) {
				s_log.Error ("Could not parse LoginRequest");
				return null;
			}

			return JsonConvert.DeserializeObject<LoginData> (js);
		}
	}
}

