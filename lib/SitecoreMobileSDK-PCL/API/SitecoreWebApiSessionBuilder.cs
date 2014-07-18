
namespace Sitecore.MobileSDK.API
{
    using Sitecore.MobileSDK.API.Session;
    using Sitecore.MobileSDK.Session;

    public class SitecoreWebApiSessionBuilder
  {
    private SitecoreWebApiSessionBuilder()
    {
    }

    public static IAnonymousSessionBuilder AnonymousSessionWithHost(string instanceUrl)
    {
      var result = SessionBuilderImpl.SessionBuilderWithHost(instanceUrl);
      return result;
    }

    public static IAuthenticatedSessionBuilder AuthenticatedSessionWithHost(string instanceUrl)
    {
      var result = SessionBuilderImpl.SessionBuilderWithHost(instanceUrl);
      return result;
    }
  }
}

