namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Inteface represents basic parameters neccesessary for read item by GUID requests.
  /// </summary>
  public interface IReadItemsByIdRequest : IBaseReadItemsRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><see cref="IReadItemsByIdRequest"/></returns>
    IReadItemsByIdRequest DeepCopyGetItemByIdRequest();

    /// <summary>
    /// Gets the item GUID.
    /// </summary>
    /// <value>
    /// The item GUID.
    /// </value>
    string ItemId { get; }
  }
}
