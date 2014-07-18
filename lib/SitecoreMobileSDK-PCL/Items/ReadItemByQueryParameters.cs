﻿
namespace Sitecore.MobileSDK
{
    using Sitecore.MobileSDK.API.Request;
    using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class ReadItemByQueryParameters : IReadItemsByQueryRequest
  {
    public ReadItemByQueryParameters(
      ISessionConfig sessionSettings,
      IItemSource itemSource, 
      IQueryParameters queryParameters, 
      string sitecoreQuery)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.SitecoreQuery = sitecoreQuery;
      this.QueryParameters = queryParameters;
    }


    public virtual IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest()
    {
      ISessionConfig connection = null;
      IItemSource itemSrc = null;
      IQueryParameters payload = null;

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

      return new ReadItemByQueryParameters(connection, itemSrc, payload, this.SitecoreQuery);
    }

    public virtual IBaseGetItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetItemByQueryRequest();
    }

    public string SitecoreQuery { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }
  }
}

