namespace Sitecore.MobileSDK.UrlBuilder.ItemByPath
{
    public interface IGetItemByPathRequest : IBaseGetItemRequest
    {
        string ItemPath { get; }
    }
}
