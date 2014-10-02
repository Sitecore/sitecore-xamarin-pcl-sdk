namespace Sitecore.MobileSDK.API.Request
{
  using System;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  public interface IMediaResourceUploadRequest
  {
    IMediaResourceUploadRequest DeepCopyUploadMediaRequest();

    /// <summary>
    /// Gets the item source.
    /// </summary>
    /// <value>
    /// The item source.
    /// </value>
    /// <seealso cref="IItemSource" />
    IItemSource ItemSource { get; }

    /// <summary>
    /// Gets the session settings.
    /// </summary>
    /// <value>
    /// The session settings.
    /// </value>
    /// <seealso cref="ISessionConfig" />
    ISessionConfig SessionSettings { get; }

    CreateMediaParameters CreateMediaParameters { get; }
  }
}

