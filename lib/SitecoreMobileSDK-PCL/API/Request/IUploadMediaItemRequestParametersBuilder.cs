
namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System.IO;

  /// <summary>
  /// Interface represents fluent flow for building <see cref="IMediaResourceDownloadRequest"/>
  /// </summary>
  /// <typeparam name="T">Type of request that is build by this builder.</typeparam>
  public interface IUploadMediaItemRequestParametersBuilder<out T>
  where T : class
  {
    /// <summary>
    /// Specifies resource data strem.
    /// </summary>
    /// <param name="itemDataStream">Data stream.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> ItemDataStream(Stream itemDataStream);

    /// <summary>
    /// Specifies source database for media item.
    /// For example: "web".
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="database">Database name.</param>
    /// <returns>this</returns>
    IUploadMediaItemRequestParametersBuilder<T> Database(string database);

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

