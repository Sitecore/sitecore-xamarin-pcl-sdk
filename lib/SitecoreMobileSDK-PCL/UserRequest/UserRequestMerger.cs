using Sitecore.MobileSDK.API.Request.Parameters;

namespace Sitecore.MobileSDK.UserRequest
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  public class UserRequestMerger
  {
    public UserRequestMerger(ISessionConfig defaultSessionConfig, IItemSource defaultSource)
    {
      this.ItemSourceMerger = new ItemSourceFieldMerger(defaultSource);
      this.SessionConfigMerger = new SessionConfigMerger(defaultSessionConfig);
    }

    public IGetRenderingHtmlRequest FillGetRenderingHtmlGaps(IGetRenderingHtmlRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new ReadRenderingHtmlParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.ParametersValuesByName,
        userRequest.SourceId, 
        userRequest.RenderingId
      );
    }

    public IReadItemsByIdRequest FillReadItemByIdGaps(IReadItemsByIdRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      IPagingParameters pagingSettings = userRequest.PagingSettings;
      return new ReadItemsByIdParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.QueryParameters, 
        pagingSettings, 
        userRequest.ItemId);
    }

    public ISitecoreStoredSearchRequest FillSitecoreStoredSearchGaps(ISitecoreStoredSearchRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      IPagingParameters pagingSettings = userRequest.PagingSettings;
      return new StoredSearchParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.QueryParameters, 
        pagingSettings,
        userRequest.ItemId,
        userRequest.Term);
    }

    public ISitecoreSearchRequest FillSitecoreSearchGaps(ISitecoreSearchRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      IPagingParameters pagingSettings = userRequest.PagingSettings;
      return new SitecoreSearchParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.QueryParameters, 
        pagingSettings, 
        userRequest.Term);
    }

    public IReadItemsByPathRequest FillReadItemByPathGaps(IReadItemsByPathRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      IPagingParameters pagingSettings = userRequest.PagingSettings;
      return new ReadItemByPathParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.QueryParameters, 
        pagingSettings,
        userRequest.ItemPath);
    }

    public IReadItemsByQueryRequest FillReadItemByQueryGaps(IReadItemsByQueryRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      IPagingParameters pagingSettings = userRequest.PagingSettings;
      return new ReadItemByQueryParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.QueryParameters, 
        pagingSettings,
        userRequest.SitecoreQuery);
    }

    public IMediaResourceDownloadRequest FillReadMediaItemGaps(IMediaResourceDownloadRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new MediaResourceDownloadParameters(mergedSessionConfig, mergedSource, userRequest.DownloadOptions, userRequest.MediaPath);
    }

    public IMediaResourceUploadRequest FillUploadMediaGaps(IMediaResourceUploadRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGapsForMediaUpload(userRequest.ItemSource);

      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);
     
      UploadMediaOptions createMediaParameters = userRequest.UploadOptions.ShallowCopy();

      return new MediaResourceUploadParameters(mergedSessionConfig, mergedSource, createMediaParameters);
    }

    public ICreateItemByIdRequest FillCreateItemByIdGaps(ICreateItemByIdRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);
      CreateItemParameters createParams = new CreateItemParameters(userRequest.ItemName, userRequest.ItemTemplateId, userRequest.FieldsRawValuesByName);

      return new CreateItemByIdParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, createParams, userRequest.ItemId);
    }

    public ICreateItemByPathRequest FillCreateItemByPathGaps(ICreateItemByPathRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);
      CreateItemParameters createParams = new CreateItemParameters(userRequest.ItemName, userRequest.ItemTemplateId, userRequest.FieldsRawValuesByName);

      return new CreateItemByPathParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, createParams, userRequest.ItemPath);
    }

    public IUpdateItemByIdRequest FillUpdateItemByIdGaps(IUpdateItemByIdRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new UpdateItemByIdParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.FieldsRawValuesByName, userRequest.ItemId);
    }

    public IUpdateItemByPathRequest FillUpdateItemByPathGaps(IUpdateItemByPathRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new UpdateItemByPathParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.FieldsRawValuesByName, userRequest.ItemPath);
    }

    public IDeleteItemsByIdRequest FillDeleteItemByIdGaps(IDeleteItemsByIdRequest userRequest)
    {
      string databse = userRequest.Database;
      if (string.IsNullOrEmpty(databse))
      {
        databse = this.ItemSourceMerger.DefaultSource.Database;
      }
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionConfig);

      return new DeleteItemByIdParameters(mergedSessionConfig, databse, userRequest.ItemId);
    }

    public IDeleteItemsByPathRequest FillDeleteItemByPathGaps(IDeleteItemsByPathRequest userRequest)
    {
      string databse = userRequest.Database;
      if (string.IsNullOrEmpty(databse))
      {
        databse = this.ItemSourceMerger.DefaultSource.Database;
      }
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionConfig);

      return new DeleteItemByPathParameters(mergedSessionConfig, databse, userRequest.ItemPath);
    }

    public IDeleteItemsByQueryRequest FillDeleteItemByQueryGaps(IDeleteItemsByQueryRequest userRequest)
    {
      string databse = userRequest.Database;
      if (string.IsNullOrEmpty(databse))
      {
        databse = this.ItemSourceMerger.DefaultSource.Database;
      }
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionConfig);

      return new DeleteItemByQueryParameters(mergedSessionConfig, databse, userRequest.SitecoreQuery);
    }

    public ItemSourceFieldMerger ItemSourceMerger { get; private set; }
    public SessionConfigMerger SessionConfigMerger { get; private set; }
  }
}

