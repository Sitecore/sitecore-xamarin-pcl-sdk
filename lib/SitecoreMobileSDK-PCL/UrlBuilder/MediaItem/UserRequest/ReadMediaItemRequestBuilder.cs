namespace Sitecore.MobileSDK.UrlBuilder.MediaItem.UserRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.Validators;

  public class ReadMediaItemRequestBuilder : IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest>
  {
    public ReadMediaItemRequestBuilder(string mediaPath)
    {
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        mediaPath,
        this.GetType().Name + ".mediaPath"
      );
      this.mediaPath = mediaPath;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Database(string database)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.itemSourceAccumulator.Database,
        this.GetType().Name + ".database"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        database,
        this.GetType().Name + ".database"
      );

      this.itemSourceAccumulator = new ItemSourcePOD(
        database,
        this.itemSourceAccumulator.Language,
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Language(string itemLanguage)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.itemSourceAccumulator.Language,
        this.GetType().Name + ".itemLanguage"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        itemLanguage,
        this.GetType().Name + ".itemLanguage"
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
        this.GetType().Name + ".itemVersion"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        itemVersion,
        this.GetType().Name + ".itemVersion"
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
        this.GetType().Name + ".downloadMediaOptions"
      );

      MediaOptionsValidator.ValidateMediaOptions
      (
        downloadMediaOptions,
        this.GetType().Name + ".downloadMediaOptions"
      );


      this.downloadMediaOptions = (DownloadMediaOptions)downloadMediaOptions.DeepCopyMediaDownloadOptions();

      return this;
    }

    public IReadMediaItemRequest Build()
    {
      var result = new ReadMediaItemParameters(null, this.itemSourceAccumulator, this.downloadMediaOptions, this.mediaPath);
      return result;
    }

    protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD(null, null, null);
    protected IDownloadMediaOptions downloadMediaOptions = null;

    private string mediaPath;
  }
}

