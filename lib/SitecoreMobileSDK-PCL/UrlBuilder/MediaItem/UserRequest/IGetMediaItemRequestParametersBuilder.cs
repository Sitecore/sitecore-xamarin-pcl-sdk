﻿
namespace Sitecore.MobileSDK
{
	using System;
	using Sitecore.MobileSDK.MediaItems;

	public interface IGetMediaItemRequestParametersBuilder<T>
		where T : class
	{
		IGetMediaItemRequestParametersBuilder<T> Database (string sitecoreDatabase);
		IGetMediaItemRequestParametersBuilder<T> Language (string itemLanguage);
		IGetMediaItemRequestParametersBuilder<T> Version (string itemVersion);

		IGetMediaItemRequestParametersBuilder<T> DownloadOptions (DownloadMediaOptions downloadMediaOptions);
		T Build();
	}
}

