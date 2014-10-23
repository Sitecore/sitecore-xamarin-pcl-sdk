using System;
using System.IO;

namespace LargeUploadTestiOS
{
  public interface IChunkedRequest
  {
    /// <summary>
    /// Specifies media resource data.
    /// </summary>
    /// <returns>Data Stream</returns>
    Stream DataStream { get; }

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
    /// Sepcifies the media item's file content type.
    /// For example: image/jpg
    /// </summary>
    /// <returns>Content type.</returns>
    string ContentType { get; }

    string RequestUrl { get; }
  }
}

