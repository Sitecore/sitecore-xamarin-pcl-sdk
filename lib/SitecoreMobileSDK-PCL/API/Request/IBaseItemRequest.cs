namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface IBaseItemRequest
  {
    IBaseItemRequest DeepCopyBaseGetItemRequest();

    IItemSource ItemSource { get; }

    ISessionConfig SessionSettings { get; }

    IQueryParameters QueryParameters { get; }
  }
}
