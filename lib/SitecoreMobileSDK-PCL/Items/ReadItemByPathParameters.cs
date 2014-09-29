namespace Sitecore.MobileSDK.Items
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class ReadItemByPathParameters : IReadItemsByPathRequest
  {
    public ReadItemByPathParameters(
      ISessionConfig sessionSettings,
      IItemSource itemSource,
      IQueryParameters queryParameters,
      IPagingParameters pagingSettings,
      string itemPath)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.ItemPath = itemPath;
      this.QueryParameters = queryParameters;
      this.PagingSettings = pagingSettings;
    }

    public virtual IReadItemsByPathRequest DeepCopyGetItemByPathRequest()
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
      return new ReadItemByPathParameters(connection, itemSrc, payload, pagingSettings, this.ItemPath);
    }

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetItemByPathRequest();
    }

    public string ItemPath { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }

    public IPagingParameters PagingSettings { get; private set; }
  }
}