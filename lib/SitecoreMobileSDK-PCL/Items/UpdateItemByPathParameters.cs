
namespace Sitecore.MobileSDK
{
  using System;
  using System.Collections.Generic;

  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder;

  public class UpdateItemByPathParameters : IUpdateItemByPathRequest
  {
    public UpdateItemByPathParameters(ISessionConfig sessionSettings, IItemSource itemSource, IQueryParameters queryParameters, IDictionary<string, string> fieldsRawValuesByName, string itemPath)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.ItemPath = itemPath;
      this.QueryParameters = queryParameters;
      this.FieldsRawValuesByName = fieldsRawValuesByName;
    }

    public virtual IUpdateItemByPathRequest DeepCopyUpdateItemByPathRequest()
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
        
      return new UpdateItemByPathParameters(connection, itemSrc, payload, this.FieldsRawValuesByName, this.ItemPath);
    }

    public virtual IReadItemsByPathRequest DeepCopyGetItemByPathRequest()
    {
      return this.DeepCopyUpdateItemByPathRequest();
    }

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyUpdateItemByPathRequest();
    }


    public string ItemPath { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }

    public IDictionary<string, string> FieldsRawValuesByName { get; private set; }

  }
}

