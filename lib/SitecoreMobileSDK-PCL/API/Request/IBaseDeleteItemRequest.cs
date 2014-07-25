namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface IBaseDeleteItemRequest
  {
    ISessionConfig SessionConfig { get; }
    IScopeParameters ScopeParameters { get; }
    string Database { get; }
  }
}