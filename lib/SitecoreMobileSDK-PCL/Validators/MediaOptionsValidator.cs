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
      else if (options.IsEmpty)
      {
        return false;
      }

      return true;
    }

    public static void ValidateMediaOptions(IDownloadMediaOptions options, string errorMessage)
    {
      bool isValidOptions = MediaOptionsValidator.IsValidMediaOptions(options);
      if (!isValidOptions)
      {
        throw new ArgumentException(errorMessage);
      }
    }
  }
}

