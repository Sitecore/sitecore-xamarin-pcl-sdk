
namespace Sitecore.MobileSDK.UrlBuilder.ItemById
{    
  public interface IReadItemsByIdRequest : IBaseGetItemRequest
  {
    IReadItemsByIdRequest DeepCopyGetItemByIdRequest();

    string ItemId { get; }
  }
}
