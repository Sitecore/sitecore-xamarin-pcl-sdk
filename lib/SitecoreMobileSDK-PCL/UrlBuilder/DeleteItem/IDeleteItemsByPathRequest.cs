namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  interface IDeleteItemsByPathRequest : IBaseDeleteItemRequest
  {
    string Itempath { get; }
  }
}
