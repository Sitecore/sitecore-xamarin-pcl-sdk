namespace Sitecore.MobileSDK.SessionSettings
{
  using Sitecore.MobileSDK.API;

  public class MediaLibrarySettings : IMediaLibrarySettings
  {
    public MediaLibrarySettings(
      string mediaLibraryRoot,
      string defaultMediaResourceExtension,
      string mediaPrefix)
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

  }
}

