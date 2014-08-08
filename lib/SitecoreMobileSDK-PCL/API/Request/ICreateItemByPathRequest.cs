namespace Sitecore.MobileSDK.API.Request
{
  public interface ICreateItemByPathRequest : IReadItemsByPathRequest, IBaseCreateItemRequest
  {
    ICreateItemByPathRequest DeepCopyCreateItemByPathRequest();
  }
}

