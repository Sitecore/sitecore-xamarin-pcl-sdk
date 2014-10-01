
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
      CreateMediaParameters createMediaParameters
    )
    {
      this.SessionSettings = sessionSettings;
      this.ItemSource = itemSource;
      this.CreateMediaParameters = createMediaParameters;
    }

    public virtual IMediaResourceDownloadRequest DeepCopyUploadMediaRequest()
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

      return new MediaResourceUploadParameters(connection, itemSource, this.ImageData, this.ImageName);
    }
      
    public IItemSource ItemSource { get; private set; }

    public ISessionConfig SessionSettings { get; private set; }

    public CreateMediaParameters CreateMediaParameters { get; private set; }
  }
}
