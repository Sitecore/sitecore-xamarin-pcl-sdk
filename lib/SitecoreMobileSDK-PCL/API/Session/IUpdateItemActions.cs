namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;

  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;

  /// <summary>
  /// Interface represents update actions that performs items update.
  /// </summary>
  public interface IUpdateItemActions
  {
    /// <summary>
    /// Updates the item with GUID asynchronously.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancelToken">The cancel token.</param>
    /// <returns>
    ///   <see cref="Task{TResult}" />
    /// </returns>
    Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByIdRequest request, CancellationToken cancelToken = default(CancellationToken));

    /// <summary>
    /// Updates the item with path asynchronously.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancelToken">The cancel token.</param>
    /// <returns>
    ///   <see cref="Task{TResult}" />
    /// </returns>
    Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}

