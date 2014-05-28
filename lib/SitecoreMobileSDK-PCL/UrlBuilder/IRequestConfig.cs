namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;

    public interface IRequestConfig
    {
        string InstanceUrl { get; set; }
        string WebApiVersion { get; }
        string ItemId { get; }
    }
}
