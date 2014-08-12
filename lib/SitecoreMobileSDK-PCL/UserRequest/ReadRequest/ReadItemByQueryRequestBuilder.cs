namespace Sitecore.MobileSDK.UserRequest.ReadRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class ReadItemByQueryRequestBuilder : AbstractScopedRequestParametersBuilder<IReadItemsByQueryRequest>
  {
    public ReadItemByQueryRequestBuilder(string sitecoreQuery)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(sitecoreQuery, this.GetType().Name + ".SitecoreQuery");

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

