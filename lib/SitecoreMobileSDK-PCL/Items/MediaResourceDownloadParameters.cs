﻿namespace Sitecore.MobileSDK.Items
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class MediaResourceDownloadParameters : IMediaResourceDownloadRequest
  {
    public MediaResourceDownloadParameters
    (
      ISessionConfig sessionSettings,
      IItemSource itemSource,
      IDownloadMediaOptions downloadOptions,
      string mediaPath
    )
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.MediaPath = mediaPath;
      this.DownloadOptions = downloadOptions;
    }

    public virtual IMediaResourceDownloadRequest DeepCopyReadMediaRequest()
    {
      ISessionConfig connection = null;
      IItemSource itemSource = null;
      IDownloadMediaOptions resizingOptions = null;

      if (null != this.SessionSettings)
      {
        connection = this.SessionSettings.SessionConfigShallowCopy();
      }

      if (null != this.ItemSource)
      {
        itemSource = this.ItemSource.ShallowCopy();
      }

      if (null != this.DownloadOptions)
      {
        resizingOptions = this.DownloadOptions.DeepCopyMediaDownloadOptions();
      }

      return new MediaResourceDownloadParameters(connection, itemSource, resizingOptions, this.MediaPath);
    }

    public string MediaPath { get; private set; }

    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public IDownloadMediaOptions DownloadOptions { get; private set; }
  }
}
