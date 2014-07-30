
namespace Sitecore.MobileSDK.UrlBuilder.UpdateItem
{
  using System;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  public abstract class AbstractUpdateItemUrlBuilder<TRequest> : AbstractGetItemUrlBuilder<TRequest>
    where TRequest : IBaseUpdateItemRequest
  {
    public AbstractUpdateItemUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base( restGrammar, webApiGrammar )
    {
    }

    public string GetFieldValuesList(TRequest request)
    {
      string result = string.Empty;

      bool fieldsAvailable = (null != request.CreateParameters.FieldsRawValuesByName);
      if (fieldsAvailable)
      {
        fieldsAvailable = (request.CreateParameters.FieldsRawValuesByName.Count > 0);
      }

      if (fieldsAvailable)
      {
        foreach (var fieldElem in request.CreateParameters.FieldsRawValuesByName)
        {  
          string escapedFieldName = UrlBuilderUtils.EscapeDataString (fieldElem.Key);
          string escapedFieldValue = UrlBuilderUtils.EscapeDataString (fieldElem.Value);
          result += escapedFieldName 
            + this.restGrammar.KeyValuePairSeparator 
            + escapedFieldValue 
            + this.restGrammar.FieldSeparator;
        }
        result = result.Remove (result.Length - 1);
      }

      return result;
    }

    //protected abstract string GetSpecificPartForRequest(TRequest request);
  }
}

