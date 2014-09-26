
namespace Sitecore.MobileSDK.API.Session
{
  using System.IO;
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
    /// <param name="request"><see cref="IReadItemsByIdRequest" /> Read item by Id request.</param>
    /// <param name="cancelToken">The cancel token, should be called in case when you want to terminate request execution.</param>
    /// <returns>
    ///  <see cref="ScItemsResponse" /> Read Items list.
    /// </returns>
    Task<ScItemsResponse> ReadItemAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken));

    /// <summary>
    /// Reads the item for item Path asynchronously
    /// </summary>
    /// <param name="request"><see cref="IReadItemsByPathRequest" /> Read item by Path request.</param>
    /// <param name="cancelToken">The cancel token, should be called in case when you want to terminate request execution.</param>
    /// <returns>
    ///  <see cref="ScItemsResponse" /> Read Items list.
    /// </returns>
    Task<ScItemsResponse> ReadItemAsync(IReadItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken));

    /// <summary>
    /// Reads the item with Sitecore Query asynchronously
    /// </summary>
    /// <param name="request"><see cref="IReadItemsByQueryRequest" /> Read item by Query request.</param>
    /// <param name="cancelToken">The cancel token, should be called in case when you want to terminate request execution.</param>
    /// <returns>
    ///  <see cref="ScItemsResponse" /> Read Items list.
    /// </returns>
    Task<ScItemsResponse> ReadItemAsync(IReadItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken));

    Task<Stream> ReadRenderingHtmlAsync(IGetRenderingHtmlRequest request, CancellationToken cancelToken = default(CancellationToken));

  }
}

