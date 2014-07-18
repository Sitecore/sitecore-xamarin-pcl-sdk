
namespace Sitecore.MobileSDK.API.Exceptions
{
    using System;

    public class RsaHandshakeException : SitecoreMobileSdkException
    {
        public RsaHandshakeException(string message, Exception inner = null) 
            : base (message, inner)
        {
        }
    }
}

