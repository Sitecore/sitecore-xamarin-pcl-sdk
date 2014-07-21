
namespace Sitecore.MobileSDK
{
    using Sitecore.MobileSDK.API;
    using Sitecore.MobileSDK.API.Items;
    using Sitecore.MobileSDK.API.Request;
    using Sitecore.MobileSDK.API.Request.Parameters;
    using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class ReadItemsByIdParameters : IReadItemsByIdRequest
  {
    public ReadItemsByIdParameters(ISessionConfig sessionSettings, IItemSource itemSource, IQueryParameters queryParameters, string itemId)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.ItemId = itemId;
      this.QueryParameters = queryParameters;
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

      return new ReadItemsByIdParameters(connection, itemSrc, payload, this.ItemId);
    }

    public virtual IBaseGetItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetItemByIdRequest();
    }



    public string ItemId { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }
  }
}

