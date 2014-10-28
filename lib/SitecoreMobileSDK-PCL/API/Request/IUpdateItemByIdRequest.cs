namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents data set for udpate of item by GUID.
  /// </summary>
  public interface IUpdateItemByIdRequest : IBaseUpdateItemRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns>
    ///   <seealso cref="IUpdateItemByIdRequest" />
    /// </returns>
    IUpdateItemByIdRequest DeepCopyUpdateItemByIdRequest();

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
