namespace Sitecore.MobileSDK.API.MediaItem
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// A builder for dynamic media options.
  /// They are used for size and colour configuration of the image being downloaded. 
  /// 
  /// These options are a part of the <seealso cref="IMediaResourceDownloadRequest"/>
  /// </summary>
  public interface IMediaOptionsBuilder
  {
    /// <summary>
    /// Specifies width for media item.
    /// </summary>
    /// <param name="width"> Target width. 
    /// It must be a positive number.
    /// </param>
    /// <returns>this</returns>
    IMediaOptionsBuilder Width(int width);

    /// <summary>
    /// Specifies height for media item.
    /// </summary>
    /// <param name="height">Target height. 
    /// It must be a positive number.
    /// </param>
    /// <returns>this</returns>
    IMediaOptionsBuilder Height(int height);

    /// <summary>
    /// Specifies max width for media item.
    /// </summary>
    /// <param name="maxWidth">Target max width.
    /// It must be a positive number.
    /// </param>
    /// <returns>this</returns>
    IMediaOptionsBuilder MaxWidth(int maxWidth);

    /// <summary>
    /// Specifies max height for media item.
    /// </summary>
    /// <param name="maxHeight">Target max height.
    /// It must be a positive number.
    /// </param>
    /// <returns>this</returns>
    IMediaOptionsBuilder MaxHeight(int maxHeight);

    /// <summary>
    /// Specifies background color for media item. 
    /// </summary>
    /// <param name="color">Target color. Should be an HTML colorName or hex color code
    /// For example: "Red" or "ff0000"
    /// 
    /// The value is case insensitive.
    /// </param>
    /// 
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
    /// <param name="scale">Target scale.
    /// It must be a positive floating point number.
    /// </param>
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

