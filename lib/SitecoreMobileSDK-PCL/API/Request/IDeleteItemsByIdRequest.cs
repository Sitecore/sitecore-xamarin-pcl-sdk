namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for deletion of item by GUID.
  /// </summary>
  public interface IDeleteItemsByIdRequest : IBaseDeleteItemRequest
  {
    /// <summary>
    /// Performs deep copy request.
    /// </summary>
    /// <returns><see cref="IDeleteItemsByIdRequest"/></returns>
    IDeleteItemsByIdRequest DeepCopyDeleteItemRequest();

    /// <summary>
    /// Gets the item GUID.
    /// </summary>
    /// <value>
    /// The item GUID.
    /// </value>
    string ItemId { get; }
  }
}
