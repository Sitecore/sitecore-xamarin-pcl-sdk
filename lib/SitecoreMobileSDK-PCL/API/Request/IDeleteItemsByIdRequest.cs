namespace Sitecore.MobileSDK.API.Request
{
  public interface IDeleteItemsByIdRequest : IBaseDeleteItemRequest
  {
    IDeleteItemsByIdRequest DeepCopyDeleteItemRequest();
    string ItemId { get; }
  }
}
