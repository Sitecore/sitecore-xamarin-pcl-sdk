

namespace Sitecore.MobileSDK
{
	using Sitecore.MobileSDK.UrlBuilder;
	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.SessionSettings;
	using Sitecore.MobileSDK.MediaItems;

	public class ReadMediaItemParameters : IReadMediaItemRequest
	{
		public ReadMediaItemParameters(
			ISessionConfig sessionSettings,
			IItemSource itemSource, 
			DownloadMediaOptions downloadOptions,
			string mediaItemPath)
		{
			this.SessionSettings = sessionSettings;
			this.ItemSource = itemSource;
			this.MediaItemPath = mediaItemPath;
			this.DownloadOptions = downloadOptions;
		}

		public string MediaItemPath { get; private set; }

		public IItemSource ItemSource { get; private set; }

		public ISessionConfig SessionSettings { get; private set; }

		public DownloadMediaOptions DownloadOptions { get; private set; }
	}
}
