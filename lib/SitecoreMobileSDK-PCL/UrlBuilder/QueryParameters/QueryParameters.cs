
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

    public IQueryParameters DeepCopy()
    {
      return new QueryParameters(this.Payload, this.Fields);
    }

    public PayloadType? Payload { get; private set; }
    public ICollection<string> Fields { get; private set; }
  }
}
