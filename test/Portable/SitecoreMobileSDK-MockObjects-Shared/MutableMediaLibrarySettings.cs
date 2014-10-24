namespace Sitecore.MobileSDK.MockObjects
{
  using Sitecore.MobileSDK.API.MediaItem;

  public class MutableMediaLibrarySettings : IMediaLibrarySettings
  {
    public MutableMediaLibrarySettings(
      string mediaLibraryRoot = "/sitecore/media library",
      string defaultMediaResourceExtension = "ashx",
      string mediaPrefix = "~/media",
      DownloadStrategy downloadStrategy = DownloadStrategy.Plain)
    {
      this.MediaLibraryRoot = mediaLibraryRoot;
      this.DefaultMediaResourceExtension = defaultMediaResourceExtension;
      this.MediaPrefix = mediaPrefix;
      this.MediaDownloadStrategy = downloadStrategy;
    }

    public IMediaLibrarySettings MediaSettingsShallowCopy()
    {
      return new MutableMediaLibrarySettings(
        this.MediaLibraryRoot,
        this.DefaultMediaResourceExtension,
        this.MediaPrefix,
        this.MediaDownloadStrategy);
    }

    public string MediaLibraryRoot
    {
      get;
      set;
    }

    public string DefaultMediaResourceExtension
    {
      get;
      set;
    }

    public string MediaPrefix
    {
      get;
      set;
    }

    public DownloadStrategy MediaDownloadStrategy
    {
      get;
      set;
    }
  }
}

