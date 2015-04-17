using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using BTZ.Common;
using BTZ.Infrastructure;
using BTZ.Data;
using ServiceStack.OrmLite;

namespace BTZ.DataAccess
{
	/// <summary>
	/// Datenzugriff auf die Nutzer
	/// Jonas Ahlf 16.04.2015 10:27:32
	/// </summary>
	public class UserRepository: IUserRepository
	{
		List<User> _localUser;
		ILog s_log = LogManager.GetLogger(typeof(UserRepository));
		readonly DatabaseHandler handler;
		/// <summary />
		public UserRepository (DatabaseHandler handler)
		{
			this.handler = handler;
			_localUser = new List<User> ();
		}
		

		#region Get
		/// <summary />
		public List<User> GetAllUser()
		{

			_localUser = handler.Database.LoadSelect<User> ();
			Console.WriteLine (_localUser.Count);

			return _localUser;
		}
		#endregion

		#region Update

		/// <summary />
		public void UpdateUser(User user)
		{
			handler.Database.Update<User> (user);
			s_log.Info (String.Format ("Updated User {0}", user.Username));
		}
		/// <summary />
		public void UpdateUsers(IList<User> users)
		{
			foreach (var user in users) {
				UpdateUser (user);
			}
		}
		#endregion

		#region Delete
		/// <summary />
		public void DeleteUser(User user)
		{
			handler.Database.Delete<User> (user);
			s_log.Info (String.Format ("Deleted User {0}", user.Username));
		}
		/// <summary />
		public void DeleteUsers(IList<User> users)
		{
			foreach (var user in users) {
				DeleteUser (user);
			}
		}
		#endregion

		#region Insert
		/// <summary />
		public void AddUser(User user)
		{
			handler.Database.Insert<User> (user);
			s_log.Info (String.Format ("Added User {0}", user.Username));
		}
		/// <summary />
		public void AddUsers(IList<User> users)
		{
			foreach (var user in users) {
				AddUser (user);
			}
		}
		#endregion

		#region Member
		void UpdateUser()
		{
			_localUser = handler.Database.LoadSelect<User> ();
		}
		#endregion
	}
}

