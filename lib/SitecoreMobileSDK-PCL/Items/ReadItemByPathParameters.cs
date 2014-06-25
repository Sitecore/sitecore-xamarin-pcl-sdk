using Sitecore.MobileSDK.UrlBuilder;

namespace Sitecore.MobileSDK.Items
{
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.SessionSettings;

  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class ReadItemByPathParameters : IReadItemsByPathRequest
  {
    public ReadItemByPathParameters(
      ISessionConfig sessionSettings,
      IItemSource itemSource, 
      IQueryParameters queryParameters, 
      string itemPath)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.ItemPath = itemPath;
      this.QueryParameters = queryParameters;
    }

    public IBaseGetItemRequest DeepCopyBaseGetItemRequest()
    {
      return null;
    }

    public string ItemPath { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }
  }
}