namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Inteface represents basic parameters neccesessary for read item by GUID requests.
  /// </summary>
  public interface ISitecoreStoredSearchRequest : ISitecoreSearchRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><seealso cref="ISitecoreSearchRequest"/></returns>
    ISitecoreStoredSearchRequest DeepCopySitecoreStoredSearchRequest();

    string ItemId { get; }
  }
}
