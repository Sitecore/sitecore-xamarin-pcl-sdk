namespace Sitecore.MobileSDK.UrlBuilder.QueryParameters
{
  using System.Linq;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class QueryParameters : IQueryParameters
  {
    public QueryParameters(
      PayloadType? payload,
      IScopeParameters scopeParameters,
      IEnumerable<string> fields)
    {
      this.Payload = payload;
      this.ScopeParameters = scopeParameters;
      this.Fields = fields;
    }

    public virtual IQueryParameters DeepCopy()
    {
      string[] fields = null;
      if (null != this.Fields)
      {
        fields = this.Fields.ToArray();
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
    public IEnumerable<string> Fields { get; private set; }
  }
}
