

namespace Sitecore.MobileSDK
{
	using System.Collections.Generic;

	using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
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

    public IGetMediaItemRequestParametersBuilder<T> DownloadOptions (IDownloadMediaOptions downloadMediaOptions)
		{
      this.downloadMediaOptions = (DownloadMediaOptions)downloadMediaOptions.ShallowCopy();

			return this;
		}
			
		public abstract T Build();

		protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD( null, null, null );
    protected IDownloadMediaOptions downloadMediaOptions = null;
	}
}

