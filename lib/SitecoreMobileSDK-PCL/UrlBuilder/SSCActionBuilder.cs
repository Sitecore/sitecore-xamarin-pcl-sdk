namespace Sitecore.MobileSDK.UrlBuilder
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.SessionSettings;


  internal class SSCActionBuilder
  {
    private IRestServiceGrammar restGrammar;
    private ISSCUrlParameters sscGrammar;

    public SSCActionBuilder(
      IRestServiceGrammar restGrammar,
      ISSCUrlParameters sscGrammar)
    {
      this.restGrammar = restGrammar;
      this.sscGrammar = sscGrammar;
    }
     
    #region Actions
    public string GetRenderingHtmlAction(ISessionConfig sessionConfig)
    {
      return this.GetSSCActionEndpointUrlForSession(
        this.sscGrammar.ItemSSCGetRenderingAction, 
        sessionConfig);
    }

    public string GetHashedMediaUrlAction(ISessionConfig sessionConfig)
    {
      return this.GetSSCActionEndpointUrlForSession(
        this.sscGrammar.ItemSSCGetHashFormediaContentAction, 
        sessionConfig);
    }

    public string GetAuthenticateActionUrlForSession(ISessionConfig sessionConfig)
    {
      return this.GetSSCActionEndpointUrlForSession(
        this.sscGrammar.ItemSSCLoginAction,
        sessionConfig);
    }
    #endregion Actions

    #region Utils
    private string GetSSCEndpointUrlForSession(ISessionConfig sessionConfig)
    {
      SessionConfigUrlBuilder builder = new SessionConfigUrlBuilder(this.restGrammar, this.sscGrammar);
      return builder.BuildUrlString(sessionConfig);
    }

    private string GetSSCActionEndpointUrlForSession(string actionName, ISessionConfig sessionConfig)
    {
      string hostWithSite = this.GetSSCEndpointUrlForSession(sessionConfig);

      string result = hostWithSite +
                      this.restGrammar.PathComponentSeparator +
                      this.sscGrammar.ItemSSCAuthEndpoint +
                      actionName;

      return result;
    }
    #endregion Utils
  }
}

