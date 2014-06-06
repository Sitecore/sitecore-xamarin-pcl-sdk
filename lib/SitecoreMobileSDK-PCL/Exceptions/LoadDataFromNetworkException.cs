using System;

namespace Sitecore.MobileSDK.Exceptions
{
    public class LoadDataFromNetworkException : SitecoreMobileSdkException
    {
        public LoadDataFromNetworkException (string message, Exception inner = null) 
            : base (message, inner)
        {
        }
    }
}

