
namespace Sitecore.MobileSDK.API
{
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.Session;


  /** Constructs all kinds of session objects 
    * Anonymous session
    * Authenticated session
    * Readonly session
    * A session capable of modifying the content
  */
  public static class SitecoreWebApiSessionBuilder
  {
    /** Creates a session for the anonymous user. It does not require any credentials.   
    *  @param instanceUrl URL of the Sitecore instance.
    *  @return A builder to initialize an anonymous session.
    */
    public static IAnonymousSessionBuilder AnonymousSessionWithHost(string instanceUrl)
    {
      var result = SessionBuilder.SessionBuilderWithHost(instanceUrl);
      return result;
    }


    /** Creates an authenticated session.
    * 
    * @param instanceUrl URL of the Sitecore instance.
    * @return A builder to set user's credentials.
    */
    public static IAuthenticatedSessionBuilder AuthenticatedSessionWithHost(string instanceUrl)
    {
      var result = SessionBuilder.SessionBuilderWithHost(instanceUrl);
      return result;
    }
  }
}

