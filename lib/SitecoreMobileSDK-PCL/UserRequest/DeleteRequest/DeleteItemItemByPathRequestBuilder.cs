namespace Sitecore.MobileSDK.UserRequest.DeleteRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.Validators;

  public class DeleteItemItemByPathRequestBuilder : AbstractDeleteItemRequestBuilder<IDeleteItemsByPathRequest>
  {
    private string itemPath;

    public DeleteItemItemByPathRequestBuilder(string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath, this.GetType().Name + ".ItemPath");
      this.itemPath = itemPath;
    }

    public override IDeleteItemsByPathRequest Build()
    {
      return new DeleteItemByPathParameters(null, this.database, this.itemPath);
    }
  }
}
