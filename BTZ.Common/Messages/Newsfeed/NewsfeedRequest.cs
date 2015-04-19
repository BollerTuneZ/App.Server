using System;

namespace BTZ.Common
{
	public class NewsfeedRequest
	{
		public NewsfeedRequest ()
		{
		}

		public RequestType RequestType{ get; set;}

		public int Id {get;set;}

		public int[] IdPaket{ get; set;}

		public NewsfeedDto SingleDto{get;set;}

		public NewsfeedDto[] Paket{get;set;}
	}
}

