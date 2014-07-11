

namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;

  public class CreateItemByIdUrlBuilder : AbstractGetItemUrlBuilder<ICreateItemByIdRequest>
  {
    public CreateItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base( restGrammar, webApiGrammar )
    {
    }

    protected override string GetSpecificPartForRequest(ICreateItemByIdRequest request)
    {
      string escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId).ToLowerInvariant();
      string escapedTemplate = UrlBuilderUtils.EscapeDataString(request.CreateParameters.ItemTemplate).ToLowerInvariant();
      string escapedName = UrlBuilderUtils.EscapeDataString(request.CreateParameters.ItemName);

      string result = this.webApiGrammar.ItemIdParameterName 
        + this.restGrammar.KeyValuePairSeparator 
        + escapedId
        + this.restGrammar.FieldSeparator 
        + this.webApiGrammar.TemplateParameterName 
        + this.restGrammar.KeyValuePairSeparator 
        + escapedTemplate
        + this.restGrammar.FieldSeparator 
        + this.webApiGrammar.ItemNameParameterName
        + this.restGrammar.KeyValuePairSeparator 
        + escapedName;

      return result;
    }

    public string GetFieldValuesList(ICreateItemByIdRequest request)
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

    protected override void ValidateSpecificRequest(ICreateItemByIdRequest request)
    {
      ItemIdValidator.ValidateItemId(request.ItemId);
    }
  }
}

