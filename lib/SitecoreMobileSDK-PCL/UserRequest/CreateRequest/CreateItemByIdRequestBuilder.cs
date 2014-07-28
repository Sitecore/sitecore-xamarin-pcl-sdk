namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Validators;

    public class CreateItemByIdRequestBuilder : AbstractCreateItemRequestBuilder<ICreateItemByIdRequest>
  {
    public CreateItemByIdRequestBuilder (string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId, this.GetType().Name + ".itemId");

      this.itemId = itemId;
    }

    public override ICreateItemByIdRequest Build()
    {
      if (string.IsNullOrEmpty(this.itemParametersAccumulator.ItemName))
      {
        throw new ArgumentException(this.GetType().Name + ".ItemName : The input cannot be null or empty");
      }

      if (string.IsNullOrEmpty(this.itemParametersAccumulator.ItemTemplate))
      {
        throw new ArgumentException(this.GetType().Name + ".ItemTemplate : The input cannot be null or empty");
      }

      CreateItemByIdParameters result = new CreateItemByIdParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemParametersAccumulator, this.itemId);
      return result;
    }

    private readonly string itemId;
  }
}

