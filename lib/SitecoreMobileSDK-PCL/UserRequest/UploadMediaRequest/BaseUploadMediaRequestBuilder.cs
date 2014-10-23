namespace Sitecore.MobileSDK.UserRequest.UploadMediaRequest
{
  using System.IO;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.Validators;

  public class BaseUploadMediaRequestBuilder : IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest>
  {
    private ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD(null, null);

    protected Stream itemDataStream;
    protected string fileName;
    protected string itemName;
    protected string itemTemplate;
    protected string mediaPath;
    protected string contentType;
    protected string parentId;

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ItemDataStream(Stream itemDataStream)
    {
      BaseValidator.CheckNullAndThrow(itemDataStream, this.GetType().Name + ".ItemDataStream");
      BaseValidator.CheckForTwiceSetAndThrow(this.itemDataStream, this.GetType().Name + ".ItemDataStream");
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

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> FileName(string fileName)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(fileName, this.GetType().Name + ".FileName");
      BaseValidator.CheckForTwiceSetAndThrow(this.fileName, this.GetType().Name + ".FileName");
      this.fileName = fileName;
      return this;
    }

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ItemName(string itemName)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemName, this.GetType().Name + ".ItemName");
      BaseValidator.CheckForTwiceSetAndThrow(this.itemName, this.GetType().Name + ".ItemName");
      this.itemName = itemName;
      return this;
    }

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ItemTemplatePath(string templatePath)
    {
      ItemPathValidator.ValidateItemTemplate(templatePath, this.GetType().Name + ".ItemTemplatePath");
      BaseValidator.CheckForTwiceSetAndThrow(this.itemTemplate, this.GetType().Name + ".ItemTemplatePath");
      this.itemTemplate = templatePath;
      return this;
    }

    public IUploadMediaItemRequestParametersBuilder<IMediaResourceUploadRequest> ContentType(string contentType)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(contentType, this.GetType().Name + ".ContentType");
      BaseValidator.CheckForTwiceSetAndThrow(this.contentType, this.GetType().Name + ".ContentType");
      this.contentType = contentType;
      return this;
    }

    public IMediaResourceUploadRequest Build()
    {
      BaseValidator.CheckNullAndThrow(this.itemDataStream, this.GetType().Name + ".ItemDataStream");
      BaseValidator.CheckNullAndThrow(this.itemName, this.GetType().Name + ".ItemName");
      BaseValidator.CheckNullAndThrow(this.fileName, this.GetType().Name + ".FileName");

      var createMediaParameters = new UploadMediaOptions(
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
  }
}
