namespace Sitecore.MobileSDK.UrlBuilder
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;

    public interface IBaseGetItemRequest
    {
        IItemSource ItemSource { get; }

        ISessionConfig SessionSettings { get; }
    }
}
