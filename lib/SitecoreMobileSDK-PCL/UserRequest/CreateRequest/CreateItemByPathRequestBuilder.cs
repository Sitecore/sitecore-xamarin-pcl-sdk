
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using Sitecore.MobileSDK.API.Request;

    public class CreateItemByPathRequestBuilder : AbstractCreateItemRequestBuilder<ICreateItemByPathRequest>
  {
    public CreateItemByPathRequestBuilder (string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath);
      this.ItemPath = itemPath;
    }

    public override ICreateItemByPathRequest Build()
    {
      if (string.IsNullOrEmpty(this.itemParametersAccumulator.ItemName))
      {
        throw new ArgumentException("CreateItemByIdRequestBuilder.ItemName : The input cannot be null or empty");
      }

      if (string.IsNullOrEmpty(this.itemParametersAccumulator.ItemTemplate))
      {
        throw new ArgumentException("CreateItemByIdRequestBuilder.ItemTemplate : The input cannot be null or empty");
      }

      CreateItemByPathParameters result = 
        new CreateItemByPathParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemParametersAccumulator, this.ItemPath);
      return result;
    }

    private readonly string ItemPath;
  }
}

