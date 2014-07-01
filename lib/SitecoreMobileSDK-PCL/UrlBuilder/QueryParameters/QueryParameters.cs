
namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
  using System.Collections.Generic;

  public class QueryParameters : IQueryParameters
  {
    public QueryParameters(PayloadType? payload, ScopeParameters scopeParameters, ICollection<string> fields)
    {
      this.Payload         = payload;
      this.ScopeParameters = scopeParameters;
		  this.Fields          = fields;
    }

    public virtual IQueryParameters DeepCopy()
    {
      string[] fields = null;
      if (null != this.Fields)
      {
        fields = new string[this.Fields.Count];
        this.Fields.CopyTo(fields, 0);
      }

      ScopeParameters scopeParameters = this.ScopeParameters.ShallowCopy();

      return new QueryParameters(this.Payload, scopeParameters, fields);
    }

    public ScopeParameters ScopeParameters { get; private set; }
    public PayloadType? Payload { get; private set; }
    public ICollection<string> Fields { get; private set; }
  }
}
