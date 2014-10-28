using Sitecore.MobileSDK.API.Request.Parameters;
using Sitecore.MobileSDK.API;

namespace Sitecore.MobileSDK.UserRequest.ReadRequest
{
  using Sitecore.MobileSDK.API.Request;
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
      IPagingParameters pagingSettings = this.AccumulatedPagingParameters;
      ISessionConfig sessionSettings = null;

      var result = new ReadItemByQueryParameters(
        sessionSettings, 
        this.itemSourceAccumulator, 
        this.queryParameters, 
        pagingSettings,
        this.sitecoreQuery);


      return result;
    }

    private string sitecoreQuery;
  }
}

