namespace Sitecore.MobileSDK.UrlBuilder.ItemByQuery
{
    public interface IReadItemsByQueryRequest : IBaseGetItemRequest
    {
        string SitecoreQuery { get; }
    }
}
