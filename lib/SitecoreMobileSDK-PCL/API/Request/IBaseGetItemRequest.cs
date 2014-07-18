namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    public interface IBaseGetItemRequest
  {
    IBaseGetItemRequest DeepCopyBaseGetItemRequest();

    IItemSource ItemSource { get; }

    ISessionConfig SessionSettings { get; }

    IQueryParameters QueryParameters { get; }
  }
}
