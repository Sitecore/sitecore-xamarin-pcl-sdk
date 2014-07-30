namespace Sitecore.MobileSDK.UrlBuilder.MediaItem.UserRequest
{
  using System;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class ReadMediaItemRequestBuilder : IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest>
  {
    public ReadMediaItemRequestBuilder(string mediaPath)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(mediaPath, this.GetType().Name + ".mediaPath");
      
      this.mediaPath = mediaPath;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Database(string database)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Database, this.GetType().Name + ".database");

      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(database, this.GetType().Name + ".database");
      
      this.itemSourceAccumulator = new ItemSourcePOD(
        database,
        this.itemSourceAccumulator.Language,
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Language(string itemLanguage)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Language, this.GetType().Name + ".itemLanguage");

      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemLanguage, this.GetType().Name + ".itemLanguage");

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        itemLanguage,
        this.itemSourceAccumulator.Version);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> Version(string itemVersion)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Version, this.GetType().Name + ".itemVersion");

      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemVersion, this.GetType().Name + ".itemVersion");

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IReadMediaItemRequest> DownloadOptions(IDownloadMediaOptions downloadMediaOptions)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.downloadMediaOptions, this.GetType().Name + ".downloadMediaOptions");

      if (MediaOptionsValidator.IsValidMediaOptions(downloadMediaOptions))
      {
        throw new ArgumentException(this.GetType().Name + ".downloadMediaOptions : is not valid");
      }

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

