namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public interface IBaseDeleteItemRequest
  {
    ISessionConfig SessionConfig { get; }
    IScopeParameters ScopeParameters { get; }

    string Database { get; }
  }
}