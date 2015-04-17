using System;
using System.Collections.Generic;
using BTZ.Common;
using BTZ.Data;

namespace BTZ.Infrastructure
{
	public interface IUserRepository
	{
		#region Get
		List<User> GetAllUser ();

		#endregion

		#region Update
		void UpdateUser(User user);

		void UpdateUsers (IList<User> users);

		#endregion

		#region Delete
		void DeleteUser(User user);

		void DeleteUsers (IList<User> users);

		#endregion

		#region Insert
		void AddUser (User user);

		void AddUsers (IList<User> users);

		#endregion

	}
}

