using System;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;
using System.Data;
using BTZ.Data;


namespace BTZ.DataAccess
{
	public  class DatabaseHandler
	{
		//Server=localhost;Database=btz;Uid=root;Pwd=;
		public DatabaseHandler ()
		{
			OrmLiteConfig.DialectProvider = MySqlDialectProvider.Instance;

			Database = "Server = 127.0.0.1; Database = btz; Uid = root; Pwd =".OpenDbConnection ();
			Database.CreateTable<User> ();
			Database.CreateTable<WallPost> ();
		}

		public IDbConnection Database;




	}
}

