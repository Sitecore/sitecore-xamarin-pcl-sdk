namespace Sitecore.MobileSDK.UrlBuilder
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    public interface IBaseGetItemRequest
    {
        IItemSource ItemSource { get; }

        ISessionConfig SessionSettings { get; }

        IQueryParameters QueryParameters { get; }
    }
}
