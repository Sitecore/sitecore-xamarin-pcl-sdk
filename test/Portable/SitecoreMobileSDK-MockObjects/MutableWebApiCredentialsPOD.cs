namespace MobileSDKUnitTest.Mock
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

    public void Dispose()
    {
      this.Username = null;
      this.Password = null;
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

