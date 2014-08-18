namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;

  /// <summary>
  /// Interface represents read actions that retrieves items.
  /// </summary>
  public interface IReadItemActions
  {
    /// <summary>
    /// Reads the item for item GUID asynchronously.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancelToken">The cancel token.</param>
    /// <returns>
    /// The <see cref="Task{Stream}" />.
    /// </returns>
    Task<ScItemsResponse> ReadItemAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken));

    /// <summary>
    /// Reads the item for item path asynchronously
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancelToken">The cancel token.</param>
    /// <returns>
    /// The <see cref="Task{Stream}" />.
    /// </returns>
    Task<ScItemsResponse> ReadItemAsync(IReadItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken));

    /// <summary>
    /// Reads the item for sitecore query asynchronously
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancelToken">The cancel token.</param>
    /// <returns>
    /// The <see cref="Task{Stream}" />.
    /// </returns>
    Task<ScItemsResponse> ReadItemAsync(IReadItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}

