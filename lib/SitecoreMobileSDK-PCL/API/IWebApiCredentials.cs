
namespace Sitecore.MobileSDK.API
{
  using System;

  public interface IWebApiCredentials : IDisposable
  {
    IWebApiCredentials CredentialsShallowCopy();

    string Username
    {
      get;
    }

    string Password
    {
      get;
    }
  }
}
