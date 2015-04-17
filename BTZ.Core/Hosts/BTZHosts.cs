using System;
using System.ServiceModel.Web;
using BTZ.Infrastructure;
using log4net;

namespace BTZ.Core
{
	public class BTZHosts: IBTZHosts
	{
		private WebServiceHost _appService;
		private readonly ILog s_log = LogManager.GetLogger (typeof(BTZHosts));
		Uri _appServiceUri;

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
			_appService.Close ();
		}

		#endregion

		void StartServices()
		{

			_appServiceUri = new Uri (String.Format ("http://localhost:{0}/btz", 56534));

			_appService = new WebServiceHost (typeof(AppService), new Uri[]{ _appServiceUri });
			_appService.Faulted += OnAppServiceFaulted;
			_appService.Opening += OnAppServiceOpening;
			_appService.Opened += OnAppServiceOpened;
			_appService.Closing += OnAppServiceClosing;
			_appService.Closed += OnAppServiceClosed;
			_appService.Open ();
		}

		void OnAppServiceClosed (object sender, EventArgs e)
		{
			s_log.Warn (String.Format("AppService closed at {0}",DateTime.Now));
		}

		void OnAppServiceClosing (object sender, EventArgs e)
		{
			s_log.Warn ("AppService is going to stop");
		}

		void OnAppServiceOpened (object sender, EventArgs e)
		{
			s_log.Info (String.Format ("AppService started on {0}", _appServiceUri));

		}

		void OnAppServiceOpening (object sender, EventArgs e)
		{
			s_log.Info (String.Format ("AppService is going to start on {0}", _appServiceUri));
		}

		void OnAppServiceFaulted (object sender, EventArgs e)
		{
			s_log.Error(String.Format("Appservice Faulted {0}",e.ToString()));
		}
	}
}

