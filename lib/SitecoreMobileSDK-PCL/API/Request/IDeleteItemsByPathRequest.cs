namespace Sitecore.MobileSDK.API.Request
{
  public interface IDeleteItemsByPathRequest : IBaseDeleteItemRequest
  {
    IDeleteItemsByPathRequest DeepCopyDeleteItemRequest();
    string ItemPath { get; }
  }
}
