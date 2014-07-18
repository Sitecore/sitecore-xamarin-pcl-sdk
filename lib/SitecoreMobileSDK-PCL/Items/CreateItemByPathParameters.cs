using Sitecore.MobileSDK.UrlBuilder.ItemByPath;


namespace Sitecore.MobileSDK
{
  using System;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder;

  public class CreateItemByPathParameters : ICreateItemByPathRequest
  {
    public CreateItemByPathParameters(ISessionConfig sessionSettings, IItemSource itemSource, IQueryParameters queryParameters, CreateItemParameters createParameters, string itemPath)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.ItemPath = itemPath;
      this.QueryParameters = queryParameters;
      this.CreateParameters = createParameters;
    }

    public virtual ICreateItemByPathRequest DeepCopyCreateItemByPathRequest()
    {
      ISessionConfig connection = null;
      IItemSource itemSrc = null;
      IQueryParameters payload = null;
      CreateItemParameters createParameters = null;

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

      if (null != this.CreateParameters)
      {
        createParameters = this.CreateParameters.ShallowCopy();
      }

      return new CreateItemByPathParameters(connection, itemSrc, payload, createParameters, this.ItemPath);
    }

    public virtual IReadItemsByPathRequest DeepCopyGetItemByPathRequest()
    {
      return this.DeepCopyCreateItemByPathRequest();
    }

    public virtual IBaseGetItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyCreateItemByPathRequest();
    }


    public string ItemPath { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }


    public CreateItemParameters CreateParameters { get; private set; }

  }
}

