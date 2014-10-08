
namespace Sitecore.MobileSDK.Items
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  public class MediaResourceUploadParameters : IMediaResourceUploadRequest
  {
    public MediaResourceUploadParameters
    (
      ISessionConfig sessionSettings,
      IItemSource itemSource,
      UploadMediaOptions uploadOptions
    )
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.UploadOptions = uploadOptions;
    }

    public virtual IMediaResourceUploadRequest DeepCopyUploadMediaRequest()
    {
      ISessionConfig connection = null;
      IItemSource itemSource = null;

      if (null != this.SessionSettings)
      {
        connection = this.SessionSettings.SessionConfigShallowCopy();
      }

      if (null != this.ItemSource)
      {
        itemSource = this.ItemSource.ShallowCopy();
      }

      UploadMediaOptions createMediaParameters = this.UploadOptions.ShallowCopy();

      return new MediaResourceUploadParameters(connection, itemSource, createMediaParameters);
    }

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyUploadMediaRequest();
    }
      
    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public UploadMediaOptions UploadOptions { get; private set; }

    public IQueryParameters QueryParameters { get; private set; }
  }
}
