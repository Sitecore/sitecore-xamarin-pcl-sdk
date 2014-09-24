namespace Sitecore.MobileSDK.Items
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class ReadRenderingHTMLParameters : IGetRenderingHtmlRequest
  {
    public ReadRenderingHTMLParameters(ISessionConfig sessionSettings, IItemSource itemSource, string sourceId, string renderingId)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.SourceId = sourceId;
      this.RenderingId = renderingId;
    }

    public virtual IGetRenderingHtmlRequest DeepCopyGetRenderingHTMLRequest()
    {
      ISessionConfig connection = null;
      IItemSource itemSrc = null;

      if (null != this.SessionSettings)
      {
        connection = this.SessionSettings.SessionConfigShallowCopy();
      }

      if (null != this.ItemSource)
      {
        itemSrc = this.ItemSource.ShallowCopy();
      }

      return new ReadRenderingHTMLParameters(connection, itemSrc, this.SourceId, this.RenderingId);
    }

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetRenderingHTMLRequest();
    }

    //TODO: igk remove QueryParameters
    public IQueryParameters QueryParameters { get; set; }

    public string SourceId { get; private set; }

    public string RenderingId { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

  }
}

