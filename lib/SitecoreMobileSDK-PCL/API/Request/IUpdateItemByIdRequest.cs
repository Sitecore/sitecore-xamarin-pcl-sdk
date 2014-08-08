namespace Sitecore.MobileSDK.API.Request
{
  public interface IUpdateItemByIdRequest : IBaseUpdateItemRequest, IReadItemsByIdRequest
  {
    IUpdateItemByIdRequest DeepCopyUpdateItemByIdRequest();
  }
}
