namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  class DeleteItemsByPathUrlBuilder : AbstractDeleteItemsUrlBuilder<IDeleteItemsByPathRequest>
  {
    public DeleteItemsByPathUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base(restGrammar, webApiGrammar)
    {
    }

    public override string GetUrlForRequest(IDeleteItemsByPathRequest request)
    {
      return 
        base.GetUrlForRequest(request)
        + request.Itempath
        + this.restGrammar.HostAndArgsSeparator
        + this.GetParametersString(request);
    }

    public override void ValidateSpecificPart(IDeleteItemsByPathRequest request)
    {
      ItemPathValidator.ValidateItemPath(request.Itempath);
    }
  }
}
