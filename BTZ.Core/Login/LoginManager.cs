using System;
using BTZ.Infrastructure;
using BTZ.Common;
using System.Linq;
using BTZ.Data;
using log4net;
using BTZ.DataAccess;

namespace BTZ.Core
{
	public class LoginManager : ILoginManager
	{

		private readonly IUserRepository _userRepository;
		private readonly ILog s_log = LogManager.GetLogger(typeof(LoginManager));


		public LoginManager (IUserRepository userRepo)
		{
			_userRepository = userRepo;
			if (_userRepository == null) {
				throw new ArgumentNullException ("UserRepository");
			}
		}
		

		#region ILoginManager implementation

		public Tuple<string, bool> Login (LoginData loginData)
		{
			var user = _userRepository.GetAllUser ().FirstOrDefault (n => n.Username.ToLower().Equals(loginData.Username.ToLower()));

			if (user == null) {
				return new Tuple<string, bool> ("", false);
			}

			if (user.Password.Equals (loginData.Password)) {
				user.Token = Guid.NewGuid ().ToString ();
				_userRepository.UpdateUser (user);
				return new Tuple<string, bool> (user.Token, true);
			}

			return new Tuple<string, bool> ("", false);
		}

		public User VerifyToken (string token)
		{
			return _userRepository.GetAllUser().FirstOrDefault(t => t.Token.Equals(token));
		}

		public User RegisterUser (LoginData loginData)
		{
			if (_userRepository.GetAllUser ().Any (n => n.Username.ToLower ().Equals (loginData.Username.ToLower ()))) {
				s_log.Info (String.Format ("Multiple registration of user {0}, not allowed", loginData.Username));
				return null;
			}

			User nUser = new User () {
				Username = loginData.Username,
				Password = loginData.Password,
				Token = Guid.NewGuid().ToString(),
			};
			s_log.Info (String.Format ("User {0} Successfully registered", loginData.Username));

			_userRepository.AddUser (nUser);

			return nUser;
		}
		#endregion
	}
}

