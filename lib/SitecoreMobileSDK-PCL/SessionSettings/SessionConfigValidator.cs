using System;

namespace Sitecore.MobileSDK.SessionSettings
{
  public class SessionConfigValidator
  {
    private SessionConfigValidator()
    {
    }


    public static bool IsValidSchemeOfInstanceUrl(string url)
    {
      string lowercaseUrl = url.ToLowerInvariant();

      bool isHttps = lowercaseUrl.StartsWith("https://");
      bool isHttp  = lowercaseUrl.StartsWith("http://");
      bool result  = (isHttps || isHttp);

      return result;
    }

  }
}

