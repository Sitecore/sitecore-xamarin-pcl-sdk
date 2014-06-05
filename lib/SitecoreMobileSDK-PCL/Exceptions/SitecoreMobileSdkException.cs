using System;

namespace Sitecore.MobileSDK.Exceptions
{
    public class SitecoreMobileSdkException : Exception
    {
        public SitecoreMobileSdkException (string message) : base (message)
        {
        }

        public SitecoreMobileSdkException (string message, Exception inner) : base (message, inner)
        {
        }
    }
}

