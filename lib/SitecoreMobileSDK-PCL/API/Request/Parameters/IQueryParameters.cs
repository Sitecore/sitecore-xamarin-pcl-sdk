namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System.Collections.Generic;

  public interface IQueryParameters
  {
    IQueryParameters DeepCopy();

    IScopeParameters ScopeParameters { get; }
    PayloadType? Payload { get; }
    IEnumerable<string> Fields { get; }
  }
}
