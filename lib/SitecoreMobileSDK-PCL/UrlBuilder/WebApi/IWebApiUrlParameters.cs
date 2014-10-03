namespace Sitecore.MobileSDK.UrlBuilder.WebApi
{
  public interface IWebApiUrlParameters
  {
    #region Item Source
    string DatabaseParameterName { get; }
    string LanguageParameterName { get; }
    string VersionParameterName { get; }
    #endregion Item Source

    #region Item Identifiers
    string SitecoreQueryParameterName { get; }
    string ItemIdParameterName { get; }
    #endregion Item Identifiers

    #region Rendering Identifiers
    string RenderingIdParameterName { get; }
    #endregion Rendering Identifiers

    #region query parameters
    string PayloadParameterName { get; }
    string TemplateParameterName { get; }
    string ScopeParameterName { get; }
    string FieldsListParameterName { get; }
    string ItemNameParameterName { get; }
    #endregion query parameters

    #region Item Web API Endpoints
    string ItemWebApiEndpoint { get; }
    string ItemWebApiActionsEndpoint { get; }
    #endregion Item Web API Endpoints

    #region Item Web API Actions
    string ItemWebApiAuthenticateAction { get; }
    string ItemWebApiGetPublicKeyAction { get; }
    string ItemWebApiGetRenderingAction { get; }

    string ItemWebApiGetHashFormediaContentAction { get; }
    string UrlForHashingParameterName { get; }
    #endregion Item Web API Actions
  }
}

