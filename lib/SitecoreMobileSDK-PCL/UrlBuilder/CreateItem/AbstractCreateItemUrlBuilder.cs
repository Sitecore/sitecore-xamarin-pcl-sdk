namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.ChangeItem;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;

  public abstract class AbstractCreateItemUrlBuilder<TRequest> : AbstractChangeItemUrlBuilder<TRequest>
    where TRequest : IBaseCreateItemRequest
  {
    public AbstractCreateItemUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }

    //TODO: IGK probable we do not need this class at all for now
    protected override string GetSpecificPartForRequest(TRequest request)
    {
      return "";

//      string escapedTemplate = UrlBuilderUtils.EscapeDataString(request.ItemTemplateId).ToLowerInvariant();
//      string escapedName = UrlBuilderUtils.EscapeDataString(request.ItemName);
//
//      string result =
//        this.sscGrammar.TemplateParameterName
//        + this.restGrammar.KeyValuePairSeparator
//        + escapedTemplate;
//
//
//      if (!string.IsNullOrEmpty(escapedName))
//      {
//        result = result
//          + this.restGrammar.FieldSeparator
//          + this.sscGrammar.ItemNameParameterName
//          + this.restGrammar.KeyValuePairSeparator
//          + escapedName;
//      }
//
//      return result;
    }
  }
}

