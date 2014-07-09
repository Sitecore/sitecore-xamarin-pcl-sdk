
namespace Sitecore.MobileSDK
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder;

  public class CreateItemByIdParameters : ICreateItemByIdRequest
  {
    public CreateItemByIdParameters(ISessionConfig sessionSettings, IItemSource itemSource, IQueryParameters queryParameters, CreateItemParameters createParameters, string itemId)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.ItemId = itemId;
      this.QueryParameters = queryParameters;
      this.CreateParameters = createParameters;
    }

    public virtual IReadItemsByIdRequest DeepCopyGetItemByIdRequest()
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

      return new ReadItemsByIdParameters(this.SessionSettings, this.ItemSource, this.QueryParameters, this.ItemId);
    }

    public virtual IBaseGetItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetItemByIdRequest();
    }


    public string ItemId { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }


    public CreateItemParameters CreateParameters { get; private set; }

  }
}

