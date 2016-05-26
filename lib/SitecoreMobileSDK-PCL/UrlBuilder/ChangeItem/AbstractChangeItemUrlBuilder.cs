namespace Sitecore.MobileSDK.UrlBuilder.ChangeItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;

  public abstract class AbstractChangeItemUrlBuilder<TRequest> : AbstractGetItemUrlBuilder<TRequest>
    where TRequest : IBaseChangeItemRequest
  {
    public AbstractChangeItemUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }

    public string GetFieldValuesList(TRequest request)
    {
      string result = string.Empty;

      bool fieldsAvailable = (null != request.FieldsRawValuesByName);
      if (fieldsAvailable)
      {
        fieldsAvailable = (request.FieldsRawValuesByName.Count > 0);
      }

      if (fieldsAvailable)
      {
        foreach (var fieldElem in request.FieldsRawValuesByName)
        {
          string escapedFieldName = UrlBuilderUtils.EscapeDataString(fieldElem.Key);
          string escapedFieldValue = UrlBuilderUtils.EscapeDataString(fieldElem.Value);
          result += escapedFieldName
            + this.restGrammar.KeyValuePairSeparator
            + escapedFieldValue
            + this.restGrammar.FieldSeparator;
        }
        result = result.Remove(result.Length - 1);
      }

      return result;
    }

  }
}



