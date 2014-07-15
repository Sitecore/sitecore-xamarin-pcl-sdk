using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
using Sitecore.MobileSDK.UrlBuilder.CreateItem;
using Sitecore.MobileSDK.UrlBuilder.ItemById;
using Sitecore.MobileSDK.UrlBuilder;


namespace Sitecore.MobileSDK
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;


  public class UpdateItembyIdParameters : IUpdateItemByIdRequest
  {
    public UpdateItembyIdParameters(ISessionConfig sessionSettings, IItemSource itemSource, IQueryParameters queryParameters, CreateItemParameters createParameters, string itemId)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.ItemId = itemId;
      this.QueryParameters = queryParameters;
      this.CreateParameters = createParameters;
    }

    public virtual IUpdateItemByIdRequest DeepCopyUpdateItemByIdRequest()
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

      return new UpdateItemByIdParameters(connection, itemSrc, payload, createParameters, this.ItemId);
    }

    public virtual IReadItemsByIdRequest DeepCopyGetItemByIdRequest()
    {
      return this.DeepCopyUpdateItemByIdRequest();
    }

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyUpdateItemByIdRequest();
    }


    public string ItemId { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }


    public CreateItemParameters CreateParameters { get; private set; }

  }
}

