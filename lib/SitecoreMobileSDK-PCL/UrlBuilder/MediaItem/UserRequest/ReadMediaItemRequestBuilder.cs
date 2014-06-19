﻿

namespace Sitecore.MobileSDK
{
	using System;
	using Sitecore.MobileSDK.UrlBuilder;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

	public class ReadMediaItemRequestBuilder : AbstractGetMediaItemRequestBuilder<IReadMediaItemRequest>
	{
		public ReadMediaItemRequestBuilder(string itemPath)
		{
			MediaItemPathValidator.ValidateMediaItemPath(itemPath);

			this.mediaItemPath = itemPath;
		}

		public override IReadMediaItemRequest Build()
		{
			var result = new ReadMediaItemParameters(null, this.itemSourceAccumulator, this.downloadMediaOptions, this.mediaItemPath);
			return result;
		}

		private string mediaItemPath;
	}
}

