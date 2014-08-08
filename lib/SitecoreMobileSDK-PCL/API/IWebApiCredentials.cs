
namespace Sitecore.MobileSDK.API
{
  using System;


  /// <summary>
  /// A data provider for user's credentials.
  /// A secure implementation must be submitted by the user
  /// </summary>
  public interface IWebApiCredentials : IDisposable
  {

    /// <summary>
    /// Creates a data provider's copy.
    /// This method will be executed once the object is submitted to the session
    /// </summary>
    /// <returns>A copy of the current instance</returns>
    IWebApiCredentials CredentialsShallowCopy();

    /// <summary>
    /// Gets the username from the keychain.
    /// </summary>
    /// <value>
    /// The username to authenticate against the sitecore instance. It may contain the domain information.
    /// For example, "sitecore/admin"
    /// </value>
    string Username
    {
      get;
    }

    /// <summary>
    /// Gets the password from the keychain.
    /// </summary>
    /// <value>
    /// The password to authenticate against the sitecore instance.
    /// </value>
    string Password
    {
      get;
    }
  }
}
