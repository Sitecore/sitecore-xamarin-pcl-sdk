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
    /// Specifies file's name, must include file resolution.
    /// For example: image.jpg.
    /// </summary>
    /// <param name="fileName">File name.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> FileName(string fileName);

    IUploadMediaItemRequestParametersBuilder<T> ItemName(string itemName);

    IUploadMediaItemRequestParametersBuilder<T> ItemTemplate(string itemName);

    IUploadMediaItemRequestParametersBuilder<T> MediaPath(string path);

    IUploadMediaItemRequestParametersBuilder<T> ContentType(string contentType);

    /// <summary>
    /// Builds request for dowloading media item.
    /// </summary>
    /// <returns>request</returns>
    T Build();
  }
}

