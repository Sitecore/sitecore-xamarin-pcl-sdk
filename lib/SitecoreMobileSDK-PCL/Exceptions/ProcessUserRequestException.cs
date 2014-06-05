using System;

namespace Sitecore.MobileSDK.Exceptions
{
    public class ProcessUserRequestException : SitecoreMobileSdkException
    {
        public ProcessUserRequestException(string message, Exception inner = null) 
            : base (message, inner)
        {
        }
    }
}

