namespace Sitecore.MobileSDK.API.Session
{
  using Sitecore.MobileSDK.API.Items;

  /// <summary>
  /// Interface represents builder for authenticated session. 
  /// </summary>
  public interface IAuthenticatedSessionBuilder
  {
    /// <summary>
    /// Specifies user's credantials.
    /// </summary>
    /// <param name="credentials">A data provider for user's credentials <seealso cref="IWebApiCredentials" /></param>
    /// <returns>
    ///   <seealso cref="IItemSource" />
    /// </returns>
    IBaseSessionBuilder Credentials(IWebApiCredentials credentials);
  }
}
