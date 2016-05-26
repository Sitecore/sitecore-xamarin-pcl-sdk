namespace Sitecore.MobileSDK.API.Session
{
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.PasswordProvider;
  using Sitecore.MobileSDK.PasswordProvider.Interface;

  /// <summary>
  /// Interface represents builder for authenticated session. 
  /// </summary>
  public interface IAuthenticatedSessionBuilder
  {
    /// <summary>
    /// Specifies user's credantials.
    /// </summary>
    /// <param name="credentials">A data provider for user's credentials <seealso cref="ISSCCredentials" /></param>
    /// <returns>
    ///   <seealso cref="IItemSource" />
    /// </returns>
    IBaseSessionBuilder Credentials(ISSCCredentials credentials);
  }
}
