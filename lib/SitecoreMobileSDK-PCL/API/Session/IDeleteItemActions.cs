namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;

  /// <summary>
  /// Interface represents deletion actions that perform items deleteion.
  /// </summary>
  public interface IDeleteItemActions
  {
    /// <summary>
    /// Deletes item for GUID asynchronously.
    /// </summary>
    /// <param name="request"><see cref="IDeleteItemsByIdRequest" /> Delete item by Id request.</param>
    /// <param name="cancelToken">The cancel token, should be called in case when you want to terminate request execution.</param>
    /// <returns>
    ///   <see cref="Task{ScDeleteItemsResponse}" /> Deleted items GUIDs list.
    /// </returns>
    Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}
