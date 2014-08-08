namespace Sitecore.MobileSDK.API.Exceptions
{
  using System;

  public class LoadDataFromNetworkException : SitecoreMobileSdkException
  {
    public LoadDataFromNetworkException(string message, Exception inner = null)
      : base(message, inner)
    {
    }
  }
}

