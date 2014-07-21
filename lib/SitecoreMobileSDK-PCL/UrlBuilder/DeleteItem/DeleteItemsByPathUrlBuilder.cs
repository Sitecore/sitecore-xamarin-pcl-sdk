namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;

  public class DeleteItemsByPathUrlBuilder : AbstractDeleteItemsUrlBuilder<IDeleteItemsByPathRequest>
  {
    public DeleteItemsByPathUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    public override string GetUrlForRequest(IDeleteItemsByPathRequest request)
    {
      this.Validate(request);

      var baseUrl = base.GetBaseUrlForRequest(request);
      string escapedPath = UrlBuilderUtils.EscapeDataString(request.ItemPath.ToLowerInvariant());

      var fullUrl = baseUrl + escapedPath;

      if (!string.IsNullOrEmpty(this.GetParametersString(request)))
      {
        fullUrl += this.restGrammar.HostAndArgsSeparator
          + this.GetParametersString(request);
      }

      return fullUrl.ToLowerInvariant();
    }

    public override void ValidateSpecificPart(IDeleteItemsByPathRequest request)
    {
      ItemPathValidator.ValidateItemPath(request.ItemPath);
    }
  }
}
