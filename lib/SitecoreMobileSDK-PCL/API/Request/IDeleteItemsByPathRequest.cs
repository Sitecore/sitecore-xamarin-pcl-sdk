namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for deletion of item by path.
  /// </summary>
  public interface IDeleteItemsByPathRequest : IBaseDeleteItemRequest
  {
    /// <summary>
    /// Performs deep copy request.
    /// </summary>
    /// <returns><see cref="IDeleteItemsByPathRequest"/></returns>
    IDeleteItemsByPathRequest DeepCopyDeleteItemRequest();

    /// <summary>
    /// Gets the item path.
    /// </summary>
    /// <value>
    /// The item path.
    /// </value>
    string ItemPath { get; }
  }
}
