namespace Sitecore.MobileSDK.Items
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class StoredSearchParameters : ISitecoreStoredSearchRequest
  {
    public StoredSearchParameters(
      ISessionConfig sessionSettings, 
      IItemSource itemSource, 
      IQueryParameters queryParameters, 
      IPagingParameters pagingSettings,
      string itemId,
      string term)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.ItemId = itemId;
      this.QueryParameters = queryParameters;
      this.PagingSettings = pagingSettings;
      this.Term = term;
    }

    public virtual ISitecoreStoredSearchRequest DeepCopySitecoreStoredSearchRequest()
    {
      ISessionConfig connection = null;
      IItemSource itemSrc = null;
      IQueryParameters payload = null;
      IPagingParameters pagingSettings = null;

      if (null != this.SessionSettings)
      {
        connection = this.SessionSettings.SessionConfigShallowCopy();
      }

      if (null != this.ItemSource)
      {
        itemSrc = this.ItemSource.ShallowCopy();
      }

      if (null != this.QueryParameters)
      {
        payload = this.QueryParameters.DeepCopy();
      }

      if (null != this.PagingSettings)
      {
        pagingSettings = this.PagingSettings.PagingParametersCopy();
      }

      return new StoredSearchParameters(connection, itemSrc, payload, pagingSettings, this.ItemId, this.Term);
    }

    public virtual ISitecoreSearchRequest DeepCopySitecoreSearchRequest()
    {
      return this.DeepCopySitecoreStoredSearchRequest();
    }
      
    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopySitecoreStoredSearchRequest();
    }



    public string ItemId { get; private set; }

    public string Term { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }
  
    public IPagingParameters PagingSettings { get; private set; }

    public bool IcludeStanderdTemplateFields { get; private set; }

    public ISortParameters SortParameters { 
      get {
        return null;
      }
    }
  }
}

