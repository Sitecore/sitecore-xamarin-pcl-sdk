namespace Sitecore.MobileSDK.API.Session
{
  /// <summary>
  /// Interface represents builder for Authenticated session. 
  /// </summary>
  public interface IAuthenticatedSessionBuilder
  {
    /// <summary>
    /// Specifies user's credantials.
    /// </summary>
    /// <param name="credentials">A data provider for user's credentials <see cref="IWebApiCredentials"/></param>
    /// <returns><see cref="IItemSource"/></returns>
    IBaseSessionBuilder Credentials(IWebApiCredentials credentials);
  }
}
