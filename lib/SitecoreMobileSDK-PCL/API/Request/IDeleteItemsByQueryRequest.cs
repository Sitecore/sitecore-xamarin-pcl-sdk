namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for deletion of item by sitecore query.
  /// </summary>
  public interface IDeleteItemsByQueryRequest : IBaseDeleteItemRequest
  {
    /// <summary>
    /// Performs deep copy request.
    /// </summary>
    /// <returns><see cref="IDeleteItemsByQueryRequest"/></returns>
    IDeleteItemsByQueryRequest DeepCopyDeleteItemRequest();

    /// <summary>
    /// Gets the sitecore query.
    /// </summary>
    /// <value>
    /// The sitecore query.
    /// </value>
    string SitecoreQuery { get; }
  }
}
