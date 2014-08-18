namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for creation of item by parent path.
  /// </summary>
  public interface ICreateItemByPathRequest : IReadItemsByPathRequest, IBaseCreateItemRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><see cref="ICreateItemByPathRequest"/></returns>
    ICreateItemByPathRequest DeepCopyCreateItemByPathRequest();
  }
}

