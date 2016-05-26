namespace Sitecore.MobileSDK.API
{
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.Session;

  /// <summary>
  /// Constructs all kinds of session objects
  /// * Anonymous session
  /// * Authenticated session
  /// * Readonly session
  /// * A session capable of modifying the content
  /// </summary>
  public static class SitecoreSSCSessionBuilder
  {
    /// <summary>
    /// Creates a session for the anonymous user. It does not require any credentials.
    /// </summary>
    /// <param name="instanceUrl">URL of the Sitecore instance, must starts with "http://" or "https://" prefix.</param>
    /// <returns>A builder to initialize an anonymous session.</returns>
    public static IAnonymousSessionBuilder AnonymousSessionWithHost(string instanceUrl)
    {
      var result = SessionBuilder.SessionBuilderWithHost(instanceUrl);
      return result;
    }

    /// <summary>
    /// Creates an authenticated session.
    /// </summary>
    /// <param name="instanceUrl">URL of the Sitecore instance, must starts with "http://" or "https://" prefix.</param>
    /// <returns>A builder to set user's credentials.</returns>
    public static IAuthenticatedSessionBuilder AuthenticatedSessionWithHost(string instanceUrl)
    {
      var result = SessionBuilder.SessionBuilderWithHost(instanceUrl);
      return result;
    }
  }
}

