using System;
using BTZ.Infrastructure;
using System.Collections;
using BTZ.Common;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace BTZ.DataAccess
{
	public class LoginManager : ILoginManager
	{
		IDictionary<User,string> loggedInUser = new Dictionary<User,string>();

		readonly IUserRepository _userRepository;

		readonly static ILog s_log = LogManager.GetLogger (typeof(LoginManager));   

		public LoginManager (IUserRepository _userRepository)
		{
			this._userRepository = _userRepository;
		}

		#region ILoginManager implementation
		public Tuple<string, bool> Login (LoginData loginData)
		{
			var result = CheckUserData (loginData);
			if (!result.Item1) {
				return new Tuple<string, bool> ("", false);
			}
			var guid = LoggUserIn (result.Item2);
			return new Tuple<string, bool> (guid, true);
		}
		public User VerifyToken (string token)
		{
			foreach (var item in loggedInUser) {
				if (item.Value.Equals (token)) {
					s_log.Info (String.Format ("User {0} logged in with token {1}", item.Key.Username, token));
					return item.Key;
				}
			}
			s_log.Warn (String.Format ("No User found with token {0}", token));
			return null;
		}
		#endregion

		string LoggUserIn(User user)
		{
			string guid = Guid.NewGuid ().ToString ();

			if (loggedInUser.ContainsKey (user)) {
				loggedInUser.Remove (user);
			}

			loggedInUser.Add (new KeyValuePair<User, string> (user, guid));
			return guid;
		}

		Tuple<bool,User> CheckUserData(LoginData data)
		{

			var user = _userRepository.GetAllUser ().FirstOrDefault (n => n.Username.Equals (data.Username));

			if (user == null) {
				return new Tuple<bool, User> (false, null);
			}

			if (user.Password.Equals (data.Password)) {
				return new Tuple<bool, User> (true, user);
			}

			return new Tuple<bool, User> (false, null);
		}

	}
}

