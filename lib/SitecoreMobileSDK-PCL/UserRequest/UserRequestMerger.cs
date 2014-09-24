namespace Sitecore.MobileSDK.UserRequest
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;


  public class UserRequestMerger
  {
    public UserRequestMerger(ISessionConfig defaultSessionConfig, IItemSource defaultSource)
    {
      this.ItemSourceMerger = new ItemSourceFieldMerger(defaultSource);
      this.SessionConfigMerger = new SessionConfigMerger(defaultSessionConfig);
    }

    public IGetRenderingHtmlRequest FillGetRenderingHTMLGaps(IGetRenderingHtmlRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new ReadRenderingHTMLParameters(
        mergedSessionConfig, 
        mergedSource, 
        userRequest.SourceId, 
        userRequest.RenderingId
      );
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
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new ReadItemByQueryParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, userRequest.SitecoreQuery);
    }

    public IMediaResourceDownloadRequest FillReadMediaItemGaps(IMediaResourceDownloadRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);

      return new ReadMediaItemParameters(mergedSessionConfig, mergedSource, userRequest.DownloadOptions, userRequest.MediaPath);
    }

    public ICreateItemByIdRequest FillCreateItemByIdGaps(ICreateItemByIdRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);
      CreateItemParameters createParams = new CreateItemParameters(userRequest.ItemName, userRequest.ItemTemplate, userRequest.FieldsRawValuesByName);

      return new CreateItemByIdParameters(mergedSessionConfig, mergedSource, userRequest.QueryParameters, createParams, userRequest.ItemId);
    }

    public ICreateItemByPathRequest FillCreateItemByPathGaps(ICreateItemByPathRequest userRequest)
    {
      IItemSource mergedSource = this.ItemSourceMerger.FillItemSourceGaps(userRequest.ItemSource);
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionSettings);
      CreateItemParameters createParams = new CreateItemParameters(userRequest.ItemName, userRequest.ItemTemplate, userRequest.FieldsRawValuesByName);

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

      return new DeleteItemByIdParameters(mergedSessionConfig, userRequest.ScopeParameters, databse, userRequest.ItemId);
    }

    public IDeleteItemsByPathRequest FillDeleteItemByPathGaps(IDeleteItemsByPathRequest userRequest)
    {
      string databse = userRequest.Database;
      if (string.IsNullOrEmpty(databse))
      {
        databse = this.ItemSourceMerger.DefaultSource.Database;
      }
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionConfig);

      return new DeleteItemByPathParameters(mergedSessionConfig, userRequest.ScopeParameters, databse, userRequest.ItemPath);
    }

    public IDeleteItemsByQueryRequest FillDeleteItemByQueryGaps(IDeleteItemsByQueryRequest userRequest)
    {
      string databse = userRequest.Database;
      if (string.IsNullOrEmpty(databse))
      {
        databse = this.ItemSourceMerger.DefaultSource.Database;
      }
      ISessionConfig mergedSessionConfig = this.SessionConfigMerger.FillSessionConfigGaps(userRequest.SessionConfig);

      return new DeleteItemByQueryParameters(mergedSessionConfig, userRequest.ScopeParameters, databse, userRequest.SitecoreQuery);
    }

    public ItemSourceFieldMerger ItemSourceMerger { get; private set; }
    public SessionConfigMerger SessionConfigMerger { get; private set; }
  }
}

