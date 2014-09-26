﻿namespace Sitecore.MobileSDK.UserRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class ReadMediaItemRequestBuilder : IGetMediaItemRequestParametersBuilder<IMediaResourceDownloadRequest>
  {
    public ReadMediaItemRequestBuilder(string mediaPath)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(mediaPath, this.GetType().Name + ".MediaPath");

      this.mediaPath = mediaPath;
    }

    public IGetMediaItemRequestParametersBuilder<IMediaResourceDownloadRequest> Database(string database)
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

    public IGetMediaItemRequestParametersBuilder<IMediaResourceDownloadRequest> Language(string itemLanguage)
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

    public IGetMediaItemRequestParametersBuilder<IMediaResourceDownloadRequest> Version(int? itemVersion)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.VersionNumber, this.GetType().Name + ".Version");
      BaseValidator.AssertPositiveNumber(itemVersion, this.GetType().Name + ".Version");

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    public IGetMediaItemRequestParametersBuilder<IMediaResourceDownloadRequest> DownloadOptions(IDownloadMediaOptions downloadMediaOptions)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.downloadMediaOptions, this.GetType().Name + ".DownloadMediaOptions");

      BaseValidator.CheckMediaOptionsOrThrow(downloadMediaOptions, this.GetType().Name + ".DownloadMediaOptions");

      this.downloadMediaOptions = downloadMediaOptions.DeepCopyMediaDownloadOptions();

      return this;
    }

    public IMediaResourceDownloadRequest Build()
    {
      var result = new MediaResourceDownloadRequest(null, this.itemSourceAccumulator, this.downloadMediaOptions, this.mediaPath);
      return result;
    }

    protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD(null, null, null);
    protected IDownloadMediaOptions downloadMediaOptions = null;

    private string mediaPath;
  }
}

