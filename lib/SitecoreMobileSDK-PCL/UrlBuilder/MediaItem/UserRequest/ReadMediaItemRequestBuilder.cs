namespace Sitecore.MobileSDK.UrlBuilder.MediaItem.UserRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class ReadMediaItemRequestBuilder : IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest>
  {
    public ReadMediaItemRequestBuilder(string mediaPath)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(mediaPath, this.GetType().Name + ".MediaPath");
      
      this.mediaPath = mediaPath;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Database(string database)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Database, this.GetType().Name + ".Database");

      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(database, this.GetType().Name + ".Database");
      
      this.itemSourceAccumulator = new ItemSourcePOD(
        database,
        this.itemSourceAccumulator.Language,
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Language(string itemLanguage)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Language, this.GetType().Name + ".Language");

      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemLanguage, this.GetType().Name + ".Language");

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        itemLanguage,
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Version(string itemVersion)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Version, this.GetType().Name + ".Version");

      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemVersion, this.GetType().Name + ".Version");

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> DownloadOptions(IDownloadMediaOptions downloadMediaOptions)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.downloadMediaOptions, this.GetType().Name + ".DownloadMediaOptions");

      BaseValidator.ValidateMediaOptionsOrThrow(downloadMediaOptions, this.GetType().Name + ".DownloadMediaOptions");

      this.downloadMediaOptions = downloadMediaOptions.DeepCopyMediaDownloadOptions();

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

