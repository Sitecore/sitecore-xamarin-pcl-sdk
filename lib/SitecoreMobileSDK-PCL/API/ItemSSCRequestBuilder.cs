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

  /// <summary>
  /// Main factory to construct buider for concrete request. 
  /// </summary>
  public class ItemSSCRequestBuilder
  {
    private ItemSSCRequestBuilder()
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

    public static ISearchItemRequestParametersBuilder<SitecoreSearchParameters> SitecoreSearchRequest(string term)
    {
      return (ISearchItemRequestParametersBuilder<SitecoreSearchParameters>)new SitecoreSearchRequestBuilder(term);
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

  }
}

