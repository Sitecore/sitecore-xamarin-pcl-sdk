namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface IMediaResourceDownloadRequest
  {
    IMediaResourceDownloadRequest DeepCopyReadMediaRequest();

    IItemSource ItemSource { get; }
    ISessionConfig SessionSettings { get; }
    IDownloadMediaOptions DownloadOptions { get; }
    string MediaPath { get; }
  }
}
