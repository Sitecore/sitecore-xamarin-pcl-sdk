﻿

namespace Sitecore.MobileSDK.UrlBuilder
{
  using System;
  using System.Linq;
  using System.Collections.Generic;

  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  public class QueryParametersUrlBuilder
  {
    public QueryParametersUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
    {
      this.restGrammar = restGrammar;
      this.webApiGrammar = webApiGrammar;
    }

    public string BuildUrlString(IQueryParameters queryParameters)
    {
      if (null == queryParameters)
      {
        return null;
      }

      string result = string.Empty;

      string payloadStatement = this.PayloadTypeToRestArgumentStatement( queryParameters.Payload );
      result = payloadStatement.ToLowerInvariant();


      bool isCollectionValid = ( null != queryParameters.Fields );
      if ( !isCollectionValid )
      {
        // @adk : avoiding null pointer exception from IEnumerable.Any()
        return result;
      }

      bool isCollectionEmpty = ( !queryParameters.Fields.Any() );
      bool isFieldsAvailable = isCollectionValid && !isCollectionEmpty;
      if ( !isFieldsAvailable )
      {
        return result;
      }

      string fieldsStatement = this.GetFieldsStatementFromCollection( queryParameters.Fields );
      if ( null != fieldsStatement )
      {
        result += this.restGrammar.FieldSeparator + fieldsStatement;
      }

      return result.ToLowerInvariant();
    }

    private string GetFieldsStatementFromCollection( ICollection<string> fields )
    {
      string result = this.webApiGrammar.FieldsListParameterName + this.restGrammar.KeyValuePairSeparator;

      IRestServiceGrammar restGrammar = this.restGrammar;

      Func<string, string> fieldTransformerFunc = (currentField) =>
      {
        string escapedField = UrlBuilderUtils.EscapeDataString( currentField );
        return restGrammar.ItemFieldSeparator + escapedField;
      };
      var fieldsWithSeparators = fields.Select( fieldTransformerFunc );

      string strFieldsList = string.Concat( fieldsWithSeparators );
      string strFieldsListWithoutLeadingSeparator = strFieldsList.Remove( 0, 1 );

      result += strFieldsListWithoutLeadingSeparator;

      return result;
    }

    private string PayloadTypeToRestArgumentStatement(PayloadType? payload)
    {
      string strPayload = this.PayloadTypeToString( payload );
      string result = string.Empty;

      if (null == strPayload)
      {
        return result;
      }

      result = this.webApiGrammar.PayloadParameterName + this.restGrammar.KeyValuePairSeparator + strPayload;

      return result;
    }

    private string PayloadTypeToString(PayloadType? payload)
    {
      if (null == payload)
      {
        return null;
      }

      var enumNamesMap = new Dictionary<PayloadType?, string>();
      enumNamesMap.Add( PayloadType.Content, "content" );
      enumNamesMap.Add( PayloadType.Full   , "full"    );
      enumNamesMap.Add( PayloadType.Min    , "min"     );

      string result = enumNamesMap[payload];
      return result;
    }

    private string ScopeToRestArgumentStatement(ScopeParameters scopeParameters)
    {
      if (null == scopeParameters)
      {
        return string.Empty;
      }

      string scopeString = string.Empty;

      if (scopeParameters.ParentScopeIsSet)
      {
        scopeString += this.restGrammar.ItemFieldSeparator + "p";
      }

      if (scopeParameters.SelfScopeIsSet)
      {
        scopeString += this.restGrammar.ItemFieldSeparator + "s";
      }

      if (scopeParameters.ChildrenScopeIsSet)
      {
        scopeString += this.restGrammar.ItemFieldSeparator + "c";
      }

      if (string.IsNullOrEmpty(scopeString))
      {
        return string.Empty;
      }

      scopeString = scopeString.Substring(1);

      string result = this.webApiGrammar.ScopeParameterName + this.restGrammar.KeyValuePairSeparator + scopeString;

      return result;
    }

    private IRestServiceGrammar restGrammar;
    private IWebApiUrlParameters webApiGrammar;
  }
}
