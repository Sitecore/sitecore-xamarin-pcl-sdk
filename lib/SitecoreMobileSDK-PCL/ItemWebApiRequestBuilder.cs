
namespace Sitecore.MobileSDK
{
  using System;


  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;

  public class ItemWebApiRequestBuilder
  {
    private ItemWebApiRequestBuilder()
    {
    }

    public static IGetVersionedItemRequestParametersBuilder<IReadItemsByIdRequest> ReadItemsRequestWithId(string itemId)
    {
      return new ReadItemByIdRequestBuilder(itemId);
    }

    public static IGetVersionedItemRequestParametersBuilder<IReadItemsByPathRequest> ReadItemsRequestWithPath(string itemPath)
    {
      return new ReadItemByPathRequestBuilder(itemPath);
    }

    public static IGetItemRequestParametersBuilder<IReadItemsByQueryRequest> ReadItemsRequestWithSitecoreQuery(string sitecoreQuery)
    {
      return new ReadItemByQueryRequestBuilder(sitecoreQuery);
    }

    public static IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> ReadMediaItemRequest(string mediaPath)
    {
      return new ReadMediaItemRequestBuilder(mediaPath);
    }
  }
}

