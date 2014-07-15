namespace Sitecore.MobileSDK.UrlBuilder
{
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public interface IBaseItemRequest
  {
    IBaseItemRequest DeepCopyBaseGetItemRequest();

    IItemSource ItemSource { get; }

    ISessionConfig SessionSettings { get; }

    IQueryParameters QueryParameters { get; }
  }
}
