using System;

namespace BTZ.Common
{
	public class BaseDto
	{
		public BaseDto ()
		{
		}

		public DtoType Type{ get; set; }

		public string JsonObject{ get; set; }
	}
}

