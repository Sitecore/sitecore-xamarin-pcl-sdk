namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using Sitecore.MobileSDK.API.Request;

  public interface ICreateItemUrlBuilder<in T> where T : ICreateItemByPathRequest
  {
    string GetUrlForRequest(T request);
  }
}