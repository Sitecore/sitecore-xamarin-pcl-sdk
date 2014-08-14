namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Inteface represents basic parameters neccesessary for read item by sitecore query requests.
  /// </summary>
  public interface IReadItemsByQueryRequest : IBaseReadItemsRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><see cref="IReadItemsByQueryRequest"/></returns>
    IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest();

    /// <summary>
    /// Gets the sitecore query.
    /// </summary>
    /// <value>
    /// The sitecore query.
    /// </value>
    string SitecoreQuery { get; }
  }
}
