
namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

	public interface IReadMediaItemRequest
	{
		IItemSource ItemSource { get; }
		ISessionConfig SessionSettings { get; }
    IDownloadMediaOptions DownloadOptions { get; }
		string MediaPath { get; }
	}
}
