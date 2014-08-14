namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for udpate of item by path.
  /// </summary>
  public interface IUpdateItemByPathRequest : IReadItemsByPathRequest, IBaseUpdateItemRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns>
    ///   <see cref="IUpdateItemByPathRequest" />
    /// </returns>
    IUpdateItemByPathRequest DeepCopyUpdateItemByPathRequest();
  }
}

