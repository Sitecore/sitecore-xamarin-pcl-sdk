namespace Sitecore.MobileSDK.SessionSettings
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.MediaItem;


  public class MediaLibrarySettings : IMediaLibrarySettings
  {
    public MediaLibrarySettings(
      string mediaLibraryRoot = "/sitecore/media library",
      string defaultMediaResourceExtension = "ashx",
      string mediaPrefix = "~/media")
    {
      this.MediaLibraryRoot = mediaLibraryRoot;
      this.DefaultMediaResourceExtension = defaultMediaResourceExtension;
      this.MediaPrefix = mediaPrefix;
    }

    public IMediaLibrarySettings MediaSettingsShallowCopy()
    {
      return new MediaLibrarySettings(
        this.MediaLibraryRoot,
        this.DefaultMediaResourceExtension,
        this.MediaPrefix);
    }

    public string MediaLibraryRoot
    {
      get;
      private set;
    }

    public string DefaultMediaResourceExtension
    {
      get;
      private set;
    }

    public string MediaPrefix
    {
      get;
      private set;
    }

    public DownloadStrategy MediaDownloadStrategy
    {
      get;
      private set;
    }
  }
}

