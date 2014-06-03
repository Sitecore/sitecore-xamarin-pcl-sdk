namespace Sitecore.MobileSDK.UrlBuilder.ItemByPath
{
    public interface IReadItemsByPathRequest : IBaseGetItemRequest
    {
        string ItemPath { get; }
    }
}
