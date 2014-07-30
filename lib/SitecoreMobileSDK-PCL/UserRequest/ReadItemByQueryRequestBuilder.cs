namespace Sitecore.MobileSDK.UserRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Validators;

  public class ReadItemByQueryRequestBuilder : AbstractBaseRequestBuilder<IReadItemsByQueryRequest>
  {
    public ReadItemByQueryRequestBuilder(string sitecoreQuery)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(sitecoreQuery, this.GetType().Name + ".sitecoreQuery");

      this.sitecoreQuery = sitecoreQuery;
    }

    public override IReadItemsByQueryRequest Build()
    {
      var result = new ReadItemByQueryParameters(null, this.itemSourceAccumulator, this.queryParameters, this.sitecoreQuery);
      return result;
    }

    private string sitecoreQuery;
  }
}

