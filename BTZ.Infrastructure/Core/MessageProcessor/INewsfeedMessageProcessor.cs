using System;

namespace BTZ.Infrastructure
{
	public interface INewsfeedMessageProcessor
	{
		string ProcessNewsfeedRequest(string request);
	}
}

