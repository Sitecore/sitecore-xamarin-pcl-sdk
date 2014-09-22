namespace SecureStringPasswordProvider.API
{
  using System;
  using System.Security;
  using System.Runtime.InteropServices;
  using SitecoreMobileSdkPasswordProvider.API;


  public class SecureStringPasswordProvider : IWebApiCredentials, IDisposable
  {
    private SecureString encryptedLogin;
    private SecureString encryptedPassword;

    private SecureStringPasswordProvider()
    {
    }

    public SecureStringPasswordProvider(string insecureLogin, string insecurePassword)
    {
      this.encryptedLogin = SecureStringPasswordProvider.EncryptString(insecureLogin);
      this.encryptedPassword = SecureStringPasswordProvider.EncryptString(insecurePassword);
    }

    #region Encryption
    private static SecureString EncryptString(string insecureString)
    {
      var result = new SecureString();
      char[] managedCharacters = insecureString.ToCharArray();

      foreach (char currentCharacter in managedCharacters)
      {
        result.AppendChar(currentCharacter);
      }

      result.MakeReadOnly();
      return result;
    }

    private static string DecryptSecureString(SecureString source)
    {
      string result = null;
      int length = source.Length;
      IntPtr pointer = IntPtr.Zero;
      char[] chars = new char[length];

      try
      {
        pointer = Marshal.SecureStringToBSTR(source);
        Marshal.Copy(pointer, chars, 0, length);

        result = string.Join("", chars);
      }
      finally
      {
        if (pointer != IntPtr.Zero)
        {
          Marshal.ZeroFreeBSTR(pointer);
        }
      }

      return result;
    }
    #endregion


    public void Dispose()
    {
      try
      {
        if (null != this.encryptedLogin)
        {
          this.encryptedLogin.Dispose();
          this.encryptedLogin = null;
        }
      }
      catch
      {
        // Suppressing all exceptions in destructor
      }


      try
      {
        if (null != this.encryptedPassword)
        {
          this.encryptedPassword.Dispose();
          this.encryptedPassword = null;
        }
      }
      catch
      {
        // Suppressing all exceptions in destructor
      }
    }
  
    #region IWebApiCredentials
    public SecureStringPasswordProvider PasswordProviderCopy()
    {
      SecureStringPasswordProvider result = new SecureStringPasswordProvider();
      {
        result.encryptedLogin = this.encryptedLogin.Copy();
        result.encryptedPassword = this.encryptedPassword.Copy();
      }

      return result;
    }

    public IWebApiCredentials CredentialsShallowCopy()
    {
      return this.PasswordProviderCopy();
    }

    public string Username
    {
      get
      {
        return SecureStringPasswordProvider.DecryptSecureString(this.encryptedLogin);
      }
    }

    public string Password
    {
      get
      {
        return SecureStringPasswordProvider.DecryptSecureString(this.encryptedPassword);
      }
    }
    #endregion IWebApiCredentials
  }
}

