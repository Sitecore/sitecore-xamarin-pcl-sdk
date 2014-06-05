
namespace Sitecore.MobileSDK.UrlBuilder.ItemById
{    
    public interface IReadItemsByIdRequest : IBaseGetItemRequest
    {
        string ItemId { get; }
    }
}
