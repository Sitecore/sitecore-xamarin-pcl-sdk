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
    /// Performs deep copy of <see cref="IDownloadMediaOptions"/>
    /// </summary>
    /// <returns><see cref="IDownloadMediaOptions"/></returns>
    IDownloadMediaOptions DeepCopyMediaDownloadOptions();

    /// <summary>
    ///  Determines whether any of property of <see cref="IDownloadMediaOptions" /> was set.
    /// </summary>
    /// <returns>true if any proprty is set, false otherwise</returns>
    bool IsEmpty { get; }

    /// <summary>
    ///  Returns width.
    /// </summary>
    /// <returns>width</returns>
    string Width { get; }

    /// <summary>
    ///  Returns height.
    /// </summary>
    /// <returns>height</returns>
    string Height { get; }

    /// <summary>
    ///  Returns max width.
    /// </summary>
    /// <returns>max width</returns>
    string MaxWidth { get; }

    /// <summary>
    ///  Returns max height.
    /// </summary>
    /// <returns>max height</returns>
    string MaxHeight { get; }

    /// <summary>
    ///  Returns backgorund color.
    /// </summary>
    /// <returns>backgorund color</returns>
    string BackgroundColor { get; }

    /// <summary>
    ///  Determines whether option "media cache" is set.
    /// </summary>
    /// <returns>1 if true or 0 if false</returns>
    string DisableMediaCache { get; }

    /// <summary>
    ///  Determines whether option "allow strech" is set.
    /// </summary>
    /// <returns>1 if true or 0 if false</returns>
    string AllowStrech { get; }

    /// <summary>
    ///  Returns scale.
    /// </summary>
    /// <returns>scale</returns>
    string Scale { get; }

    /// <summary>
    ///  Determines whether option "display as thumbnail" is set.
    /// </summary>
    /// <returns>max width</returns>
    string DisplayAsThumbnail { get; }

    /// <summary>
    ///  Returns <see cref="Dictionary{TKey,TValue}"/> that contains key value pairs where : 
    ///  key - option name,
    ///  value - options value;
    /// </summary>
    /// <returns><see cref="Dictionary{TKey,TValue}"/></returns>
    Dictionary<string, string> OptionsDictionary { get; }
  }
}

