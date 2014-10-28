namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for creation of item by parent path.
  /// </summary>
  public interface ICreateItemByPathRequest : IBaseCreateItemRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><seealso cref="ICreateItemByPathRequest"/></returns>
    ICreateItemByPathRequest DeepCopyCreateItemByPathRequest();

    /// <summary>
    /// Gets the item's path in the content tree. 
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

