namespace Sitecore.MobileSDK.API.Exceptions
{
    using System;

    public class SitecoreMobileSdkException : Exception
    {
        public SitecoreMobileSdkException (string message, Exception inner = null) : base (message, inner)
        {
        }
    }
}

