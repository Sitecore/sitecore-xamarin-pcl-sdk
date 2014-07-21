namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;

  public class DeleteItemByIdUrlBuilder : AbstractDeleteItemUrlBuilder<IDeleteItemsByIdRequest>
  {
    public DeleteItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    public override string GetUrlForRequest(IDeleteItemsByIdRequest request)
    {
      this.Validate(request);

      var baseUrl = base.GetBaseUrlForRequest(request);
      var escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId);

      var fullUrl = baseUrl
                       + this.RestGrammar.HostAndArgsSeparator
                       + this.WebApiGrammar.ItemIdParameterName
                       + this.RestGrammar.KeyValuePairSeparator
                       + escapedId;

      if (!string.IsNullOrEmpty(this.GetParametersString(request)))
      {
        fullUrl += this.RestGrammar.FieldSeparator
          + this.GetParametersString(request);
      }

      return fullUrl.ToLowerInvariant();
    }

    public override void ValidateSpecificPart(IDeleteItemsByIdRequest request)
    {
      ItemIdValidator.ValidateItemId(request.ItemId);
    }

  }
}
