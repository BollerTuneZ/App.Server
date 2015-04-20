using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using BTZ.Common;
using log4net;
using BTZ.Infrastructure;
using System.Threading;

namespace BTZ.Core
{
	public class TcpService
	{

		static readonly ILog s_log = LogManager.GetLogger (typeof(TcpService));

		#region Processors
		readonly ILogInMessageProcessor _loginMessageProcessor;
		readonly INewsfeedMessageProcessor _newsfeedMessageProcessor;
		#endregion

		public TcpService ()
		{
			_loginMessageProcessor = TinyIoC.TinyIoCContainer.Current.Resolve<ILogInMessageProcessor> ();
		}

		public void Start()
		{
			ServerThread ();
		}

		void ServerThread ()
		{
			try {
				IPAddress ipAd = IPAddress.Parse ("192.168.1.3");
				// use local m/c IP address, and 
				// use the same in the client

				/* Initializes the Listener */
				TcpListener myList = new TcpListener (ipAd, 55566);

				/* Start Listeneting at the specified port */        
				myList.Start ();

				Console.WriteLine ("The server is running at port 8001...");    
				Console.WriteLine ("The local End point is  :" +
				myList.LocalEndpoint);
				Console.WriteLine ("Waiting for a connection.....");

				while (true) {
					Socket s = myList.AcceptSocket ();

					new Thread(() =>
						HandleClient (s)).Start();
				}
			} catch (Exception e) {
				s_log.Error (e);
			}    
		}

		void HandleClient(Socket client)
		{
			NetworkStream stream = new NetworkStream (client);

			StreamReader sr = new StreamReader (stream);
			StreamWriter sw = new StreamWriter (stream);
			var request = sr.ReadLine ();

			BaseDto dto;

			try {

				dto = JsonConvert.DeserializeObject<BaseDto>(request);

			} catch (Exception ex) {
				s_log.Error (ex);
				return;
			}

			object result;

			switch (dto.Type) {
			case DtoType.Login:
				result = _loginMessageProcessor.LoginUser (dto.JsonObject);
				break;
			case DtoType.Register:
				result = _loginMessageProcessor.RegisterUser (dto.JsonObject);
				break;
			case DtoType.Newsfeed:
				result = _newsfeedMessageProcessor.ProcessNewsfeedRequest (dto.JsonObject);
				break;
			default:
				result = null;
				break;
			}

			if (result == null) {
				sw.WriteLine ("INVALID");
				client.Close ();
				return;
			}
			sw.WriteLine (JsonConvert.SerializeObject (result));
			client.Close ();
		}

	}

}

