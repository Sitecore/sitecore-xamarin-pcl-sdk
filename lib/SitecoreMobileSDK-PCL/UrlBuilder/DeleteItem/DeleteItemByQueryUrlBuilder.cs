namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class DeleteItemByQueryUrlBuilder : AbstractDeleteItemUrlBuilder<IDeleteItemsByQueryRequest>
  {
    public DeleteItemByQueryUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
      : base(restGrammar, sscGrammar)
    {
    }

    public override void ValidateSpecificPart(IDeleteItemsByQueryRequest request)
    {
      SitecoreQueryValidator.ValidateSitecoreQuery(request.SitecoreQuery, this.GetType().Name + ".SitecoreQuery");
    }

    public override string GetUrlForRequest(IDeleteItemsByQueryRequest request)
    {
      this.Validate(request);

      var baseUrl = base.GetBaseUrlForRequest(request);
      string escapedQuery = UrlBuilderUtils.EscapeDataString(request.SitecoreQuery);

      string fullUrl = baseUrl
                       + this.RestGrammar.HostAndArgsSeparator
                       + this.SSCGrammar.SitecoreQueryParameterName
                       + this.RestGrammar.KeyValuePairSeparator;

      fullUrl = fullUrl.ToLowerInvariant() + escapedQuery;

      if (!string.IsNullOrEmpty(this.GetParametersString(request)))
      {
        var additionalParams = this.RestGrammar.FieldSeparator
                               + this.GetParametersString(request);
        fullUrl += additionalParams.ToLowerInvariant();
      }

      return fullUrl;
    }
  }
}
