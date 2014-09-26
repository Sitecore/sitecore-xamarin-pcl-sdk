namespace SecureStringPasswordProvider.Windows
{
  using System;
  using SitecoreMobileSdkPasswordProvider.API;

  public class SecureStringPasswordProvider : IWebApiCredentials, IDisposable
  {
    private global::SecureStringPasswordProvider.API.SecureStringPasswordProvider providerImpl;

    private SecureStringPasswordProvider()
    {
    }

    public SecureStringPasswordProvider(string insecureLogin, string insecurePassword)
    {
      this.providerImpl =
        new global::SecureStringPasswordProvider.API.SecureStringPasswordProvider(
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
