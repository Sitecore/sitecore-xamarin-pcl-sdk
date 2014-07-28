namespace Sitecore.MobileSDK.UserRequest.DeleteRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;
  using Sitecore.MobileSDK.Validators;

  class DeleteItemItemByQueryRequestBuilder : AbstractDeleteItemRequestBuilder<IDeleteItemsByQueryRequest>
  {
    private readonly string sitecoreQuery;

    public DeleteItemItemByQueryRequestBuilder(string sitecoreQuery)
    {
      SitecoreQueryValidator.ValidateSitecoreQuery(sitecoreQuery, this.GetType().Name + ".sitecoreQuery");
      this.sitecoreQuery = sitecoreQuery;
    }

    public override IDeleteItemsByQueryRequest Build()
    {
      return new DeleteItemByQueryParameters(null, this.scopeParameters, this.database, this.sitecoreQuery);
    }
  }
}
