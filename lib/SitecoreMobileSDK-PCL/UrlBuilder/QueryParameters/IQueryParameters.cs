
namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
  using System.Collections.Generic;

  public interface IQueryParameters
  {
    PayloadType? Payload { get; }
    ICollection<string> Fields {get;}
  }
}
