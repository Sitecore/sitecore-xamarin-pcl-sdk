namespace Sitecore.MobileSDK.UrlBuilder
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;

  public class QueryParametersUrlBuilder
  {
    public QueryParametersUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
    {
      this.restGrammar = restGrammar;
      this.sscGrammar = sscGrammar;
    }

    public string BuildUrlString(IQueryParameters queryParameters)
    {
      if (null == queryParameters)
      {
        return null;
      }

      string result = string.Empty;

      bool isCollectionValid = (null != queryParameters.Fields);
      if (!isCollectionValid)
      {
        // @adk : avoiding null pointer exception from IEnumerable.Any()
        return result;
      }

      bool isCollectionEmpty = (!queryParameters.Fields.Any());
      bool isFieldsAvailable = isCollectionValid && !isCollectionEmpty;
      if (!isFieldsAvailable)
      {
        return result;
      }

      string fieldsStatement = this.GetFieldsStatementFromCollection(queryParameters.Fields);

      //FIXME: @igk extract or refactor to do not use dafault fields
      string defaultFieldsList = "%2CItemID%2CItemName%2CItemPath%2CParentID%2CTemplateID%2CTemplateName%2CItemLanguage%2CItemVersion%2CDisplayName";

      if (null != fieldsStatement)
      {
        if (!string.IsNullOrEmpty(result))
        {
          result += this.restGrammar.FieldSeparator;
        }
        result += fieldsStatement + defaultFieldsList;
      }

      return result.ToLowerInvariant();
    }

    private string GetFieldsStatementFromCollection(IEnumerable<string> fields)
    {
      string result = this.sscGrammar.FieldsListParameterName + this.restGrammar.KeyValuePairSeparator;

      IRestServiceGrammar restGrammar = this.restGrammar;

      Func<string, string> fieldTransformerFunc = (currentField) =>
      {
        string escapedField = UrlBuilderUtils.EscapeDataString(currentField);
        return restGrammar.ItemFieldSeparator + escapedField;
      };
      var fieldsWithSeparators = fields.Select(fieldTransformerFunc);

      string strFieldsList = string.Concat(fieldsWithSeparators);
      string strFieldsListWithoutLeadingSeparator = strFieldsList.Remove(0, 1);

      result += strFieldsListWithoutLeadingSeparator;

      return result;
    }

    private IRestServiceGrammar restGrammar;
    private ISSCUrlParameters sscGrammar;
  }
}
