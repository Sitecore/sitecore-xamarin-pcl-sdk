namespace MobileSDKUnitTest.Mock
{
  using System;
  using Sitecore.MobileSDK.API;

  public class MutableMediaLibrarySettings : IMediaLibrarySettings
  {
    public MutableMediaLibrarySettings(
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
      return new MutableMediaLibrarySettings(
        this.MediaLibraryRoot,
        this.DefaultMediaResourceExtension,
        this.MediaPrefix);
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
  }
}

