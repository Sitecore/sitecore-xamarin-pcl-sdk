namespace MobileSDKUnitTest.Mock
{
  using System;
  using Sitecore.MobileSDK.API;

  public class WebApiCredentialsPOD : IWebApiCredentials
  {
    public WebApiCredentialsPOD(string username, string password)
    {
      this.Username = username;
      this.Password = password;
    }

    public IWebApiCredentials CredentialsShallowCopy()
    {
      return new WebApiCredentialsPOD(this.Username, this.Password);
    }

    public void Dispose()
    {
      this.Username = null;
      this.Password = null;
    }

    public string Username
    {
      get;
      private set;
    }

    public string Password
    {
      get;
      private set;
    }
  }
}

