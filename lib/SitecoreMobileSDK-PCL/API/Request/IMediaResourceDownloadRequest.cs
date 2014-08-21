namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Interface represents data set for downloading resource.
  /// </summary>
  public interface IMediaResourceDownloadRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns><seealso cref="IMediaResourceDownloadRequest"/></returns>
    IMediaResourceDownloadRequest DeepCopyReadMediaRequest();

    /// <summary>
    /// Gets the item source.
    /// </summary>
    /// <value>
    /// The item source.
    /// </value>
    /// <seealso cref="IItemSource" />
    IItemSource ItemSource { get; }

    /// <summary>
    /// Gets the session settings.
    /// </summary>
    /// <value>
    /// The session settings.
    /// </value>
    /// <seealso cref="ISessionConfig" />
    ISessionConfig SessionSettings { get; }

    /// <summary>
    /// Gets the download options.
    /// </summary>
    /// <value>
    /// The download options.
    /// </value>
    /// <seealso cref="IDownloadMediaOptions" />
    IDownloadMediaOptions DownloadOptions { get; }

    /// <summary>
    /// Gets the media item path, relative to media library rootю
    /// Must starts with '/' symbol.
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <value>
    /// The media item path.
    /// </value>
    string MediaPath { get; }
  }
}
