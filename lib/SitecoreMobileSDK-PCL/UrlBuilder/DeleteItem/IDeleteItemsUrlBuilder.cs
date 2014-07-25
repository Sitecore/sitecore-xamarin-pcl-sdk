namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.API.Request;

  public interface IDeleteItemsUrlBuilder<in T> where T : IBaseDeleteItemRequest
  {
    string GetUrlForRequest(T request);
  }
}