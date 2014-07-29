namespace Sitecore.MobileSDK.API
{
  public interface IMediaLibrarySettings
  {
    IMediaLibrarySettings MediaSettingsShallowCopy();

    string MediaLibraryRoot
    {
      get;
    }

    string DefaultMediaResourceExtension
    {
      get;
    }

    string MediaPrefix
    {
      get;
    }
  }
}

