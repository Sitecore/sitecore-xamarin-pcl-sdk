
namespace Sitecore.MobileSDK.UrlBuilder.UpdateItem
{
  using System;
  using Sitecore.MobileSDK.Validators;

  public class UpdateItemByIdRequestBuilder : AbstractUpdateItemRequestBuilder<IUpdateItemByIdRequest>
  {
    public UpdateItemByIdRequestBuilder (string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId);

      this.itemId = itemId;
    }

    public override IUpdateItemByIdRequest Build()
    {

      UpdateItemByIdParameters result = new UpdateItemByIdParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemParametersAccumulator, this.itemId);
      return result;
    }

    private readonly string itemId;
  }
}
