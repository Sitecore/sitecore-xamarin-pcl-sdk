
namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.UrlBuilder.CreateItem;

    public interface IBaseCreateItemRequest : IBaseItemRequest
  {
    CreateItemParameters CreateParameters{ get; }
  }
}

