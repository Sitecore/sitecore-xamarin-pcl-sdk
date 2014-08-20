namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;

  /// <summary>
  /// Interface represents creation actions that performs items cretion.
  /// </summary>
  public interface ICreateItemActions
  {
    /// <summary>
    /// Creates item with parent GUID asynchronously.
    /// </summary>
    /// <param name="request"><see cref="ICreateItemByIdRequest" />The request.</param>
    /// <param name="cancelToken">The cancel token, should be called in case when you want to terminate request execution.</param>
    /// <returns>
    ///   <see cref="ScItemsResponse" />Created Item.
    /// </returns>
    Task<ScItemsResponse> CreateItemAsync(ICreateItemByIdRequest request, CancellationToken cancelToken = default(CancellationToken));

    /// <summary>
    /// Creates item with parent path asynchronously.
    /// </summary>
    /// <param name="request"><see cref="ICreateItemByPathRequest" />The request.</param>
    /// <param name="cancelToken">The cancel token, should be called in case when you want to terminate request execution.</param>
    /// <returns>
    ///   <see cref="ScItemsResponse" />Created Item.
    /// </returns>
    Task<ScItemsResponse> CreateItemAsync(ICreateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}

