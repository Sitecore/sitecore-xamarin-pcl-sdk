using System;

namespace Sitecore.MobileSDK.Exceptions
{
    public class SitecoreMobileSdkException : Exception
    {
        public SitecoreMobileSdkException (string message, Exception inner = null) : base (message, inner)
        {
        }
    }
}

