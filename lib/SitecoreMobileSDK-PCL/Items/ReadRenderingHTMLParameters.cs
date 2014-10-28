
namespace Sitecore.MobileSDK.Items
{
  using System.Collections.Generic;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class ReadRenderingHtmlParameters : IGetRenderingHtmlRequest
  {
    public ReadRenderingHtmlParameters(ISessionConfig sessionSettings, IItemSource itemSource, IDictionary<string, string> parametersValuesByName, string sourceId, string renderingId)
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.SourceId = sourceId;
      this.RenderingId = renderingId;
      this.ParametersValuesByName = parametersValuesByName;
    }

    public virtual IGetRenderingHtmlRequest DeepCopyGetRenderingHtmlRequest()
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

      return new ReadRenderingHtmlParameters(
        connection, 
        itemSrc, 
        this.ParametersValuesByName, 
        this.SourceId, 
        this.RenderingId
      );
    }

    public string SourceId { get; private set; }

    public string RenderingId { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IDictionary<string, string> ParametersValuesByName { get; private set; }
  }
}

