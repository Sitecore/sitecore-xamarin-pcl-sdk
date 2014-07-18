
namespace Sitecore.MobileSDK.API
{
    using Sitecore.MobileSDK.API.Request;
    using Sitecore.MobileSDK.UrlBuilder.CreateItem;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
    using Sitecore.MobileSDK.UrlBuilder.MediaItem;
    using Sitecore.MobileSDK.UserRequest;

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

    public static IBaseRequestParametersBuilder<IReadItemsByQueryRequest> ReadItemsRequestWithSitecoreQuery(string sitecoreQuery)
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

    public static ICreateItemRequestParametersBuilder<ICreateItemByPathRequest> CreateItemRequestWithPath(string itemPath)
    {
      return new CreateItemByPathRequestBuilder(itemPath);
    }
  }
}

