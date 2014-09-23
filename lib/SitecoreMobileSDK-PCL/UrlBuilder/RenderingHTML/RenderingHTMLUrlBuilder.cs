namespace Sitecore.MobileSDK.UrlBuilder.ItemById
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
