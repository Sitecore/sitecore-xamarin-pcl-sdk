namespace Sitecore.MobileSDK.UrlBuilder
{
    public interface IGetItemByPathRequest : IBaseGetItemRequest
    {
        string ItemPath { get; }
    }
}
