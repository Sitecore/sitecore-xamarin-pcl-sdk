namespace Sitecore.MobileSDK.API.Request
{
  public interface IUpdateItemByPathRequest : IReadItemsByPathRequest, IBaseUpdateItemRequest
  {
    IUpdateItemByPathRequest DeepCopyUpdateItemByPathRequest();
  }
}

