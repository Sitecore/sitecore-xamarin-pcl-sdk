namespace Sitecore.MobileSDK.API.Session
{
    using Sitecore.MobileSDK.Session;
    using Sitecore.MobileSDK.SessionSettings;

    public interface IAuthenticatedSessionBuilder
  {
    IBaseSessionBuilder Credentials(IWebApiCredentials credentials);
  }
}

