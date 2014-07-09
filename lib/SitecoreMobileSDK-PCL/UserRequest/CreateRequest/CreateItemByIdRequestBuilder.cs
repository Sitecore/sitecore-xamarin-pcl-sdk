
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;

  public class CreateItemByIdRequestBuilder : AbstractCreateItemRequestBuilder<ICreateItemByIdRequest>
  {
    public CreateItemByIdRequestBuilder (string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId);

      this.itemId = itemId;
    }

    public override ICreateItemByIdRequest Build()
    {
      CreateItemByIdParameters result = new CreateItemByIdParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemParametersAccumulator, this.itemId);
      return result;
    }

    private readonly string itemId;
  }
}

