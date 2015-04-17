using System;
using System.Runtime.Serialization;

namespace BTZ.Common
{
	[DataContract]
	public class EntityMessage
	{
		[DataMember]
		public string Type{ get; set; }

		[DataMember]
		public string Entity{ get; set; }
	}
}

