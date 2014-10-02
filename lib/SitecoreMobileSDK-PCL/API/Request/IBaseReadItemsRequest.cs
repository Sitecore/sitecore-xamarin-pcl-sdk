namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.API.Request.Parameters;


  /// <summary>
  /// Interface represents data set for read item requests.
  /// </summary>
  public interface IBaseReadItemsRequest : IBaseItemRequest
  {
    /// <summary>
    /// Paging settings for partial loading large datasets of items.
    /// </summary>
    /// <value>
    /// An immutable paging settings object.
    /// </value>
    /// <seealso cref="IPagingParameters" />
    IPagingParameters PagingSettings { get; }
  }
}
