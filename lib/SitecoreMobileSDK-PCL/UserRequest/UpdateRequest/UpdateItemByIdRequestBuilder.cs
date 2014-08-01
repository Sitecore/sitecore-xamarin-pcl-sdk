namespace Sitecore.MobileSDK.UserRequest.UpdateRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Validators;

  public class UpdateItemByIdRequestBuilder : AbstractUpdateItemRequestBuilder<IUpdateItemByIdRequest>
  {
    public UpdateItemByIdRequestBuilder (string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId, this.GetType().Name + ".ItemId");

      this.itemId = itemId;
    }

    public override IUpdateItemByIdRequest Build()
    {
      UpdateItemByIdParameters result = new UpdateItemByIdParameters(null, this.itemSourceAccumulator, this.queryParameters, this.FieldsRawValuesByName, this.itemId);
      return result;
    }

    private readonly string itemId;
  }
}
