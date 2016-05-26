using Sitecore.MobileSDK.UserRequest.SearchRequest;
using Sitecore.MobileSDK.Items;

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
  using Sitecore.MobileSDK.UserRequest.UploadMediaRequest;

  /// <summary>
  /// Main factory to construct buider for concrete request. 
  /// </summary>
  public class ItemWebApiRequestBuilder
  {
    private ItemWebApiRequestBuilder()
    {
    }

    /// <summary>
    /// Provides builder for constructing read item request by item GUID.
    /// </summary>
    /// <param name="itemId">Item GUID value enclosed in curly braces.
    /// For example: "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"
    /// </param>
    /// <returns>
    /// <see cref="IGetVersionedItemRequestParametersBuilder{T}" /> Read item by Id request builder.
    /// </returns>
    public static IGetVersionedItemRequestParametersBuilder<IReadItemsByIdRequest> ReadItemsRequestWithId(string itemId)
    {
      return new ReadItemByIdRequestBuilder(itemId);
    }

    public static IGetVersionedItemRequestParametersBuilder<IReadItemsByIdRequest> ReadChildrenRequestWithId(string itemId)
    {
      return new ReadChildrenByIdRequestBuilder(itemId);
    }

    //TODO: @igk I should think about naming
    public static IGetVersionedItemRequestParametersBuilder<IReadItemsByIdRequest> StoredQuerryRequest(string itemId)
    {
      return new RunStoredQuerryRequestBuilder(itemId);
    }

    public static RunStoredSearchRequestBuilder StoredSearchRequest(string itemId)
    {
      return new RunStoredSearchRequestBuilder(itemId);
    }

    public static IGetVersionedItemRequestParametersBuilder<SitecoreSearchParameters> SitecoreSearchRequest(string term)
    {
      return new SitecoreSearchRequestBuilder(term);
    }

    /// <summary>
    /// Provides builder for constructing read item request by item path.
    /// </summary>
    /// <param name="itemPath">Absolute Item path, must starts with "/" symbol</param>
    /// <returns>
    /// <see cref="IGetVersionedItemRequestParametersBuilder{T}" /> Read item by Path request builder.
    /// </returns>
    public static IGetVersionedItemRequestParametersBuilder<IReadItemsByPathRequest> ReadItemsRequestWithPath(string itemPath)
    {
      return new ReadItemByPathRequestBuilder(itemPath);
    }

    /// <summary>
    /// Provides builder for constructing read item request with Sitecore query.
    /// </summary>
    /// <param name="sitecoreQuery">Sitecore query or fast query</param>
    /// <returns>
    ///   <see cref="IScopedRequestParametersBuilder{T}" /> Read item by Sitecore Query request builder.
    /// </returns>
    public static IScopedRequestParametersBuilder<IReadItemsByQueryRequest> ReadItemsRequestWithSitecoreQuery(string sitecoreQuery)
    {
      return new ReadItemByQueryRequestBuilder(sitecoreQuery);
    }

    /// <summary>
    /// Provides builder for constructing download resource request with media item path.
    /// </summary>
    /// <param name="mediaPath">A path to the media item, relative to the media library root</param>
    /// <returns>
    ///   <see cref="IGetMediaItemRequestParametersBuilder{T}" /> Read media item by Media Item Path request builder.
    /// </returns>
    public static IGetMediaItemRequestParametersBuilder<IMediaResourceDownloadRequest> DownloadResourceRequestWithMediaPath(string mediaPath)
    {
      return new DownloadMediaResourceRequestBuilder(mediaPath);
    }

    /// <summary>
    /// Provides builder for constructing create item request with parent item GUID.
    /// </summary>
    /// <param name="itemId">Parent item GUID value enclosed in curly braces.
    /// For example: "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"
    /// </param>
    /// <returns>
    ///   <see cref="ISetTemplateBuilder{T}" /> Create item with parent Item Id request.
    /// </returns>
    public static ISetTemplateBuilder<ICreateItemByIdRequest> CreateItemRequestWithParentId(string itemId)
    {
      return new CreateItemByIdRequestBuilder(itemId);
    }

    /// <summary>
    /// Provides builder for constructing create item request with parent item path.
    /// </summary>
    /// <param name="itemPath">Parent item absolute path, must starts with "/" symbol.</param>
    /// <returns>
    ///   <see cref="ISetTemplateBuilder{T}" /> Create item with parent Item Path request.
    /// </returns>
    public static ISetTemplateBuilder<ICreateItemByPathRequest> CreateItemRequestWithParentPath(string itemPath)
    {
      return new CreateItemByPathRequestBuilder(itemPath);
    }

    /// <summary>
    /// Provides builder for constructing delete item request with item GUID.
    /// </summary>
    /// <param name="itemId">Item GUID value enclosed in curly braces.
    /// For example: "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"
    /// </param>
    /// <returns>
    ///   <see cref="IDeleteItemRequestBuilder{T}" /> Delete item with Item Id request.
    /// </returns>
    public static IDeleteItemRequestBuilder<IDeleteItemsByIdRequest> DeleteItemRequestWithId(string itemId)
    {
      return new DeleteItemByIdRequestBuilder(itemId);
    }

    /// <summary>
    /// Provides builder for constructing delete item request with item path.
    /// </summary>
    /// <param name="itemPath">Absolute Item Path, must starts with "/" symbol.</param>
    /// <returns>
    ///   <see cref="IDeleteItemRequestBuilder{T}" /> Delete item with Item Path request.
    /// </returns>
    public static IDeleteItemRequestBuilder<IDeleteItemsByPathRequest> DeleteItemRequestWithPath(string itemPath)
    {
      return new DeleteItemItemByPathRequestBuilder(itemPath);
    }

    /// <summary>
    /// Provides builder for constructing delete item request with Sitecore query or fast query.
    /// </summary>
    /// <param name="sitecoreQuery">Sitecore query.</param>
    /// <returns>
    ///   <see cref="IDeleteItemRequestBuilder{T}" /> Delete item with Sitecore Query request.
    /// </returns>
    public static IDeleteItemRequestBuilder<IDeleteItemsByQueryRequest> DeleteItemRequestWithSitecoreQuery(string sitecoreQuery)
    {
      return new DeleteItemItemByQueryRequestBuilder(sitecoreQuery);
    }

    /// <summary>
    /// Provides builder for constructing update item request with item id.
    /// </summary>
    /// <param name="itemId">Item GUID value enclosed in curly braces.
    /// For example: "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"
    /// </param>
    /// <returns>
    ///   <see cref="IUpdateItemRequestParametersBuilder{T}" /> Update item with Item Id request.
    /// </returns>
    public static IUpdateItemRequestParametersBuilder<IUpdateItemByIdRequest> UpdateItemRequestWithId(string itemId)
    {
      return new UpdateItemByIdRequestBuilder(itemId);
    }

    /// <summary>
    /// Provides builder for constructing update item request with item path.
    /// </summary>
    /// <param name="itemPath">Absolute Item path, must starts with "/" symbol</param>
    /// <returns>
    ///   <see cref="IUpdateItemRequestParametersBuilder{T}" /> Update item with Item Path request.
    /// </returns>
    public static IUpdateItemRequestParametersBuilder<IUpdateItemByPathRequest> UpdateItemRequestWithPath(string itemPath)
    {
      return new UpdateItemByPathRequestBuilder(itemPath);
    }

    /// <summary>
    /// Provides builder for constructing read rendering request with source item id and rendering item id.
    /// </summary>
    /// <param name="sourceId">Source item GUID values enclosed in curly braces.
    /// For example: "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"</param>
    /// <param name="renderingId">Rendering item GUID value enclosed in curly braces.
    /// For example: "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"</param>
    /// <returns>
    ///   <see cref="IRenderingHtmlRequestParametersBuilder{T}" />Get rendering html request builder.
    /// </returns>
    public static IRenderingHtmlRequestParametersBuilder<IGetRenderingHtmlRequest> RenderingHtmlRequestWithSourceAndRenderingId(string sourceId, string renderingId)
    {
      return new RenderingHtmlRequestBuilder(sourceId, renderingId);
    }

    /// <summary>
    /// Provides builder for constructing upload media request with parent item path.
    /// </summary>
    /// <param name="itemPath">Relative path to the media library folder. Must stars with the '/' symbol.</param>
    /// <returns>
    ///   <see cref="IUploadMediaItemRequestParametersBuilder{T}" />Upload media request builder.
    /// </returns>
    public static IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> UploadResourceRequestWithParentPath(string mediaPath)
    {
      return new UploadMediaItemByParentPathRequestBuilder(mediaPath);
    }

    /// <summary>
    /// Provides builder for constructing upload media request with parent item Id.
    /// </summary>
    /// <param name="parentId">Parent item GUID value enclosed in curly braces.
    /// For example: "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"</param>
    /// <returns>
    ///   <see cref="IUploadMediaItemRequestParametersBuilder{T}" />Upload media request builder.
    /// </returns>
    public static IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> UploadResourceRequestWithParentId(string parentId)
    {
      return new UploadMediaItemByParentIdRequestBuilder(parentId);
    }
  }
}

