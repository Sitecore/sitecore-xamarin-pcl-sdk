namespace Sitecore.MobileSDK.UrlBuilder.RenderingHTML
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class RenderingHTMLUrlBuilder : AbstractGetItemUrlBuilder<IGetRenderingHtmlRequest>
  {
    public RenderingHTMLUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    public override string GetUrlForRequest(IGetRenderingHtmlRequest request)
    {
      this.ValidateRequest(request);

      string hostUrl = this.GetHostUrlForRequest(request)
            + this.restGrammar.PathComponentSeparator
            + this.webApiGrammar.ItemWebApiActionsEndpoint
            + this.webApiGrammar.ItemWebApiGetRenderingAction;

      string specificParameters = this.GetSpecificPartForRequest(request);

      string allParameters = null;

      if (!string.IsNullOrEmpty(specificParameters))
      {
        allParameters =
          allParameters +
          this.restGrammar.FieldSeparator + specificParameters;
      }

      bool shouldTruncateBaseUrl = (!string.IsNullOrEmpty(allParameters) && allParameters.StartsWith(this.restGrammar.FieldSeparator));
      while (shouldTruncateBaseUrl)
      {
        allParameters = allParameters.Substring(1);
        shouldTruncateBaseUrl = (!string.IsNullOrEmpty(allParameters) && allParameters.StartsWith(this.restGrammar.FieldSeparator));
      }


      string result = hostUrl;

      if (!string.IsNullOrEmpty(allParameters))
      {
        result =
          result +
          this.restGrammar.HostAndArgsSeparator + allParameters;
      }

      return result;
    }

    protected override string GetSpecificPartForRequest(IGetRenderingHtmlRequest request)
    {
      string escapedSourceId = UrlBuilderUtils.EscapeDataString(request.SourceId);
      string escapedRenderingId = UrlBuilderUtils.EscapeDataString(request.RenderingId);
      string result = this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator + escapedSourceId
        + restGrammar.FieldSeparator 
        + this.webApiGrammar.RenderingIdParameterName + this.restGrammar.KeyValuePairSeparator + escapedRenderingId;

      return result.ToLowerInvariant();
    }

    protected override void ValidateSpecificRequest(IGetRenderingHtmlRequest request)
    {
      ItemIdValidator.ValidateItemId(request.SourceId, this.GetType().Name + ".SourceId");
      ItemIdValidator.ValidateItemId(request.RenderingId, this.GetType().Name + ".RenderingId");
    }
  }
}
