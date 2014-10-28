namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for deletion of item by Sitecore query.
  /// </summary>
  public interface IDeleteItemsByQueryRequest : IBaseDeleteItemRequest
  {
    /// <summary>
    /// Performs deep copy request.
    /// </summary>
    /// <returns><seealso cref="IDeleteItemsByQueryRequest"/></returns>
    IDeleteItemsByQueryRequest DeepCopyDeleteItemRequest();

    /// <summary>
    /// Gets the Sitecore query, request string for retrieving and filtering items from the Sitecore database.
    /// See Sitecore documentation for details.
    /// </summary>
    /// <value>
    /// The Sitecore query value.
    /// </value>
    string SitecoreQuery { get; }
  }
}
