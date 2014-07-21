namespace Sitecore.MobileSDK.Items.Delete
{
  public interface IDeleteItemsByPathRequest : IBaseDeleteItemRequest
  {
    IDeleteItemsByPathRequest DeepCopyDeleteItemRequest();
    string ItemPath { get; }
  }
}
