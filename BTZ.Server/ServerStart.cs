using System;
using BTZ.Infrastructure;
using BTZ.DataAccess;
using log4net;
using System.Threading;
using TinyIoC;
using BTZ.Data;

namespace BTZ.Server
{
	/// <summary>
	/// Einstiegspunkt für den Server
	/// Jonas Ahlf 16.04.2015 12:49:31
	/// </summary>
	public class ServerStart
	{
		
		private IBTZHosts _hosts;
		private static readonly ILog s_log = LogManager.GetLogger (typeof(ServerStart));
		private Thread _serviceThread;
		Bootstrap bootStrap = new Bootstrap ();
		public ServerStart ()
		{
			LogSetup.Setup ();
		}


		internal void StartUp()
		{

			bootStrap.Register ();

			_hosts = TinyIoCContainer.Current.Resolve<IBTZHosts> ();
			var _userRepo = TinyIoCContainer.Current.Resolve<IUserRepository> ();

			if (_hosts == null) {
				s_log.Error ("Hosts are null!");
				return;
			}

			_serviceThread = new Thread (StartServices);
			_serviceThread.Start ();

			Thread.Sleep (5000);
			Console.WriteLine ("Taste drücken um den Server zu beenden");
			Console.ReadKey ();
			s_log.Info ("Manuell Stop of the Server");
			_hosts.Stop ();
			Thread.Sleep (2000);
		}

		void StartServices()
		{
			_hosts.Start ();
		}
		
	}
}

