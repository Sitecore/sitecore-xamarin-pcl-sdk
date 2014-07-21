namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  public class DeleteItemByPathRequestBuilder : AbstractBaseDeleteRequestBuilder<IDeleteItemsByPathRequest>
  {
    private string itemPath;

    public DeleteItemByPathRequestBuilder(string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath);
      this.itemPath = itemPath;
    }

    public override IDeleteItemsByPathRequest Build()
    {
      return new DeleteItemByPathParameters(null, this.scopeParameters, this.database, this.itemPath);
    }
  }
}
