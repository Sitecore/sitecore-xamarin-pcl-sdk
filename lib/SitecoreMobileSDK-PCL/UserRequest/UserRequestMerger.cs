
namespace Sitecore.MobileSDK
{
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  public class UserRequestMerger
  {
    public UserRequestMerger (ISessionConfig defaultSessionConfig, IItemSource defaultSource)
    {
        this.itemSourceMerger = new ItemSourceFieldMerger (defaultSource);
        this.sessionConfigMerger = new SessionConfigMerger (defaultSessionConfig);
    }

    public IReadItemsByIdRequest FillReadItemByIdGaps(IReadItemsByIdRequest userRequest)
    {
        IItemSource mergedSource = this.itemSourceMerger.FillItemSourceGaps (userRequest.ItemSource);
        ISessionConfig mergedSessionConfig = this.sessionConfigMerger.FillSessionConfigGaps (userRequest.SessionSettings);

        return new ReadItemsByIdParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.ItemId);
    }

    public IReadItemsByPathRequest FillReadItemByPathGaps(IReadItemsByPathRequest userRequest)
    {
        IItemSource mergedSource = this.itemSourceMerger.FillItemSourceGaps (userRequest.ItemSource);
        ISessionConfig mergedSessionConfig = this.sessionConfigMerger.FillSessionConfigGaps (userRequest.SessionSettings);

        return new ReadItemByPathParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.ItemPath);
    }

    public IReadItemsByQueryRequest FillReadItemByQueryGaps(IReadItemsByQueryRequest userRequest)
    {
        IItemSource mergedSource = this.itemSourceMerger.FillItemSourceGaps (userRequest.ItemSource);
        ISessionConfig mergedSessionConfig = this.sessionConfigMerger.FillSessionConfigGaps (userRequest.SessionSettings);

        return new ReadItemByQueryParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.SitecoreQuery);
    }

    public IReadMediaItemRequest FillReadMediaItemGaps(IReadMediaItemRequest userRequest)
    {
      IItemSource mergedSource = this.itemSourceMerger.FillItemSourceGaps (userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.sessionConfigMerger.FillSessionConfigGaps (userRequest.SessionSettings);

      return new ReadMediaItemParameters(mergedSessionConfig, mergedSource, userRequest.DownloadOptions, userRequest.MediaItemPath);
    }

    private ItemSourceFieldMerger itemSourceMerger;
    private SessionConfigMerger sessionConfigMerger;
  }
}

