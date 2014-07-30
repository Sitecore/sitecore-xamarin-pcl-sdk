namespace Sitecore.MobileSDK.Validators
{
  using System;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public static class MediaOptionsValidator
  {
    public static bool IsValidMediaOptions(IDownloadMediaOptions options)
    {
      if (null == options)
      {
        return false;
      }
      
      if (options.IsEmpty)
      {
        return false;
      }

      return true;
    }
  }
}

