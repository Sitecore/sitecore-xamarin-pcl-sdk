namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for udpate of item by GUID.
  /// </summary>
  public interface IUpdateItemByIdRequest : IBaseUpdateItemRequest, IReadItemsByIdRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns>
    ///   <seealso cref="IUpdateItemByIdRequest" />
    /// </returns>
    IUpdateItemByIdRequest DeepCopyUpdateItemByIdRequest();
  }
}
