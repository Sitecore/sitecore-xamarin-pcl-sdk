namespace Sitecore.MobileSDK.UserRequest.DeleteRequest
{
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;

  class DeleteItemItemByQueryRequestBuilder : AbstractDeleteItemRequestBuilder<IDeleteItemsByQueryRequest>
  {
    private readonly string sitecoreQuery;

    public DeleteItemItemByQueryRequestBuilder(string query)
    {
      SitecoreQueryValidator.ValidateSitecoreQuery(query);
      this.sitecoreQuery = query;
    }

    public override IDeleteItemsByQueryRequest Build()
    {
      return new DeleteItemByQueryParameters(null, this.scopeParameters, this.database, this.sitecoreQuery);
    }
  }
}
