
namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.UrlBuilder;


  public interface IReadItemsByIdRequest : IBaseItemRequest
  {
    IReadItemsByIdRequest DeepCopyGetItemByIdRequest();

    string ItemId { get; }
  }
}
