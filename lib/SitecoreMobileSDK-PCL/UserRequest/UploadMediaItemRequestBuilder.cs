namespace Sitecore.MobileSDK.UserRequest
{
  using System.IO;

  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class UploadMediaItemRequestBuilder : IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest>
  {
    public UploadMediaItemRequestBuilder(Stream itemDataStream)
    {
      this.itemDataStream = itemDataStream;
    }

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> Database(string database)
    {
      if (string.IsNullOrEmpty(database))
      {
        return this;
      }

      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Database, this.GetType().Name + ".Database");
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(database, this.GetType().Name + ".Database");

      this.itemSourceAccumulator = new ItemSourcePOD(
        database,
        this.itemSourceAccumulator.Language,
        this.itemSourceAccumulator.VersionNumber);

      return this;
    }

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> Language(string itemLanguage)
    {
      if (string.IsNullOrEmpty(itemLanguage))
      {
        return this;
      }

      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Language, this.GetType().Name + ".Language");

      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemLanguage, this.GetType().Name + ".Language");

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        itemLanguage,
        this.itemSourceAccumulator.VersionNumber);

      return this;
    }

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> Version(int? itemVersion)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.VersionNumber, this.GetType().Name + ".Version");
      BaseValidator.AssertPositiveNumber(itemVersion, this.GetType().Name + ".Version");

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    //TODO: @igk add parameters value checking
    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> FileName(string fileName)
    {
      this.fileName = fileName;
      return this;
    }

    //TODO: @igk add parameters value checking
    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ItemName(string itemName)
    {
      this.itemName = itemName;
      return this;
    }

    //TODO: @igk add parameters value checking
    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ItemTemplate(string itemTemplate)
    {
      this.itemTemplate = itemTemplate;
      return this;
    }

    //TODO: @igk add parameters value checking
    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> MediaPath(string path)
    {
      this.mediaPath = path;
      return this;
    }

    //TODO: @igk add parameters value checking
    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ContentType(string contentType)
    {
      this.contentType = contentType;
      return this;
    }

    public IMediaResourceUploadRequest Build()
    {
      CreateMediaParameters createMediaParameters = new CreateMediaParameters (
        this.itemDataStream, 
        this.fileName,
        this.itemName,
        this.itemTemplate, 
        this.mediaPath,
        this.contentType
      ); 
      var result = new MediaResourceUploadParameters(null, this.itemSourceAccumulator, createMediaParameters);
      return result;
    }

    protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD(null, null, null);

    private Stream itemDataStream;
    private string fileName;
    private string itemName;
    private string itemTemplate;
    private string mediaPath;
    private string contentType;
  }
}

