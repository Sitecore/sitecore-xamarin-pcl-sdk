namespace Sitecore.MobileSDK.MockObjects
{
  using Sitecore.MobileSDK.PasswordProvider.Interface;

  public class SSCCredentialsPOD : IWebApiCredentials
  {
    public SSCCredentialsPOD(string username, string password)
    {
      this.Username = username;
      this.Password = password;
    }

    public IWebApiCredentials CredentialsShallowCopy()
    {
      return new SSCCredentialsPOD(this.Username, this.Password);
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

