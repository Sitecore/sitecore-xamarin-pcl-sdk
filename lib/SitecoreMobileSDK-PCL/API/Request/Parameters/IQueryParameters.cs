
namespace Sitecore.MobileSDK.API.Request.Parameters
{
    using System.Collections.Generic;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    public interface IQueryParameters
  {
    IQueryParameters DeepCopy();

    IScopeParameters ScopeParameters{ get; }
    PayloadType? Payload { get; }
    ICollection<string> Fields {get;}
  }
}
