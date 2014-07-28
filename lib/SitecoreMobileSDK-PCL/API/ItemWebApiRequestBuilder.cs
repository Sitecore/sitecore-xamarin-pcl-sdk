namespace Sitecore.MobileSDK.API
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UserRequest.CreateRequest;
  using Sitecore.MobileSDK.Validators;

  using Sitecore.MobileSDK.UserRequest.DeleteRequest;

  public class ItemWebApiRequestBuilder
  {
    private ItemWebApiRequestBuilder()
    {
    }

    public static IGetVersionedItemRequestParametersBuilder<IReadItemsByIdRequest> ReadItemsRequestWithId(string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId, typeof(ItemWebApiRequestBuilder).Name + ".itemId");
      return new ReadItemByIdRequestBuilder(itemId);
    }

    public static IGetVersionedItemRequestParametersBuilder<IReadItemsByPathRequest> ReadItemsRequestWithPath(string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath, typeof(ItemWebApiRequestBuilder).Name + ".itemPath");
      return new ReadItemByPathRequestBuilder(itemPath);
    }

    public static IBaseRequestParametersBuilder<IReadItemsByQueryRequest> ReadItemsRequestWithSitecoreQuery(string sitecoreQuery)
    {
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        sitecoreQuery,
        "[ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery] value path cannot be null or empty"
      );

      return new ReadItemByQueryRequestBuilder(sitecoreQuery);
    }

    public static IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> ReadMediaItemRequest(string mediaPath)
    {
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        mediaPath,
        "[ItemWebApiRequestBuilder.ReadMediaItemRequest] value path cannot be null or empty"
      );

      return new ReadMediaItemRequestBuilder(mediaPath);
    }

    public static ICreateItemRequestParametersBuilder<ICreateItemByIdRequest> CreateItemRequestWithId(string itemId)
    {
      ItemIdValidator.ValidateItemId(itemId, typeof(ItemWebApiRequestBuilder).Name + ".itemId");
      return new CreateItemByIdRequestBuilder(itemId);
    }

    public static ICreateItemRequestParametersBuilder<ICreateItemByPathRequest> CreateItemRequestWithPath(string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath, typeof(ItemWebApiRequestBuilder).Name + ".itemPath");
      return new CreateItemByPathRequestBuilder(itemPath);
    }

    public static IDeleteItemRequestBuilder<IDeleteItemsByIdRequest> DeleteItemRequestWithId(string itemId)
    {
      return new DeleteItemByIdRequestBuilder(itemId);
    }

    public static IDeleteItemRequestBuilder<IDeleteItemsByPathRequest> DeleteItemRequestWithPath(string itemPath)
    {
      return new DeleteItemItemByPathRequestBuilder(itemPath);
    }

    public static IDeleteItemRequestBuilder<IDeleteItemsByQueryRequest> DeleteItemRequestWithSitecoreQuery(string sitecoreQuery)
    {
      return new DeleteItemItemByQueryRequestBuilder(sitecoreQuery);
    }
  }
}

