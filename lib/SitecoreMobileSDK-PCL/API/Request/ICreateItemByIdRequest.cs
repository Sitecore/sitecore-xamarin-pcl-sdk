
namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.UrlBuilder.CreateItem;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;

    public interface ICreateItemByIdRequest : IReadItemsByIdRequest, IBaseCreateItemRequest
  {
    ICreateItemByIdRequest DeepCopyCreateItemByIdRequest();
  }
}

