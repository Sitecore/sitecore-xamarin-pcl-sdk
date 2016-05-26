namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class DeleteItemByIdUrlBuilder : AbstractDeleteItemUrlBuilder<IDeleteItemsByIdRequest>
  {
    public DeleteItemByIdUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }
  
    public override string GetUrlForRequest(IDeleteItemsByIdRequest request)
    {
      this.Validate(request);

      string hostUrl = base.GetBaseUrlForRequest(request);
      string itemId = UrlBuilderUtils.EscapeDataString(request.ItemId.ToLowerInvariant());

      string result = hostUrl + this.RestGrammar.PathComponentSeparator + itemId;

      string parameters = this.GetParametersString(request);

      if (!string.IsNullOrEmpty(parameters))
      {
        result += this.RestGrammar.HostAndArgsSeparator + parameters;
      }

      return result.ToLowerInvariant();
    }

    public override void ValidateSpecificPart(IDeleteItemsByIdRequest request)
    {
      ItemIdValidator.ValidateItemId(request.ItemId, this.GetType().Name + ".ItemId");
    }

  }
}
