using System;
using BTZ.Common;
namespace BTZ.Core
{
	public class ServiceTest : IBaseAppService
	{
		public ServiceTest ()
		{
		}
		public string Register (string loginData)
		{
			return "x";

		}

		public string Login (string loginData)
		{
			return "y";

		}

		public string GetWallPosts (string token, string request)
		{
			return "z";
		}

		public string SendWallPost (string token, string data)
		{
			return "g";
		}

		public void Test ()
		{
			Console.WriteLine ("Test");
		}
	}
}

