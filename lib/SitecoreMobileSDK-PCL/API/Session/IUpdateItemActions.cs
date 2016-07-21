namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;

  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;

  /// <summary>
  /// Interface represents update actions that perform items update.
  /// </summary>
  public interface IUpdateItemActions
  {
    /// <summary>
    /// Updates the item with GUID asynchronously.
    /// </summary>
    /// <param name="request"> <see cref="IUpdateItemByIdRequest" /> Update by item Id request.</param>
    /// <param name="cancelToken">The cancel token, should be called in case when you want to terminate request execution.</param>
    /// <returns>
    ///   <see cref="ScItemsResponse" /> Updated items list.
    /// </returns>
    Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByIdRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}

