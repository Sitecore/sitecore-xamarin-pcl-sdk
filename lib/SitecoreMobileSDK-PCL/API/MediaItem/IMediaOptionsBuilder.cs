namespace Sitecore.MobileSDK.API.MediaItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Interface represents builder for options for download resource request. 
  /// <seealso cref="IReadMediaItemRequest"/>
  /// </summary>
  public interface IMediaOptionsBuilder
  {
    /// <summary>
    /// Specifies width for media item.
    /// </summary>
    /// <param name="width">Target width. Must be > 0.</param>
    /// <returns>this</returns>
    IMediaOptionsBuilder Width(int width);

    /// <summary>
    /// Specifies height for media item.
    /// </summary>
    /// <param name="height">Target height. Must be > 0.</param>
    /// <returns>this</returns>
    IMediaOptionsBuilder Height(int height);

    /// <summary>
    /// Specifies max width for media item.
    /// </summary>
    /// <param name="maxWidth">Target max width. Must be > 0.</param>
    /// <returns>this</returns>
    IMediaOptionsBuilder MaxWidth(int maxWidth);

    /// <summary>
    /// Specifies max height for media item.
    /// </summary>
    /// <param name="maxHeight">Target max height. Must be > 0.</param>
    /// <returns>this</returns>
    IMediaOptionsBuilder MaxHeight(int maxHeight);

    /// <summary>
    /// Specifies background color for media item. 
    /// </summary>
    /// <param name="color">Target color.</param>
    /// <returns>this</returns>
    IMediaOptionsBuilder BackgroundColor(string color);

    /// <summary>
    /// Specifies whether to use media cache. 
    /// </summary>
    /// <param name="disable">To use cache.</param>
    /// <returns>this</returns>
    IMediaOptionsBuilder DisableMediaCache(bool disable);

    /// <summary>
    /// Specifies whether to allow strech. 
    /// </summary>
    /// <param name="allow">To allow strech.</param>
    /// <returns>this</returns>
    IMediaOptionsBuilder AllowStrech(bool allow);

    /// <summary>
    /// Specifies scale for media item. 
    /// </summary>
    /// <param name="scale">Target scale. Must be > 0</param>
    /// <returns>this</returns>
    IMediaOptionsBuilder Scale(float scale);

    /// <summary>
    /// Specifies whether to display as thumbnail. 
    /// </summary>
    /// <param name="displayAsThumbnail">To display as sumbnail.</param>
    /// <returns>this</returns>
    IMediaOptionsBuilder DisplayAsThumbnail(bool displayAsThumbnail);


    /// <summary>
    /// Builds media options. 
    /// </summary>
    /// <returns><see cref="IDownloadMediaOptions"/></returns>
    IDownloadMediaOptions Build();
  }
}

