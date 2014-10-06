using Sitecore.MobileSDK.SessionSettings;

namespace Sitecore.MobileSDK.UrlBuilder
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.API;


  internal class WebApiActionBuilder
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
      SessionConfigUrlBuilder builder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
      return builder.BuildUrlString(sessionConfig);
    }

    public string GetWebApiActionEndpointUrlForSession(string actionName, ISessionConfig sessionConfig)
    {
      string hostWithSite = this.GetWebApiEndpointUrlForSession(sessionConfig);

      string result = hostWithSite +
                      this.restGrammar.PathComponentSeparator +
                      this.webApiGrammar.ItemWebApiActionsEndpoint +
                      actionName;

      return result;
    }
  }
}

