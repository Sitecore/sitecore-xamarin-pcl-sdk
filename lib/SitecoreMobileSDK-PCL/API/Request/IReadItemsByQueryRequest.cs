namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Inteface represents basic parameters neccesessary for read item by Sitecore query requests.
  /// </summary>
  public interface IReadItemsByQueryRequest : IBaseReadItemsRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><seealso cref="IReadItemsByQueryRequest"/></returns>
    IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest();

    /// <summary>
    /// Gets the Sitecore query, request string for retrieving and filtering items from the Sitecore database.
    /// See Sitecore documentation for details.
    /// </summary>
    /// <value>
    /// The Sitecore query.
    /// </value>
    string SitecoreQuery { get; }
  }
}
