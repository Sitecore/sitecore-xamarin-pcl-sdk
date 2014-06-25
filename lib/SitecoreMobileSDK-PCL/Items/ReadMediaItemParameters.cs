﻿
namespace Sitecore.MobileSDK
{
	using Sitecore.MobileSDK.UrlBuilder;
	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

	public class ReadMediaItemParameters : IReadMediaItemRequest
	{
		public ReadMediaItemParameters(
			ISessionConfig sessionSettings,
			IItemSource itemSource, 
      IDownloadMediaOptions downloadOptions,
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

    public IDownloadMediaOptions DownloadOptions { get; private set; }
	}
}