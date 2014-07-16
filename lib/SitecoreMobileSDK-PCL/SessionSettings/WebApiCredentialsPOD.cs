
namespace Sitecore.MobileSDK.SessionSettings
{
  using System;

  public class WebApiCredentialsPOD : IWebApiCredentials
  {
    public WebApiCredentialsPOD(string userName, string password)
    {
      this.Login = userName;
      this.Password = password;
    }

    public IWebApiCredentials CredentialsShallowCopy()
    {
      return new WebApiCredentialsPOD(this.Login, this.Password);
    }

    public string Login
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

