namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  class DeleteItemByQueryRequestBuilder : AbstractBaseDeleteRequestBuilder<IDeleteItemsByQueryRequest>
  {
   private readonly string sitecoreQuery;

   public DeleteItemByQueryRequestBuilder(string query)
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
