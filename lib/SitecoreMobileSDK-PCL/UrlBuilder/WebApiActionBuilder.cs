using Sitecore.MobileSDK.Validators;

namespace Sitecore.MobileSDK.UrlBuilder
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.API;


  public class WebApiActionBuilder
  {
    private IRestServiceGrammar restGrammar;
    private IWebApiUrlParameters webApiGrammar;

    public WebApiActionBuilder(
      IRestServiceGrammar restGrammar,
      IWebApiUrlParameters webApiGrammar)
    {
      this.restGrammar = restGrammar;
      this.webApiGrammar = webApiGrammar;
    }

    public string GetWebApiEndpointUrlForSession(ISessionConfig sessionConfig)
    {
      string host = SessionConfigValidator.AutocompleteInstanceUrl(sessionConfig.InstanceUrl);
      string result = host +
        this.webApiGrammar.ItemWebApiEndpoint +
        sessionConfig.ItemWebApiVersion;

      return result.ToLowerInvariant();
    }


  }
}

