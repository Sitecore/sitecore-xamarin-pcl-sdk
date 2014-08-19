namespace Sitecore.MobileSDK.API
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
    ///   <see cref="IMediaLibrarySettings" />
    /// </returns>
    IMediaLibrarySettings MediaSettingsShallowCopy();

    /// <summary>
    /// Specifies path to media library root, for example '/sitecore/media library'.
    /// </summary>
    string MediaLibraryRoot
    {
      get;
    }

    /// <summary>
    /// Default extension for media resources, for example 'ashx'.
    /// </summary>
    string DefaultMediaResourceExtension
    {
      get;
    }

    /// <summary>
    /// Hook to build request for resource download, for example '~/media'.
    /// </summary>
    string MediaPrefix
    {
      get;
    }
  }
}

