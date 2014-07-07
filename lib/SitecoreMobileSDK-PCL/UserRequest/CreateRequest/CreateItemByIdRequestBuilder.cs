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
      return null;
    }

    private readonly string itemId;
  }
}

