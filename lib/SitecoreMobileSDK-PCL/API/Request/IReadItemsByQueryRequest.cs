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
    /// <returns><seealso cref="IReadItemsByQueryRequest"/></returns>
    IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest();

    /// <summary>
    /// Gets the sitecore query, request string for retrieving and filtering items from the Sitecore database.
    /// See Sitecore documentation for details.
    /// </summary>
    /// <value>
    /// The sitecore query.
    /// </value>
    string SitecoreQuery { get; }
  }
}
