namespace Sitecore.MobileSDK.Items
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class ReadRenderingHTMLParameters : IGetRenderingHtmlRequest
  {
    public ReadRenderingHTMLParameters(ISessionConfig sessionSettings, IItemSource itemSource, IQueryParameters queryParameters, string sourceId, string renderingId)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.SourceId = sourceId;
      this.RenderingId = renderingId;
      this.QueryParameters = queryParameters;
    }

    public virtual IGetRenderingHtmlRequest DeepCopyGetRenderingHTMLRequest()
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

      return new ReadRenderingHTMLParameters(connection, itemSrc, payload, this.SourceId, this.RenderingId);
    }

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetRenderingHTMLRequest();
    }


    public string SourceId { get; private set; }

    public string RenderingId { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }
  }
}

