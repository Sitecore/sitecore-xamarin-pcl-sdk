namespace Sitecore.MobileSDK.UserRequest.DeleteRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;
  using Sitecore.MobileSDK.Validators;

  public class DeleteItemByIdRequestBuilder : AbstractDeleteItemRequestBuilder<IDeleteItemsByIdRequest>
  {
    private string itemId;

    public DeleteItemByIdRequestBuilder(string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId, this.GetType().Name + ".ItemId");
      this.itemId = itemId;
    }

    public override IDeleteItemsByIdRequest Build()
    {
      return new DeleteItemByIdParameters(null, this.scopeParameters, this.database, this.itemId);
    }
  }
}
