using Sitecore.MobileSDK.API.Request.Parameters;

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

    ISortParameters SortParameters { get; }

    string Term { get; }
  }
}
