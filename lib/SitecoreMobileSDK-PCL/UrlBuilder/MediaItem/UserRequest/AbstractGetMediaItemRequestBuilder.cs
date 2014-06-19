﻿

namespace Sitecore.MobileSDK
{
	using System.Collections.Generic;

	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.MediaItems;
	using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


	public abstract class AbstractGetMediaItemRequestBuilder<T> : IGetMediaItemRequestParametersBuilder<T>
		where T : class
	{
		public IGetMediaItemRequestParametersBuilder<T> Database (string sitecoreDatabase)
		{
			this.itemSourceAccumulator = new ItemSourcePOD (
				sitecoreDatabase, 
				this.itemSourceAccumulator.Language, 
				this.itemSourceAccumulator.Version);

			return this;
		}

		public IGetMediaItemRequestParametersBuilder<T> Language (string itemLanguage)
		{
			this.itemSourceAccumulator = new ItemSourcePOD (
				this.itemSourceAccumulator.Database, 
				itemLanguage, 
				this.itemSourceAccumulator.Version);

			return this;
		}

		public IGetMediaItemRequestParametersBuilder<T> Version (string itemVersion)
		{
			this.itemSourceAccumulator = new ItemSourcePOD (
				this.itemSourceAccumulator.Database, 
				this.itemSourceAccumulator.Language,
				itemVersion);

			return this;
		}

		public IGetMediaItemRequestParametersBuilder<T> DownloadOptions (DownloadMediaOptions downloadMediaOptions)
		{
			//TODO:!!! make downloadMediaOptions object copy here !!!
			this.downloadMediaOptions = downloadMediaOptions;

			return this;
		}
			
		public abstract T Build();

		protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD( null, null, null );
		protected DownloadMediaOptions downloadMediaOptions = null;
	}
}

