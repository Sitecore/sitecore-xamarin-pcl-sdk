namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;

  public class DeleteItemsByIdUrlBuilder : AbstractDeleteItemsUrlBuilder<IDeleteItemsByIdRequest>
  {
    public DeleteItemsByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    public override string GetUrlForRequest(IDeleteItemsByIdRequest request)
    {
      var baseUrl = base.GetUrlForRequest(request);
      
      string escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId);

      string fullUrl = baseUrl
                       + this.restGrammar.HostAndArgsSeparator
                       + this.webApiGrammar.ItemIdParameterName
                       + this.restGrammar.KeyValuePairSeparator
                       + escapedId;

      if (!string.IsNullOrEmpty(this.GetParametersString(request)))
      {
        fullUrl += this.restGrammar.FieldSeparator 
          + this.GetParametersString(request);
      }

      return fullUrl;
    }

    public override void ValidateSpecificPart(IDeleteItemsByIdRequest request)
    {
      ItemIdValidator.ValidateItemId(request.ItemId);
    }

  }
}
