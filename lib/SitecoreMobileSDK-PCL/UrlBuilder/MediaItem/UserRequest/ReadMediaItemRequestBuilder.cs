namespace Sitecore.MobileSDK
{
	using System;

  using Sitecore.MobileSDK.API;
	using Sitecore.MobileSDK.API.Request;
	using Sitecore.MobileSDK.API.Request.Parameters;

	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.UrlBuilder;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.Validators;


  public class ReadMediaItemRequestBuilder : IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest>
	{
    public ReadMediaItemRequestBuilder(string itemPath)
    {
      this.mediaPath = itemPath;
    }
      
    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Database(string sitecoreDatabase)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.itemSourceAccumulator.Database,
        "[ReadMediaItemRequestBuilder.Database] the property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        sitecoreDatabase, 
        "[ReadMediaItemRequestBuilder.Database] the value cannot be null or empty"
      );

      this.itemSourceAccumulator = new ItemSourcePOD(
        sitecoreDatabase, 
        this.itemSourceAccumulator.Language, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Language(string itemLanguage)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.itemSourceAccumulator.Language,
        "[ReadMediaItemRequestBuilder.Language] the property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        itemLanguage, 
        "[ReadMediaItemRequestBuilder.Language] the value cannot be null or empty"
      );

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database, 
        itemLanguage, 
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Version(string itemVersion)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.itemSourceAccumulator.Version,
        "[ReadMediaItemRequestBuilder.Version] the property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        itemVersion, 
        "[ReadMediaItemRequestBuilder.Version] the value cannot be null or empty"
      );


      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database, 
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> DownloadOptions(IDownloadMediaOptions downloadMediaOptions)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.downloadMediaOptions,
        "[ReadMediaItemRequestBuilder.DownloadOptions] the property cannot be assigned twice"
      );

      MediaOptionsValidator.ValidateMediaOptions
      (
        downloadMediaOptions,
        "[ReadMediaItemRequestBuilder.DownloadOptions] the value cannot be null or empty"
      );


      this.downloadMediaOptions = (DownloadMediaOptions)downloadMediaOptions.DeepCopyMediaDownloadOptions();

      return this;
    }

		public IReadMediaItemRequest Build()
		{
			var result = new ReadMediaItemParameters(null, this.itemSourceAccumulator, this.downloadMediaOptions, this.mediaPath);
			return result;
		}

    protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD( null, null, null );
    protected IDownloadMediaOptions downloadMediaOptions = null;

		private string mediaPath;
	}
}

