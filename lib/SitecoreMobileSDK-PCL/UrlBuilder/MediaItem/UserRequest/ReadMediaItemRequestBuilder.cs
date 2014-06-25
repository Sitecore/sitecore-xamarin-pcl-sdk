

namespace Sitecore.MobileSDK
{
	using System;
  using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.UrlBuilder;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  public class ReadMediaItemRequestBuilder : IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest>
	{

    public ReadMediaItemRequestBuilder(string itemPath)
    {
      MediaItemPathValidator.ValidateMediaItemPath(itemPath);

      this.mediaItemPath = itemPath;
    }
      
    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Database (string sitecoreDatabase)
    {
      this.itemSourceAccumulator = new ItemSourcePOD (
        sitecoreDatabase, 
        this.itemSourceAccumulator.Language, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Language (string itemLanguage)
    {
      this.itemSourceAccumulator = new ItemSourcePOD (
        this.itemSourceAccumulator.Database, 
        itemLanguage, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Version (string itemVersion)
    {
      this.itemSourceAccumulator = new ItemSourcePOD (
        this.itemSourceAccumulator.Database, 
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> DownloadOptions (IDownloadMediaOptions downloadMediaOptions)
    {
      this.downloadMediaOptions = (DownloadMediaOptions)downloadMediaOptions.ShallowCopy();

      return this;
    }
      
		
		public IReadMediaItemRequest Build()
		{
			var result = new ReadMediaItemParameters(null, this.itemSourceAccumulator, this.downloadMediaOptions, this.mediaItemPath);
			return result;
		}

    protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD( null, null, null );
    protected IDownloadMediaOptions downloadMediaOptions = null;

		private string mediaItemPath;
	}
}

