namespace Sitecore.MobileSDK.Exceptions
{
	using System;

    public class WebApiJsonErrorException : SitecoreMobileSdkException
	{
        public WebApiJsonStatusMessage Response { get; private set; }

        public WebApiJsonErrorException (WebApiJsonStatusMessage response, Exception inner = null) 
            : base( response.Message, inner )
		{
			this.Response = response;
		}

        private WebApiJsonErrorException (string message, Exception inner = null) 
            : base (message, inner)
		{
		}
	}
}