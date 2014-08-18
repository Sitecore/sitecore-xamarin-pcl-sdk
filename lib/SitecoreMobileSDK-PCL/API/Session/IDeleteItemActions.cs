namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;

  /// <summary>
  /// Interface represents deletion actions that performs items deleteion.
  /// </summary>
  public interface IDeleteItemActions
  {
    /// <summary>
    /// Deletes item for GUID asynchronously.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancelToken">The cancel token.</param>
    /// <returns>
    ///   <see cref="Task{TResult}" />
    /// </returns>
    Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken));

    /// <summary>
    /// Deletes item for item path asynchronously.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancelToken">The cancel token.</param>
    /// <returns>
    /// this
    /// </returns>
    Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken));

    /// <summary>
    /// Deletes item for sitecore query asynchronously.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancelToken">The cancel token.</param>
    /// <returns>
    /// this
    /// </returns>
    Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}
