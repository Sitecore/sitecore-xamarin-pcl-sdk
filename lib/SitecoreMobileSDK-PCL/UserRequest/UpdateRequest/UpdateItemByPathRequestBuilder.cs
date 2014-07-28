
namespace Sitecore.MobileSDK.UrlBuilder.UpdateItem
{
  using System;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.API.Request;

  public class UpdateItemByPathRequestBuilder : AbstractUpdateItemRequestBuilder<IUpdateItemByPathRequest>
  {
    public UpdateItemByPathRequestBuilder (string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath);

      this.itemPath = itemPath;
    }

    public override IUpdateItemByPathRequest Build()
    {

      UpdateItemByPathParameters result = new UpdateItemByPathParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemParametersAccumulator, this.itemPath);
      return result;
    }

    private readonly string itemPath;
  }
}
