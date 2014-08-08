namespace Sitecore.MobileSDK.UserRequest.ReadRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class ReadItemByIdRequestBuilder : AbstractGetVersionedItemRequestBuilder<IReadItemsByIdRequest>
  {
    public ReadItemByIdRequestBuilder(string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId, this.GetType().Name + ".ItemId");

      this.itemId = itemId;
    }

    public override IReadItemsByIdRequest Build()
    {
      ReadItemsByIdParameters result = new ReadItemsByIdParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemId);
      return result;
    }

    private readonly string itemId;
  }
}

