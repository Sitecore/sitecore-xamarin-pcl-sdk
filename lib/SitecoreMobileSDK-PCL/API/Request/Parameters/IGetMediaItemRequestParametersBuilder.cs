namespace Sitecore.MobileSDK.API.Request.Parameters
{
  /// <summary>
  /// Interface represents fluent flow for building <see cref="IMediaResourceDownloadRequest"/>
  /// </summary>
  /// <typeparam name="T">Type of request that is build by this builder.</typeparam>
  public interface IGetMediaItemRequestParametersBuilder<out T>
  where T : class
  {
    /// <summary>
    /// Specifies source database for media item.
    /// </summary>
    /// <param name="database">Databse name.</param>
    /// <returns>this</returns>
    IGetMediaItemRequestParametersBuilder<T> Database(string database);

    /// <summary>
    /// Specifies language for media item.
    /// </summary>
    /// <param name="itemLanguage">Language name.</param>
    /// <returns>this</returns>
    IGetMediaItemRequestParametersBuilder<T> Language(string itemLanguage);

    /// <summary>
    /// Specifies version of media item.
    /// </summary>
    /// <param name="itemVersion">Version number. If null - latest version will be used.</param>
    /// <returns>this</returns>
    IGetMediaItemRequestParametersBuilder<T> Version(int? itemVersion);

    /// <summary>
    /// Specifies media options for media item.
    /// <see cref="IDownloadMediaOptions"/>
    /// </summary>
    /// <param name="downloadMediaOptions">Media options.</param>
    /// <returns>this</returns>
    IGetMediaItemRequestParametersBuilder<T> DownloadOptions(IDownloadMediaOptions downloadMediaOptions);

    /// <summary>
    /// Builds request for dowloading media item.
    /// </summary>
    /// <returns>request</returns>
    T Build();
  }
}

