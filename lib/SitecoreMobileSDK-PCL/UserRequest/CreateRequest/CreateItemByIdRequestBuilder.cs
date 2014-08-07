namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Validators;

    public class CreateItemByIdRequestBuilder : AbstractCreateItemRequestBuilder<ICreateItemByIdRequest>
  {
    public CreateItemByIdRequestBuilder (string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId, this.GetType().Name + ".ItemId");

      this.itemId = itemId;
    }

    public override ICreateItemByIdRequest Build()
    {
      BaseValidator.CheckNullAndThrow(this.itemParametersAccumulator.ItemName, this.GetType().Name + ".ItemName");

      BaseValidator.CheckNullAndThrow(this.itemParametersAccumulator.ItemTemplate, this.GetType().Name + ".ItemTemplate");

      CreateItemByIdParameters result = new CreateItemByIdParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemParametersAccumulator, this.itemId);
      return result;
    }

    private readonly string itemId;
  }
}

