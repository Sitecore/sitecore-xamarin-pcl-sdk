namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  public interface IDeleteItemsByIdRequest : IBaseDeleteItemRequest 
  {
    string ItemId { get; }
  }
}
