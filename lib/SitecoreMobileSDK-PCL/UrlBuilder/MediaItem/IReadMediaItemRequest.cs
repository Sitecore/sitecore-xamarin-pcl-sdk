
namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

	public interface IReadMediaItemRequest
	{
    IReadMediaItemRequest DeepCopyReadMediaRequest();

		IItemSource ItemSource { get; }
		ISessionConfig SessionSettings { get; }
    IDownloadMediaOptions DownloadOptions { get; }
		string MediaItemPath { get; }
	}
}
