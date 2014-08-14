namespace Sitecore.MobileSDK.API
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Template;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.UserRequest.CreateRequest;
  using Sitecore.MobileSDK.UserRequest.ReadRequest;
  using Sitecore.MobileSDK.UserRequest.UpdateRequest;
  using Sitecore.MobileSDK.UserRequest.DeleteRequest;

  /// <summary>
  /// Main builder class, which provide possibility to create any request.
  /// </summary>
  public class ItemWebApiRequestBuilder
  {
    private ItemWebApiRequestBuilder()
    {
    }

    /// <summary>
    /// Provides builder to construct read item request by item id.
    /// </summary>
    /// <param name="itemId">Item Id value</param>
    /// <returns><see cref="IGetVersionedItemRequestParametersBuilder"/></returns>
    public static IGetVersionedItemRequestParametersBuilder<IReadItemsByIdRequest> ReadItemsRequestWithId(string itemId)
    {
      return new ReadItemByIdRequestBuilder(itemId);
    }

    /// <summary>
    /// Provides builder to construct read item request by item path.
    /// </summary>
    /// <param name="itemPath">Item Path value</param>
    /// <returns><see cref="IGetVersionedItemRequestParametersBuilder"/></returns>
    public static IGetVersionedItemRequestParametersBuilder<IReadItemsByPathRequest> ReadItemsRequestWithPath(string itemPath)
    {
      return new ReadItemByPathRequestBuilder(itemPath);
    }

    /// <summary>
    /// Provides builder to construct read item request with Sitecore query.
    /// </summary>
    /// <param name="sitecoreQuery">Sitecore query value</param>
    /// <returns><see cref="IScopedRequestParametersBuilder"/></returns>
    public static IScopedRequestParametersBuilder<IReadItemsByQueryRequest> ReadItemsRequestWithSitecoreQuery(string sitecoreQuery)
    {
      return new ReadItemByQueryRequestBuilder(sitecoreQuery);
    }

    /// <summary>
    /// Provides builder to construct download resource request with media item path.
    /// </summary>
    /// <param name="mediaPath">A path to the media item, relative to the media library root<see cref="ISessionConfig"/></param>
    /// <returns><see cref="IGetMediaItemRequestParametersBuilder"/></returns>
    public static IGetMediaItemRequestParametersBuilder<IMediaResourceDownloadRequest> DownloadResourceRequestWithMediaPath(string mediaPath)
    {
      return new ReadMediaItemRequestBuilder(mediaPath);
    }

    /// <summary>
    /// Provides builder to construct create item request with parent item id.
    /// </summary>
    /// <param name="itemId">Item Id value</param>
    /// <returns><see cref="ISetTemplateBuilder"/></returns>
    public static ISetTemplateBuilder<ICreateItemByIdRequest> CreateItemRequestWithParentId(string itemId)
    {
      return new CreateItemByIdRequestBuilder(itemId);
    }

    /// <summary>
    /// Provides builder to construct create item request with parent item path.
    /// </summary>
    /// <param name="itemId">Item Path value</param>
    /// <returns><see cref="ISetTemplateBuilder"/></returns>
    public static ISetTemplateBuilder<ICreateItemByPathRequest> CreateItemRequestWithParentPath(string itemPath)
    {
      return new CreateItemByPathRequestBuilder(itemPath);
    }
   
    /// <summary>
    /// Provides builder to construct delete item request with item id.
    /// </summary>
    /// <param name="itemId">Item Id value</param>
    /// <returns><see cref="IDeleteItemRequestBuilder"/></returns>
    public static IDeleteItemRequestBuilder<IDeleteItemsByIdRequest> DeleteItemRequestWithId(string itemId)
    {
      return new DeleteItemByIdRequestBuilder(itemId);
    }

    /// <summary>
    /// Provides builder to construct delete item request with item path.
    /// </summary>
    /// <param name="itemId">Item Path value</param>
    /// <returns><see cref="IDeleteItemRequestBuilder"/></returns>
    public static IDeleteItemRequestBuilder<IDeleteItemsByPathRequest> DeleteItemRequestWithPath(string itemPath)
    {
      return new DeleteItemItemByPathRequestBuilder(itemPath);
    }

    /// <summary>
    /// Provides builder to construct delete item request with Sitecore query.
    /// </summary>
    /// <param name="itemId">Sitecore query value</param>
    /// <returns><see cref="IDeleteItemRequestBuilder"/></returns>
    public static IDeleteItemRequestBuilder<IDeleteItemsByQueryRequest> DeleteItemRequestWithSitecoreQuery(string sitecoreQuery)
    {
      return new DeleteItemItemByQueryRequestBuilder(sitecoreQuery);
    }

    /// <summary>
    /// Provides builder to construct update item request with item id.
    /// </summary>
    /// <param name="itemId">Item Id value</param>
    /// <returns><see cref="IUpdateItemRequestParametersBuilder"/></returns>
    public static IUpdateItemRequestParametersBuilder<IUpdateItemByIdRequest> UpdateItemRequestWithId(string itemId)
    {
      return new UpdateItemByIdRequestBuilder(itemId);
    }

    /// <summary>
    /// Provides builder to construct update item request with item path.
    /// </summary>
    /// <param name="itemId">Item Path value</param>
    /// <returns><see cref="IUpdateItemRequestParametersBuilder"/></returns>
    public static IUpdateItemRequestParametersBuilder<IUpdateItemByPathRequest> UpdateItemRequestWithPath(string itemPath)
    {
      return new UpdateItemByPathRequestBuilder(itemPath);
    }
  }
}

