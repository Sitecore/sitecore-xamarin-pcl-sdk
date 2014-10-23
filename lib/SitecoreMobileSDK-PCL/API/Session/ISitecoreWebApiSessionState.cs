using Sitecore.MobileSDK.API.MediaItem;

namespace Sitecore.MobileSDK.API.Session
{
  using System;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.PasswordProvider;

  /// <summary>
  /// Interface represents session state.
  /// </summary>
  public interface ISitecoreWebApiSessionState : IDisposable
  {
    /// <summary>
    /// Gets the default settings: database name, language and item version number.
    /// </summary>
    /// <value>
    /// The default source.
    /// </value>
    /// <seealso cref="IItemSource" />
    IItemSource DefaultSource { get; }

    /// <summary>
    /// Gets the session settings: instance url, site, web API version.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    /// <seealso cref="ISessionConfig"/>
    ISessionConfig Config { get; }

    /// <summary>
    /// Gets the credentials: username and password.
    /// </summary>
    /// <value>
    /// The credentials.
    /// </value>
    /// <seealso cref="IWebApiCredentials" />
    IWebApiCredentials Credentials { get; }

    /// <summary>
    /// Gets the settings : 
    /// 
    /// * Path of the media library root ("/Sitecore/Media Library")
    /// * Default extension for media files ("ashx")
    /// * media hook prefix ("~/media")
    /// * Image resizing service to use
    /// 
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    /// <seealso cref="IMediaLibrarySettings"/>
    IMediaLibrarySettings MediaLibrarySettings { get; }
  }
}

