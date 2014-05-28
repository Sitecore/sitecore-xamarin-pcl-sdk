namespace Sitecore.MobileSDK.UrlBuilder
{
    public interface IRequestConfig
    {
        string InstanceUrl { get; set; }
        string WebApiVersion { get; }
        string ItemId { get; }
    }
}
