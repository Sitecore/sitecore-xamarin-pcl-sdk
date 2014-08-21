namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System.Collections.Generic;

  /// <summary>
  /// Interface represents media options for download resource request.
  /// <seealso cref="IMediaResourceDownloadRequest"/>
  /// </summary>
  public interface IDownloadMediaOptions
  {
    /// <summary>
    /// Performs deep copy of <seealso cref="IDownloadMediaOptions"/>
    /// </summary>
    /// <returns><seealso cref="IDownloadMediaOptions"/></returns>
    IDownloadMediaOptions DeepCopyMediaDownloadOptions();

    /// <summary>
    ///  Determines whether any of property of <see cref="IDownloadMediaOptions" /> was set.
    /// </summary>
    /// <returns>true if any property is set, false otherwise</returns>
    bool IsEmpty { get; }

    /// <summary>
    /// Specifies width for media item.
    /// </summary>
    /// <param name="width"> Target width. 
    /// It must be a positive number.
    /// </param>
    /// <returns>this</returns>
    string Width { get; }

    /// <summary>
    /// Specifies height for media item.
    /// </summary>
    /// <param name="height">Target height. 
    /// It must be a positive number.
    /// </param>
    /// <returns>this</returns>
    string Height { get; }

    /// <summary>
    /// Specifies max width for media item.
    /// </summary>
    /// <param name="maxWidth">Target max width.
    /// It must be a positive number.
    /// </param>
    /// <returns>this</returns>
    string MaxWidth { get; }

    /// <summary>
    /// Specifies max height for media item.
    /// </summary>
    /// <param name="maxHeight">Target max height.
    /// It must be a positive number.
    /// </param>
    /// <returns>this</returns>
    string MaxHeight { get; }

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
    string BackgroundColor { get; }

    /// <summary>
    /// Specifies whether to use media cache. 
    /// </summary>
    /// <param name="disable">To use cache.</param>
    /// <returns>this</returns>
    string DisableMediaCache { get; }

    /// <summary>
    /// Specifies whether to allow strech. 
    /// </summary>
    /// <param name="allow">To allow strech.</param>
    /// <returns>this</returns>
    string AllowStrech { get; }

    /// <summary>
    /// Specifies scale for media item. 
    /// </summary>
    /// <param name="scale">Target scale.
    /// It must be a positive floating point number.
    /// </param>
    /// <returns>this</returns>
    string Scale { get; }

    /// <summary>
    /// Specifies whether to display as thumbnail. 
    /// </summary>
    /// <param name="displayAsThumbnail">To display as sumbnail.</param>
    /// <returns>this</returns>
    string DisplayAsThumbnail { get; }

    /// <summary>
    ///  Returns <see cref="Dictionary{TKey,TValue}"/> that contains key value pairs where : 
    ///  key - option name,
    ///  value - options value;
    /// 
    /// key value must equals to appropriate request's parameter key:
    /// "w" - width
    /// "h" - height
    /// "mw" - max width
    /// "mh" - max height
    /// "as" - allow strech
    /// "dmc" - disable media cache
    /// "sc" - scale
    /// "thn" - display as thumbnail
    /// "bc" - background color
    /// </summary>
    /// <returns><seealso cref="Dictionary{TKey,TValue}"/></returns>
    Dictionary<string, string> OptionsDictionary { get; }
  }
}

