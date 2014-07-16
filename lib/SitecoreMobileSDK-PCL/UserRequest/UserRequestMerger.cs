
namespace Sitecore.MobileSDK.UserRequest
{
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;


  public class UserRequestMerger
  {
    public UserRequestMerger (ISessionConfig defaultSessionConfig, IItemSource defaultSource)
    {
      this.ItemSourceMerger = new ItemSourceFieldMerger(defaultSource);
      this.SessionConfigMerger = new SessionConfigMerger(defaultSessionConfig);
    }

    public IReadItemsByIdRequest FillReadItemByIdGaps(IReadItemsByIdRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new ReadItemsByIdParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.ItemId);
    }

    public IReadItemsByPathRequest FillReadItemByPathGaps(IReadItemsByPathRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new ReadItemByPathParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.ItemPath);
    }

    public IReadItemsByQueryRequest FillReadItemByQueryGaps(IReadItemsByQueryRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps (userRequest.SessionSettings);

      return new ReadItemByQueryParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.SitecoreQuery);
    }

    public IReadMediaItemRequest FillReadMediaItemGaps(IReadMediaItemRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new ReadMediaItemParameters(mergedSessionConfig, mergedSource, userRequest.DownloadOptions, userRequest.MediaPath);
    }

    public ICreateItemByIdRequest FillCreateItemByIdGaps(ICreateItemByIdRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new CreateItemByIdParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.CreateParameters, userRequest.ItemId);
    }

    public ICreateItemByPathRequest FillCreateItemByPathGaps(ICreateItemByPathRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps (userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps (userRequest.SessionSettings);

      return new CreateItemByPathParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.CreateParameters, userRequest.ItemPath);
    }

    public IUpdateItemByIdRequest FillUpdateItemByIdGaps(IUpdateItemByIdRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps (userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps (userRequest.SessionSettings);

      return new UpdateItemByIdParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.CreateParameters, userRequest.ItemId);
    }

    public ItemSourceFieldMerger ItemSourceMerger { get; private set; }
    public SessionConfigMerger   SessionConfigMerger { get; private set; }
  }
}

