namespace Sitecore.MobileSDK.WinRTPasswordProvider
{
    using PasswordProvider.Interface;
    using System;
    using System.Linq;
    using Windows.Security.Credentials;

    public class SecureStringPasswordProvider : IWebApiCredentials
  {

    private const string RESOURCE_NAME = "SITECORE_PASSWORD_PROVIDER_CREDENTIALS_STORAGE";

    private string userName;

    private SecureStringPasswordProvider()
    {
    }

    private SecureStringPasswordProvider(string userName)
    {
       this.userName = userName;
    }

    #region PasswordVault
    private void SaveCredential(string userName, string password)
    {
        var vault = new PasswordVault();
        this.userName = userName;
        var credential = new PasswordCredential(RESOURCE_NAME, userName, password);

        vault.Add(credential);
    }

    private string GetStoredPassword()
    {
        string password = null;

        var vault = new PasswordVault();
        try
        {
            var credential = vault.FindAllByResource(RESOURCE_NAME).FirstOrDefault();
            if (credential != null)
            {
                password = vault.Retrieve(RESOURCE_NAME, this.userName).Password;
            }
        }
        catch (Exception e)
        {
         throw new Exception("[SecureStringPasswordProvider] : can not get password, ", e);
        }

        return password;
    }

    private void RemoveCredential()
    {
        var vault = new PasswordVault();
        try
        {
            vault.Remove(vault.Retrieve(RESOURCE_NAME, this.userName));
        }
        catch (Exception e)
        {
          throw new Exception("[SecureStringPasswordProvider] : can not clear password storage, ", e);
        }
    }
    
    #endregion PasswordVault

    public SecureStringPasswordProvider(string insecureLogin, string insecurePassword)
    {
      if (string.IsNullOrEmpty(insecureLogin))
      {
          throw new ArgumentException("[SecureStringPasswordProvider] : username cannot be null or empty");
      }
      this.SaveCredential(insecureLogin, insecurePassword);
    }

    public void Dispose()
    {
      this.RemoveCredential();
    }

    #region IWebApiCredentials

    public SecureStringPasswordProvider PasswordProviderCopy()
    {
       return new SecureStringPasswordProvider(this.userName);
    }

    public IWebApiCredentials CredentialsShallowCopy()
    {
        return this.PasswordProviderCopy();
    }

    public string Username
    {
      get
      {
        return this.userName;
      }
    }

    public string Password
    {
      get
      {
        return this.GetStoredPassword();
      }
    }
    #endregion IWebApiCredentials
  }
}

