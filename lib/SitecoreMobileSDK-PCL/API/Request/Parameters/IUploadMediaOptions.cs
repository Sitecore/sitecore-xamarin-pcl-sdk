
namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System;
  using System.IO;

  /// <summary>
  /// Interface represents media options for upload resource request.
  /// <seealso cref="IMediaResourceUploadRequest"/>
  /// </summary>
  public interface IUploadMediaOptions
  {
    /// <summary>
    /// Specifies media resource data.
    /// </summary>
    /// <returns>Data Stream</returns>
    Stream ImageDataStream { get; }

    /// <summary>
    /// Specifies file's name, must include file extension.
    /// For example: image.jpg.
    /// </summary>
    /// <returns>File name</returns>
    string FileName { get; }

    /// <summary>
    /// Specifies items's name.
    /// For example: image.
    /// </summary>
    /// <returns>Item name</returns>
    string ItemName { get; }

    /// <summary>
    /// Sepcifies item's template path, relative to the default template's folder("/sitecore/templates").
    /// For example: System/Media/Unversioned/Image
    /// </summary>
    /// <returns>Template path</returns>
    string ItemTemplatePath { get; }

    /// <summary>
    /// Sepcifies folder path, relative to the media library folder.
    /// Must stars with the '/' symbol.
    /// </summary>
    /// <returns>Folder path</returns>
    string MediaPath { get; }

    /// <summary>
    /// Sepcifies the media item's file content type.
    /// For example: image/jpg
    /// </summary>
    /// <returns>Content type.</returns>
    string ContentType { get; }
  }
}

