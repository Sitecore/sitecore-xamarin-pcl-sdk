namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Inteface represents basic parameters neccesessary for read item by GUID requests.
  /// </summary>
  public interface ISitecoreSearchRequest : IBaseReadItemsRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><seealso cref="ISitecoreSearchRequest"/></returns>
    ISitecoreSearchRequest DeepCopySitecoreSearchRequest();

    string Term { get; }
  }
}
