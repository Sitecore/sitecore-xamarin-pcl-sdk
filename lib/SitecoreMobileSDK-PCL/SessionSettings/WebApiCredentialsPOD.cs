
namespace Sitecore.MobileSDK.SessionSettings
{
  using System;

  public class WebApiCredentialsPOD : IWebApiCredentials
  {
    public WebApiCredentialsPOD(string userName, string password)
    {
      this.UserName = userName;
      this.Password = password;
    }

    public IWebApiCredentials CredentialsShallowCopy()
    {
      return new WebApiCredentialsPOD(this.UserName, this.Password);
    }

    public string UserName
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

