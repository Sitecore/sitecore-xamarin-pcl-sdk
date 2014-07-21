namespace Sitecore.MobileSDK.Items.Delete
{
  public interface IDeleteItemsByIdRequest : IBaseDeleteItemRequest
  {
    IDeleteItemsByIdRequest DeepCopyDeleteItemRequest();
    string ItemId { get; }
  }
}
