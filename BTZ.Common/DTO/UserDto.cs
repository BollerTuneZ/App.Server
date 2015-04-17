using System;
using System.Runtime.Serialization;

namespace BTZ.Common
{
	[DataContract]
	public class UserDto
	{
		public UserDto ()
		{
		}

		[DataMember]
		public string Username{ get; set; }

		[DataMember]
		public string Password{ get; set; }
	}
}

