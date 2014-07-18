
namespace Sitecore.MobileSDK.API.Request
{
    using Sitecore.MobileSDK.API.Request.Parameters;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder.MediaItem;

    public interface IReadMediaItemRequest
	{
    IReadMediaItemRequest DeepCopyReadMediaRequest();

		IItemSource ItemSource { get; }
		ISessionConfig SessionSettings { get; }
    IDownloadMediaOptions DownloadOptions { get; }
		string MediaPath { get; }
	}
}
