namespace Sitecore.MobileSDK.API.Request
{
  using System;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  public interface IMediaResourceUploadRequest : IBaseItemRequest
  {
    IMediaResourceUploadRequest DeepCopyUploadMediaRequest();

    CreateMediaParameters CreateMediaParameters { get; }
  }
}

