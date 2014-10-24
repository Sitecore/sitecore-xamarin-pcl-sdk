namespace SecureStringPasswordProvider.iOS
{
  using System;
  using Sitecore.MobileSDK.PasswordProvider.Interface;


  public class SecureStringPasswordProvider : IWebApiCredentials, IDisposable
  {
    private global::Sitecore.MobileSDK.PasswordProvider.SecureStringPasswordProvider providerImpl;

    private SecureStringPasswordProvider()
    {
    }

    public SecureStringPasswordProvider(string insecureLogin, string insecurePassword)
    {
      this.providerImpl = 
        new global::Sitecore.MobileSDK.PasswordProvider.SecureStringPasswordProvider(
          insecureLogin,
          insecurePassword);
    }

    public void Dispose()
    {
      try
      {
        if (null != this.providerImpl)
        {
          this.providerImpl.Dispose();
          this.providerImpl = null;
        }
      }
      catch
      {
        // Suppressing all exceptions in destructor
      }
    }

    #region IWebApiCredentials
    public IWebApiCredentials CredentialsShallowCopy()
    {
      var result = new SecureStringPasswordProvider();
      result.providerImpl = this.providerImpl.PasswordProviderCopy();

      return result;
    }

    public string Username
    {
      get
      {
        return this.providerImpl.Username;
      }
    }

    public string Password
    {
      get
      {
        return this.providerImpl.Password;
      }
    }
    #endregion IWebApiCredentials
  }
}

