using System;

namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  public class CreateItemByIdRequestBuilder : AbstractCreateItemRequestBuilder<ICreateItemByIdRequest>
  {
    public CreateItemByIdRequestBuilder (string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId);

      this.itemId = itemId;
    }

    public override ICreateItemByIdRequest Build()
    {
      CreateItemByIdParameters result = new CreateItemByIdParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemId);
      return result;
    }

    private readonly string itemId;
  }
}

