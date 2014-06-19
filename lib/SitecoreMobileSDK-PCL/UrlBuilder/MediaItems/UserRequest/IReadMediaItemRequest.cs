
namespace Sitecore.MobileSDK.MediaItems
{
	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.SessionSettings;
	using Sitecore.MobileSDK.MediaItems;

	public interface IReadMediaItemRequest
	{
		IItemSource ItemSource { get; }
		ISessionConfig SessionSettings { get; }
		DownloadMediaOptions DownloadOptions { get; }
		string MediaItemPath { get; }
	}
}
