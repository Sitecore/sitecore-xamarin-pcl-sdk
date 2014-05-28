namespace Sitecore.MobileSDK
{
	using System;

	public class ScResponseException : Exception
	{
		public ScErrorResponse Response { get; private set; }

		public ScResponseException (ScErrorResponse response)
		{
			this.Response = response;
		}

		public ScResponseException (string message) : base (message)
		{
		}

		public ScResponseException (string message, Exception inner) : base (message, inner)
		{
		}
	}
}