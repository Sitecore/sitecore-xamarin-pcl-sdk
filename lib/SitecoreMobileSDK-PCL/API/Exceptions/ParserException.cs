namespace Sitecore.MobileSDK.API.Exceptions
{
  using System;

  public class ParserException : SitecoreMobileSdkException
  {
    public ParserException(string message, Exception inner = null)
      : base(message, inner)
    {
    }
  }
}

