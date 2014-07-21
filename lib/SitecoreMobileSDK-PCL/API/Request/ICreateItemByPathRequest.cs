
namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.UrlBuilder.CreateItem;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;

    public interface ICreateItemByPathRequest : IReadItemsByPathRequest, IBaseCreateItemRequest
  {
    ICreateItemByPathRequest DeepCopyCreateItemByPathRequest();
  }
}

