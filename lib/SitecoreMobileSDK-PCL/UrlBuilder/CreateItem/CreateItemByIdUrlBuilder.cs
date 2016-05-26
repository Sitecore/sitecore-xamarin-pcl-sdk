namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Validators;

  public class CreateItemByIdUrlBuilder : AbstractCreateItemUrlBuilder<ICreateItemByIdRequest>
  {
    public CreateItemByIdUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }

    protected override string GetSpecificPartForRequest(ICreateItemByIdRequest request)
    {
      string escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId).ToLowerInvariant();

      //string result = base.GetSpecificPartForRequest(request);
      string result = this.restGrammar.FieldSeparator
        + this.sscGrammar.ItemIdParameterName
        + this.restGrammar.KeyValuePairSeparator
        + escapedId;

      return result;
    }

    protected override void ValidateSpecificRequest(ICreateItemByIdRequest request)
    {
      ItemIdValidator.ValidateItemId(request.ItemId, this.GetType().Name + ".ItemId");
    }
  }
}

