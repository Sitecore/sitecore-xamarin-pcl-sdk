namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for creation of item by parent GUID.
  /// </summary>
  public interface ICreateItemByIdRequest : IReadItemsByIdRequest, IBaseCreateItemRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><see cref="ICreateItemByIdRequest"/></returns>
    ICreateItemByIdRequest DeepCopyCreateItemByIdRequest();
  }
}

