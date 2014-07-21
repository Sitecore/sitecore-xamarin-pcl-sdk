namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  public class DeleteItemByIdRequestBuilder : AbstractDeleteItemRequestBuilder<IDeleteItemsByIdRequest>
  {
    private string itemId;

    public DeleteItemByIdRequestBuilder(string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId);
      this.itemId = itemId;
    }

    public override IDeleteItemsByIdRequest Build()
    {
      return new DeleteItemByIdParameters(null, scopeParameters, database, this.itemId);
    }
  }
}
