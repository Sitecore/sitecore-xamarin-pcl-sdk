using System;

namespace Sitecore.MobileSDK.Exceptions
{
    public class ParserException : SitecoreMobileSdkException
    {
        public ParserException(string message, Exception inner = null) 
            : base (message, inner)
        {
        }
    }
}

