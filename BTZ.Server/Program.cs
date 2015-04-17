using System;
namespace BTZ.Server
{
	class MainClass
	{
		static ServerStart _serverStart;

		public static void Main (string[] args)
		{
			_serverStart = new ServerStart ();
			_serverStart.StartUp ();
		}
	}
}
