using System;
using BTZ.Common;

namespace BTZ.Infrastructure
{
	public interface ILogInMessageProcessor
	{

		LoginResponse LoginUser(string jsonString); 

		LoginResponse RegisterUser(string jsonString);
	}
}

