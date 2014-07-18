namespace Sitecore.MobileSDK.API.Exceptions
{
    using System;

    public class ProcessUserRequestException : SitecoreMobileSdkException
    {
        public ProcessUserRequestException(string message, Exception inner = null) 
            : base (message, inner)
        {
        }
    }
}

