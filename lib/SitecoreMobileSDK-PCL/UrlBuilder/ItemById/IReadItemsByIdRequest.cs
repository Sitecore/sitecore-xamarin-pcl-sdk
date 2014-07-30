
namespace Sitecore.MobileSDK.UrlBuilder.ItemById
{    
  public interface IReadItemsByIdRequest : IBaseItemRequest
  {
    IReadItemsByIdRequest DeepCopyGetItemByIdRequest();

    string ItemId { get; }
  }
}
