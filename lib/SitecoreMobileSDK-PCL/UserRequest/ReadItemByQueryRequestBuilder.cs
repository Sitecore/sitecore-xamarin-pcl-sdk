
namespace Sitecore.MobileSDK.UserRequest
{
  using System;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.Validators;

  public class ReadItemByQueryRequestBuilder : AbstractBaseRequestBuilder<IReadItemsByQueryRequest>
  {
    public ReadItemByQueryRequestBuilder(string sitecoreQuery)
    {
      SitecoreQueryValidator.ValidateSitecoreQuery(sitecoreQuery);

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

