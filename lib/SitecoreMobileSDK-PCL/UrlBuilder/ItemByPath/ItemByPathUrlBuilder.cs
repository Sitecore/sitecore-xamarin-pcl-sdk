

namespace Sitecore.MobileSDK.UrlBuilder.ItemByPath
{
  using System;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.SessionSettings;


  public class ItemByPathUrlBuilder : AbstractGetItemUrlBuilder<IReadItemsByPathRequest>
  {
    public ItemByPathUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
      : base( restGrammar, webApiGrammar )
    {
    }

    protected override string GetSpecificPartForRequest(IReadItemsByPathRequest request)
    {
      throw new InvalidOperationException("ItemByPathUrlBuilder.GetSpecificPartForRequest() - Unexpected instruction");
    }

    public override string GetUrlForRequest(IReadItemsByPathRequest request)
    {
      this.ValidateRequest (request);
      string escapedPath = UrlBuilderUtils.EscapeDataString(request.ItemPath);

      var sessionBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
      string result = sessionBuilder.BuildUrlString(request.SessionSettings);

      var sourceBuilder = new ItemSourceUrlBuilder (this.restGrammar, this.webApiGrammar, request.ItemSource);
      string itemSourceArgs = sourceBuilder.BuildUrlQueryString ();

      var itemLoadParamsBuilder = new QueryParametersUrlBuilder(this.restGrammar, this.webApiGrammar);
      string itemLoadParams = itemLoadParamsBuilder.BuildUrlString(request.QueryParameters);

      result = 
        result +
        escapedPath +
        this.restGrammar.HostAndArgsSeparator +
        itemSourceArgs;

      if (!string.IsNullOrEmpty(itemLoadParams))
      {
        result = 
          result +
          this.restGrammar.FieldSeparator +
          itemLoadParams;
      }

      return result.ToLowerInvariant();
    }

    protected override void ValidateSpecificRequest(IReadItemsByPathRequest request)
    {
      ItemPathValidator.ValidateItemPath (request.ItemPath);
    }
  }
}
