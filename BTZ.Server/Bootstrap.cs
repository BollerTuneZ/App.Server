using System;
using TinyIoC;
using BTZ.Infrastructure;
using BTZ.DataAccess;
using BTZ.Core;


namespace BTZ.Server
{
	/// <summary>
	/// Jonas Ahlf 17.04.2015 22:44:18
	/// </summary>
	public class Bootstrap
	{
		public void Register()
		{

			TinyIoCContainer.Current.Register<DatabaseHandler> ().AsSingleton ();
			TinyIoCContainer.Current.Register<IUserRepository,UserRepository> ().AsSingleton ();
			TinyIoCContainer.Current.Register<ILoginManager,LoginManager> ().AsSingleton ();
			TinyIoCContainer.Current.Register<ILogInMessageProcessor,LogInMessageProcessor> ().AsSingleton ();
			TinyIoCContainer.Current.Register<IBTZHosts,BTZHosts> ();
			TinyIoCContainer.Current.Register<IFileHelper,FileHelper> ();
			TinyIoCContainer.Current.Register<IImageRepository,ImageRepository> ();

		}
	}
}

