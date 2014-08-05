
namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.UrlBuilder;


  public interface IReadItemsByIdRequest : IBaseReadItemsRequest
  {
    IReadItemsByIdRequest DeepCopyGetItemByIdRequest();

    string ItemId { get; }
  }
}
