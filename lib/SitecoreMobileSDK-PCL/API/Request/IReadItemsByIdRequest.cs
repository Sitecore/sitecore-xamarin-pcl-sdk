namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Inteface represents basic parameters neccesessary for read item by GUID requests.
  /// </summary>
  public interface IReadItemsByIdRequest : IBaseReadItemsRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><see cref="IReadItemsByIdRequest"/></returns>
    IReadItemsByIdRequest DeepCopyGetItemByIdRequest();

    /// <summary>
    /// Gets item GUID values enclosed in curly braces.
    /// For example: "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <value>
    /// The item GUID.
    /// </value>
    string ItemId { get; }
  }
}
