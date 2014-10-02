namespace Sitecore.MobileSDK.Items
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class ReadItemByQueryParameters : IReadItemsByQueryRequest
  {
    public ReadItemByQueryParameters(
      ISessionConfig sessionSettings,
      IItemSource itemSource,
      IQueryParameters queryParameters,
      IPagingParameters pagingSettings,
      string sitecoreQuery)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.SitecoreQuery = sitecoreQuery;
      this.QueryParameters = queryParameters;
      this.PagingSettings = pagingSettings;
    }


    public virtual IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest()
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


      return new ReadItemByQueryParameters(connection, itemSrc, payload, pagingSettings, this.SitecoreQuery);
    }

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetItemByQueryRequest();
    }

    public string SitecoreQuery { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }
  
    public IPagingParameters PagingSettings { get; private set; }
  }
}

