namespace Sitecore.MobileSDK.Exceptions
{
	using System;

    public class ScResponseException : SitecoreMobileSdkException
	{
		public ScErrorResponse Response { get; private set; }

        public ScResponseException (ScErrorResponse response, Exception inner = null) 
            : base( response.Message, inner )
		{
			this.Response = response;
		}

        private ScResponseException (string message) : base (message)
		{
		}

        private ScResponseException (string message, Exception inner) : base (message, inner)
		{
		}
	}
}