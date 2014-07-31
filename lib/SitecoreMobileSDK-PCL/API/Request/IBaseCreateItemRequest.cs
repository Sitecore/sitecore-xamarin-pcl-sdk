
namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.UrlBuilder.CreateItem;

  public interface IBaseCreateItemRequest : IBaseChangeItemRequest
  {
    string ItemName{ get; }
    string ItemTemplate{ get; }
  }
}

