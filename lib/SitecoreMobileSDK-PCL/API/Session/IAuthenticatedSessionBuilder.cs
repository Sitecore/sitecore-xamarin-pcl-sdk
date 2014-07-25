namespace Sitecore.MobileSDK.API.Session
{
  public interface IAuthenticatedSessionBuilder
  {
    IBaseSessionBuilder Credentials(IWebApiCredentials credentials);
  }
}

