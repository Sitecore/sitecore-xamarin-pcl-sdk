
namespace Sitecore.MobileSDK.Exceptions
{
    using System;

	public class WebApiJsonError
	{
		public int StatusCode { get; private set;}

		public string Message { get; private set;}

		public WebApiJsonError (int statusCode, string message)
		{
			this.StatusCode = statusCode;
			this.Message = message;
		}

        private WebApiJsonError()
        {
        }
	}
}