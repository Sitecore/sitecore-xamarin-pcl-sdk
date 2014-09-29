namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.API.Request.Parameters;


  /// <summary>
  /// Interface represents data set for read item requests.
  /// </summary>
  public interface IBaseReadItemsRequest : IBaseItemRequest
  {
    IPagingParameters PagingSettings { get; }
  }
}
