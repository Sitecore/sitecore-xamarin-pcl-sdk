namespace Sitecore.MobileSDK.UrlBuilder.ItemByQuery
{
    public interface IGetItemByQueryRequest : IBaseGetItemRequest
    {
        string SitecoreQuery { get; }
    }
}
