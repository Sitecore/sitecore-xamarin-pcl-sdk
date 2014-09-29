namespace Sitecore.MobileSDK.Items
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class ReadItemsByIdParameters : IReadItemsByIdRequest
  {
    public ReadItemsByIdParameters(
      ISessionConfig sessionSettings, 
      IItemSource itemSource, 
      IQueryParameters queryParameters, 
      IPagingParameters pagingSettings,
      string itemId)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.ItemId = itemId;
      this.QueryParameters = queryParameters;
      this.PagingSettings = pagingSettings;
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

      IPagingParameters pagingSettings = null;
      return new ReadItemsByIdParameters(connection, itemSrc, payload, pagingSettings, this.ItemId);
    }

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetItemByIdRequest();
    }



    public string ItemId { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }
  
    public IPagingParameters PagingSettings { get; private set; }
  }
}

