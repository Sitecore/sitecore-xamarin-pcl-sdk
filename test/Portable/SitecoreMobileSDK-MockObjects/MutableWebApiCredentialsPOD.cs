namespace SitecoreMobileSDKMockObjects
{
  using System;
  using Sitecore.MobileSDK.API;

  public class MutableWebApiCredentialsPOD : IWebApiCredentials
  {
    public MutableWebApiCredentialsPOD(string username, string password)
    {
      this.Username = username;
      this.Password = password;
    }

    public IWebApiCredentials CredentialsShallowCopy()
    {
      return new MutableWebApiCredentialsPOD(this.Username, this.Password);
    }

    public string Username
    {
      get;
      set;
    }

    public string Password
    {
      get;
      set;
    }  
  }
}

