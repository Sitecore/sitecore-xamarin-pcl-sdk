
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
      string[] fields = null;
      if (null != this.Fields)
      {
        fields = new string[this.Fields.Count];
        this.Fields.CopyTo(fields, 0);
      }

      return new QueryParameters(this.Payload, fields);
    }

    public PayloadType? Payload { get; private set; }
    public ICollection<string> Fields { get; private set; }
  }
}
