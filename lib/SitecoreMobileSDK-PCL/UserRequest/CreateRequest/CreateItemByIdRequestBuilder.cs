namespace Sitecore.MobileSDK.UserRequest.CreateRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class CreateItemByIdRequestBuilder : AbstractCreateItemRequestBuilder<ICreateItemByIdRequest>
  {
    private readonly string itemId;

    public CreateItemByIdRequestBuilder(string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId, this.GetType().Name + ".ItemId");

      this.itemId = itemId;
    }

    public override ICreateItemByIdRequest Build()
    {
      BaseValidator.CheckForNullAndEmptyOrThrow(this.itemParametersAccumulator.ItemName, this.GetType().Name + ".ItemName");

      BaseValidator.CheckForNullAndEmptyOrThrow(this.itemParametersAccumulator.ItemTemplate, this.GetType().Name + ".ItemTemplate");

      CreateItemByIdParameters result = new CreateItemByIdParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemParametersAccumulator, this.itemId);
      return result;
    }
  }
}

