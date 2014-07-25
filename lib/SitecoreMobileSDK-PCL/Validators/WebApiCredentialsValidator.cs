namespace Sitecore.MobileSDK.Validators
{
  using System;
  using Sitecore.MobileSDK.API;

  public static class WebApiCredentialsValidator
  {
    public static bool IsValidCredentials(IWebApiCredentials credentials)
    {
      if (null == credentials)
      {
        return false;
      }
      else if (string.IsNullOrWhiteSpace(credentials.Username))
      {
        // anonymous session
        return true;
      }
      else
      {
        if (string.IsNullOrEmpty(credentials.Password))
        {
          return false;
        }
        else
        {
          return true;
        }
      }
    }

    public static bool IsAnonymousSession(IWebApiCredentials credentials)
    {
      if (!WebApiCredentialsValidator.IsValidCredentials(credentials))
      {
        return false;
      }

      return string.IsNullOrWhiteSpace(credentials.Username);
    }
  }
}

