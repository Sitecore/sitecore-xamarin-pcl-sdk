namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using Sitecore.MobileSDK.Items.Delete;

  public interface IDeleteItemsUrlBuilder<in T> where T : IBaseDeleteItemRequest
  {
    string GetUrlForRequest(T request);
  }
}