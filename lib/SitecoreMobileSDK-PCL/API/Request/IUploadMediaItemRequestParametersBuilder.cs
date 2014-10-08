namespace Sitecore.MobileSDK.API.Request.Parameters
{
  /// <summary>
  /// Interface represents fluent flow for building <see cref="IMediaResourceDownloadRequest"/>
  /// </summary>
  /// <typeparam name="T">Type of request that is build by this builder.</typeparam>
  public interface IUploadMediaItemRequestParametersBuilder<out T>
  where T : class
  {
    /// <summary>
    /// Specifies source database for media item.
    /// For example: "web".
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="database">Databse name.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> Database(string database);

    /// <summary>
    /// Specifies language for media item.
    /// For example: "en".
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="itemLanguage">Language name.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> Language(string itemLanguage);

    /// <summary>
    /// Specifies version of media item.
    /// For example: 1.
    /// </summary>
    /// <param name="itemVersion">Version number.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> Version(int? itemVersion);

    /// <summary>
    /// Specifies file's name, must include file extension.
    /// For example: image.jpg.
    /// </summary>
    /// <param name="fileName">File name.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> FileName(string fileName);

    /// <summary>
    /// Specifies items's name.
    /// For example: image.
    /// </summary>
    /// <param name="itemName">Item name.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> ItemName(string itemName);

    /// <summary>
    /// Sepcifies item's template path, relative to the default template's folder("/sitecore/templates").
    /// For example: System/Media/Unversioned/Image
    /// </summary>
    /// <param name="templatePath">The template path.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> ItemTemplatePath(string templatePath);

    /// <summary>
    /// Sepcifies folder path, relative to the media library folder.
    /// Must stars with the '/' symbol.
    /// </summary>
    /// <param name="path">Path to the folder.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> MediaPath(string path);

    /// <summary>
    /// Sepcifies the media item's file content type.
    /// For example: image/jpg
    /// </summary>
    /// <param name="contentType">Content type.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> ContentType(string contentType);

    /// <summary>
    /// Builds request for uploading media item.
    /// </summary>
    /// <returns>request</returns>
    T Build();
  }
}

