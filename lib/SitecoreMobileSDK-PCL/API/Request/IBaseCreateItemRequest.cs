
namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.UrlBuilder.CreateItem;

    public interface IBaseCreateItemRequest : IBaseGetItemRequest
  {
    CreateItemParameters CreateParameters{ get; }
  }
}

