namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Inteface represents basic parameters neccesessary for read item by path requests.
  /// </summary>
  public interface IReadItemsByPathRequest : IBaseReadItemsRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><see cref="IReadItemsByPathRequest"/></returns>
    IReadItemsByPathRequest DeepCopyGetItemByPathRequest();

    /// <summary>
    /// Gets the item path.
    /// </summary>
    /// <value>
    /// The item path.
    /// </value>
    string ItemPath { get; }
  }
}
