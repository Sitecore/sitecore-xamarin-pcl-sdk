namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for udpate of item by path.
  /// </summary>
  public interface IUpdateItemByPathRequest : IBaseUpdateItemRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns>
    ///   <seealso cref="IUpdateItemByPathRequest" />
    /// </returns>
    IUpdateItemByPathRequest DeepCopyUpdateItemByPathRequest();


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

