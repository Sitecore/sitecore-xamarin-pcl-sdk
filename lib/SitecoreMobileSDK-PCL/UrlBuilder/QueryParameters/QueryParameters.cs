namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
  using System.Linq;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class QueryParameters : IQueryParameters
  {
    public QueryParameters(IEnumerable<string> fields)
    {
      this.Fields = fields;
    }

    public virtual IQueryParameters DeepCopy()
    {
      string[] fields = null;
      if (null != this.Fields)
      {
        fields = this.Fields.ToArray();
      }

      return new QueryParameters(fields);
    }

    public IEnumerable<string> Fields { get; private set; }
  }
}
