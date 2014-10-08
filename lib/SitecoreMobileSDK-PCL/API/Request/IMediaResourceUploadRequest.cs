namespace Sitecore.MobileSDK.API.Request
{
  using System;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  /// <summary>
  /// Interface represents data set for uploading resource.
  /// </summary>
  public interface IMediaResourceUploadRequest : IBaseItemRequest
  {
    /// <summary>
    /// Performs deep copy of request.
    /// </summary>
    /// <returns></returns>
    IMediaResourceUploadRequest DeepCopyUploadMediaRequest();

    /// <summary>
    /// Gets the upload options.
    /// </summary>
    /// <seealso cref="IUploadMediaOptions" />
    /// <returns><seealso cref="IUploadMediaOptions"/></returns>
    UploadMediaOptions UploadOptions { get; }
  }
}

