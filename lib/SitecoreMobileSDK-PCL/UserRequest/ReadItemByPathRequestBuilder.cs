
namespace Sitecore.MobileSDK.UserRequest
{
  using System;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class ReadItemByPathRequestBuilder : AbstractGetVersionedItemRequestBuilder<IReadItemsByPathRequest>
  {
    public ReadItemByPathRequestBuilder(string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath);

      this.itemPath = itemPath;
    }

    public override IReadItemsByPathRequest Build()
    {
      var result = new ReadItemByPathParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemPath);
      return result;
    }

    private string itemPath;
  }
}

