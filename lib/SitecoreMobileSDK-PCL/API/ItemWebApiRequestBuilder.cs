namespace Sitecore.MobileSDK.API
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Template;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UserRequest.CreateRequest;
  using Sitecore.MobileSDK.UserRequest.ReadRequest;
  using Sitecore.MobileSDK.UserRequest.UpdateRequest;
  using Sitecore.MobileSDK.UserRequest.DeleteRequest;

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

    public static IScopedRequestParametersBuilder<IReadItemsByQueryRequest> ReadItemsRequestWithSitecoreQuery(string sitecoreQuery)
    {
      return new ReadItemByQueryRequestBuilder(sitecoreQuery);
    }

    public static IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> ReadMediaItemRequest(string mediaPath)
    {
      return new ReadMediaItemRequestBuilder(mediaPath);
    }

    public static ISetTemplateBuilder<ICreateItemByIdRequest> CreateItemRequestWithParentId(string itemId)
    {
      return new CreateItemByIdRequestBuilder(itemId);
    }

    public static ISetTemplateBuilder<ICreateItemByPathRequest> CreateItemRequestWithParentPath(string itemPath)
    {
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

    public static IUpdateItemRequestParametersBuilder<IUpdateItemByIdRequest> UpdateItemRequestWithId(string itemId)
    {
      return new UpdateItemByIdRequestBuilder(itemId);
    }

    public static IUpdateItemRequestParametersBuilder<IUpdateItemByPathRequest> UpdateItemRequestWithPath(string itemPath)
    {
      return new UpdateItemByPathRequestBuilder(itemPath);
    }
  }
}

