namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;

  public class DeleteItemsByQueryUrlBuilder : AbstractDeleteItemsUrlBuilder<IDeleteItemsByQueryRequest>
  {
    public DeleteItemsByQueryUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar) : base(restGrammar, webApiGrammar)
    {
    }

    public override void ValidateSpecificPart(IDeleteItemsByQueryRequest request)
    {
      SitecoreQueryValidator.ValidateSitecoreQuery(request.SitecoreQuery);
    }

    public override string GetUrlForRequest(IDeleteItemsByQueryRequest request)
    {
      this.Validate(request);

      var baseUrl = base.GetBaseUrlForRequest(request);
      string escapedQuery = UrlBuilderUtils.EscapeDataString(request.SitecoreQuery);

      string fullUrl = baseUrl
                       + this.restGrammar.HostAndArgsSeparator
                       + this.webApiGrammar.SitecoreQueryParameterName
                       + this.restGrammar.KeyValuePairSeparator;

      fullUrl = fullUrl.ToLowerInvariant() + escapedQuery;

      if (!string.IsNullOrEmpty(this.GetParametersString(request)))
      {
        var additionalParams = this.restGrammar.FieldSeparator
                               + this.GetParametersString(request);
        fullUrl += additionalParams.ToLowerInvariant();
      }

      return fullUrl;
    }
  }
}
