using System;

namespace Sitecore.MobileSDK
{
	public class ScErrorResponse
	{
		public int StatusCode { get; private set;}

		public string Message { get; private set;}

		public ScErrorResponse (int statusCode, string message)
		{
			this.StatusCode = statusCode;
			this.Message = message;
		}
	}
}