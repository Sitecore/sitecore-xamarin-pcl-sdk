
namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
  using System.Collections.Generic;

  public class QueryParameters : IQueryParameters
  {
    public QueryParameters(PayloadType? payload, IScopeParameters scopeParameters, ICollection<string> fields)
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

      IScopeParameters scopeParameters = null;

      if (null != this.ScopeParameters)
      {
        scopeParameters = this.ScopeParameters.ShallowCopyScopeParametersInterface();
      }
      return new QueryParameters(this.Payload, scopeParameters, fields);
    }

    public IScopeParameters ScopeParameters { get; private set; }
    public PayloadType? Payload { get; private set; }
    public ICollection<string> Fields { get; private set; }
  }
}
