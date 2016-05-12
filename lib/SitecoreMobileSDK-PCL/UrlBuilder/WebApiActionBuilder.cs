namespace Sitecore.MobileSDK.UrlBuilder
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.SessionSettings;


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
     
    #region Actions
    public string GetRenderingHtmlAction(ISessionConfig sessionConfig)
    {
      return this.GetWebApiActionEndpointUrlForSession(
        this.webApiGrammar.ItemWebApiGetRenderingAction, 
        sessionConfig);
    }

    public string GetHashedMediaUrlAction(ISessionConfig sessionConfig)
    {
      return this.GetWebApiActionEndpointUrlForSession(
        this.webApiGrammar.ItemWebApiGetHashFormediaContentAction, 
        sessionConfig);
    }

    public string GetAuthenticateActionUrlForSession(ISessionConfig sessionConfig)
    {
      return this.GetWebApiActionEndpointUrlForSession(
        this.webApiGrammar.ItemWebApiLoginAction,
        sessionConfig);
    }
    #endregion Actions

    #region Utils
    private string GetWebApiEndpointUrlForSession(ISessionConfig sessionConfig)
    {
      SessionConfigUrlBuilder builder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
      return builder.BuildUrlString(sessionConfig);
    }

    private string GetWebApiActionEndpointUrlForSession(string actionName, ISessionConfig sessionConfig)
    {
      string hostWithSite = this.GetWebApiEndpointUrlForSession(sessionConfig);

      string result = hostWithSite +
                      this.restGrammar.PathComponentSeparator +
                      this.webApiGrammar.ItemWebApiAuthEndpoint +
                      actionName;

      return result;
    }
    #endregion Utils
  }
}

