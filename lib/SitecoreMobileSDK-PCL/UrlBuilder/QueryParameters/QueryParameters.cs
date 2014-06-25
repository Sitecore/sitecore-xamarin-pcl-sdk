
namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
  using System.Collections.Generic;

  public class QueryParameters : IQueryParameters
  {
    public QueryParameters(PayloadType? payload, ICollection<string> fields)
    {
      this.Payload = payload;
      this.Fields = fields;
    }

    public PayloadType? Payload { get; private set; }
    public ICollection<string> Fields { get; private set; }
  }
}
