namespace Sitecore.MobileSDK.UserRequest
{
  using System.IO;

  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class UploadMediaItemByParentPathRequestBuilder : IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest>
  {
    public UploadMediaItemByParentPathRequestBuilder(string parentPath)
    {
      this.mediaPath = parentPath;
    }

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ItemDataStream(Stream itemDataStream)
    {
      BaseValidator.CheckNullAndThrow (itemDataStream, "UploadMediaItemRequestParametersBuilder.ItemDataStream is required");
      BaseValidator.CheckForTwiceSetAndThrow(this.itemDataStream, this.GetType().Name + ".itemDataStream");
      this.itemDataStream = itemDataStream;
      return this;
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

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> FileName(string fileName)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow (fileName, "UploadMediaItemRequestParametersBuilder.FileName is required");
      BaseValidator.CheckForTwiceSetAndThrow(this.fileName, this.GetType().Name + ".fileName");
      this.fileName = fileName;
      return this;
    }

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ItemName(string itemName)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemName, "UploadMediaItemRequestParametersBuilder.ItemName is required");
      BaseValidator.CheckForTwiceSetAndThrow(this.itemName, this.GetType().Name + ".itemName");
      this.itemName = itemName;
      return this;
    }

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ItemTemplatePath(string templatePath)
    {
      ItemPathValidator.ValidateItemTemplate(templatePath, this.GetType().Name + ".templatePath");
      BaseValidator.CheckForTwiceSetAndThrow(this.itemTemplate, this.GetType().Name + ".templatePath");
      this.itemTemplate = itemTemplate;
      return this;
    }

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ContentType(string contentType)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(contentType, "UploadMediaItemRequestParametersBuilder.contentType is required");
      BaseValidator.CheckForTwiceSetAndThrow(this.contentType, this.GetType().Name + ".contentType");
      this.contentType = contentType;
      return this;
    }

    public IMediaResourceUploadRequest Build()
    {
      BaseValidator.CheckNullAndThrow(this.itemDataStream, this.GetType().Name + ".itemDataStream");
      BaseValidator.CheckNullAndThrow(this.itemName, this.GetType().Name + ".itemName");
      BaseValidator.CheckNullAndThrow(this.contentType, this.GetType().Name + ".contentType");
      BaseValidator.CheckNullAndThrow(this.fileName, this.GetType().Name + ".fileName");

      UploadMediaOptions createMediaParameters = new UploadMediaOptions(
        this.itemDataStream, 
        this.fileName,
        this.itemName,
        this.itemTemplate, 
        this.mediaPath,
        this.parentId,
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
    protected string parentId;
  }
}

