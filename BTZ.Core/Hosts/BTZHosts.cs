using System;
using System.ServiceModel.Web;
using BTZ.Infrastructure;
using log4net;
using Zyan.Communication;
using BTZ.Common;
using Zyan.Communication.Security;
using Zyan.Communication.Protocols.Tcp;
using System.Threading;

namespace BTZ.Core
{
	public class BTZHosts: IBTZHosts
	{
		private readonly ILog s_log = LogManager.GetLogger (typeof(BTZHosts));
		private ZyanComponentHost _appService;
		public BTZHosts ()
		{
		}

		#region IBTZHosts implementation

		public void Start ()
		{
			s_log.Info ("Start Services...");
			StartServices ();
		}

		public void Stop ()
		{
			//_appService.UnregisterComponent<IBaseAppService> ();
		}

		#endregion

		void StartServices()
		{
			new Thread (() => {
				TcpService service = new TcpService();

				service.Start();
			}).Start ();

			/*

			var duplexSetup = new TcpDuplexServerProtocolSetup (55566, new NullAuthenticationProvider (), false);
			_appService = new ZyanComponentHost("appservice", duplexSetup);

			_appService.RegisterComponent<IBaseAppService, AppService>(ActivationType.SingleCall);*/
		}
	}
}

