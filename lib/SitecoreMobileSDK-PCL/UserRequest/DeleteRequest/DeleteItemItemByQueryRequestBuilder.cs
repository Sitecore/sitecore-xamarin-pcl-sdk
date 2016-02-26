namespace Sitecore.MobileSDK.UserRequest.DeleteRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.Validators;

  class DeleteItemItemByQueryRequestBuilder : AbstractDeleteItemRequestBuilder<IDeleteItemsByQueryRequest>
  {
    private readonly string sitecoreQuery;

    public DeleteItemItemByQueryRequestBuilder(string sitecoreQuery)
    {
      SitecoreQueryValidator.ValidateSitecoreQuery(sitecoreQuery, this.GetType().Name + ".SitecoreQuery");
      this.sitecoreQuery = sitecoreQuery;
    }

    public override IDeleteItemsByQueryRequest Build()
    {
      return new DeleteItemByQueryParameters(null, this.database, this.sitecoreQuery);
    }
  }
}
