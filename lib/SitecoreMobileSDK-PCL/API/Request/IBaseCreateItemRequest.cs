
namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.UrlBuilder.CreateItem;

  public interface IBaseCreateItemRequest : IBaseUpdateItemRequest
  {
    //CreateItemParameters CreateParameters{ get; }
    string ItemName{ get; }
    string ItemTemplate{ get; }
  }
}

