namespace Sitecore.MobileSDK.API.Request
{
  public interface ICreateItemByIdRequest : IReadItemsByIdRequest, IBaseCreateItemRequest
  {
    ICreateItemByIdRequest DeepCopyCreateItemByIdRequest();
  }
}

