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
    /// <returns><see cref="IMediaResourceDownloadRequest"/></returns>
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
    IDownloadMediaOptions DownloadOptions { get; }

    /// <summary>
    /// Gets the media item path.
    /// </summary>
    /// <value>
    /// The media item path.
    /// </value>
    string MediaPath { get; }
  }
}
