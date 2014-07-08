


namespace Sitecore.MobileSDK
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;

  public class ItemWebApiRequestBuilder
  {
    private ItemWebApiRequestBuilder()
    {
    }

    public static IGetItemRequestParametersBuilder<IReadItemsByIdRequest> ReadItemsRequestWithId(string itemId)
    {
        return new ReadItemByIdRequestBuilder(itemId);
    }

    public static IGetItemRequestParametersBuilder<IReadItemsByPathRequest> ReadItemsRequestWithPath(string itemPath)
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

    public static ICreateItemRequestParametersBuilder<ICreateItemByIdRequest> CreateItemRequestWithId(string itemId)
    {
      return new CreateItemByIdRequestBuilder(itemId);
    }
  }
}

