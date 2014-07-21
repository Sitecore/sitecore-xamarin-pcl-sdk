namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  public interface IDeleteItemsByIdRequest : IBaseDeleteItemRequest 
  {
    IDeleteItemsByIdRequest DeepCopyDeleteItemRequest();

    string ItemId { get; }
  }
}
