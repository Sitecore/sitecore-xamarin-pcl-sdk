namespace Sitecore.MobileSDK.UrlBuilder
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  class ScopeParametersUrlBuilder
  {
    private IRestServiceGrammar restGrammar;
    private IWebApiUrlParameters webApiGrammar;

    public ScopeParametersUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
    {
      this.restGrammar = restGrammar;
      this.webApiGrammar = webApiGrammar;
    }

    public string ScopeToRestArgumentStatement(IScopeParameters scopeParameters)
    {
      if (null == scopeParameters)
      {
        return string.Empty;
      }

      string scopeString = string.Empty;

      foreach (ScopeType singleScopeEntry in scopeParameters.OrderedScopeSequence)
      {
        string urlParameterForScope = this.ScopeTypeToString(singleScopeEntry);
        scopeString += this.restGrammar.ItemFieldSeparator + urlParameterForScope;
      }

      if (string.IsNullOrEmpty(scopeString))
      {
        return string.Empty;
      }

      scopeString = scopeString.Substring(1);

      string result = this.webApiGrammar.ScopeParameterName + this.restGrammar.KeyValuePairSeparator + scopeString;

      return result;
    }

    private string ScopeTypeToString(ScopeType scope)
    {
      var enumNamesMap = new Dictionary<ScopeType, string>();
      enumNamesMap.Add(ScopeType.Parent, "p");
      enumNamesMap.Add(ScopeType.Self, "s");
      enumNamesMap.Add(ScopeType.Children, "c");

      string result = enumNamesMap[scope];
      return result;
    }
  }
}
