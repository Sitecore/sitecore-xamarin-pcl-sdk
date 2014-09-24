namespace Sitecore.MobileSDK.UserRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class RenderingHTMLRequestBuilder : IRenderingHTMLRequestParametersBuilder<IGetRenderingHtmlRequest>
  {
    public RenderingHTMLRequestBuilder(string sourceId, string renderingId)
    {
      ItemIdValidator.ValidateItemId(sourceId, this.GetType().Name + ".SourceId");
      ItemIdValidator.ValidateItemId(renderingId, this.GetType().Name + ".RenderingId");

      this.sourceId = sourceId;
      this.renderingId = renderingId;
    }

    public IRenderingHTMLRequestParametersBuilder<IGetRenderingHtmlRequest> SourceAndRenderingDatabase(string database)
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

    public IRenderingHTMLRequestParametersBuilder<IGetRenderingHtmlRequest> SourceAndRenderingLanguage(string itemLanguage)
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

    public IRenderingHTMLRequestParametersBuilder<IGetRenderingHtmlRequest> SourceVersion(int? itemVersion)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.VersionNumber, this.GetType().Name + ".Version");
      BaseValidator.AssertPositiveNumber(itemVersion, this.GetType().Name + ".Version");

      this.itemSourceAccumulator = new ItemSourcePOD(
        this.itemSourceAccumulator.Database,
        this.itemSourceAccumulator.Language,
        itemVersion);

      return this;
    }

    public IRenderingHTMLRequestParametersBuilder<IGetRenderingHtmlRequest> AddAdditionalParameterNameValue(string parameterName, string parameterValue)
    {
      //TODO: igk!!!
      return this;
    }

    public IGetRenderingHtmlRequest Build()
    {
      ReadRenderingHTMLParameters result = new ReadRenderingHTMLParameters(
        null, 
        this.itemSourceAccumulator,
        this.sourceId,
        this.renderingId
      );

      return result;
    }

    protected ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD(null, null, null);

    private string sourceId;
    private string renderingId;
  }
}

