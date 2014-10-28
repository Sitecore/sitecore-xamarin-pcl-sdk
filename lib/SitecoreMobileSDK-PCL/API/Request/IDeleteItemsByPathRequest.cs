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
    /// <returns><seealso cref="IDeleteItemsByPathRequest"/></returns>
    IDeleteItemsByPathRequest DeepCopyDeleteItemRequest();

    /// <summary>
    /// Gets the item path in the content tree. 
    /// For example: "/sitecore/content/Home"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <value>
    /// The item path.
    /// </value>
    string ItemPath { get; }
  }
}
