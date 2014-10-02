namespace Sitecore.MobileSDK.API.MediaItem
{
  /// <summary>
  /// Interface represents settings that specifies the rules to build media requests. 
  /// </summary>
  public interface IMediaLibrarySettings
  {
    /// <summary>
    /// Performs shallow copy of media setting.
    /// </summary>
    /// <returns>
    ///   <seealso cref="IMediaLibrarySettings" />
    /// </returns>
    IMediaLibrarySettings MediaSettingsShallowCopy();

    /// <summary>
    /// Specifies path to media library root to build download media resources request.
    /// By default: '/sitecore/media library'.
    /// </summary>
    string MediaLibraryRoot
    {
      get;
    }

    /// <summary>
    /// Specifies default extension to build download media resources request.
    /// By default: 'ashx'.
    /// </summary>
    string DefaultMediaResourceExtension
    {
      get;
    }

    /// <summary>
    /// Specifies hook string to build download media resources request.
    /// By default: '~/media'.
    /// </summary>
    string MediaPrefix
    {
      get;
    }

    DownloadStrategy MediaDownloadStrategy
    {
      get;
    }
  }
}

