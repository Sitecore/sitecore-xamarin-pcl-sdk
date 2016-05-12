namespace Sitecore.MobileSDK.UrlBuilder.WebApi
{
  public interface IWebApiUrlParameters
  {
    #region Authentication
    string ItemWebApiAuthEndpoint { get; }
    string ItemWebApiItemsEndpoint { get; }
    string ItemWebApiLoginAction  { get; }
    string ItemWebApiLogoutAction { get; }
    #endregion Authentication

    #region Item Source
    string DatabaseParameterName { get; }
    string LanguageParameterName { get; }
    string VersionParameterName { get; }
    #endregion Item Source

    #region Item Identifiers
    string SitecoreQueryParameterName { get; }
    string ItemIdParameterName { get; }
    string ItemPathParameterName { get; }
    string ItemWebApiChildrenAction { get; }
    #endregion Item Identifiers

    #region Search
    string RunStoredSearchParameterName { get; }
    #endregion Search

    #region Rendering Identifiers
    string RenderingIdParameterName { get; }
    #endregion Rendering Identifiers

    #region query parameters
    string TemplateParameterName { get; }
    string FieldsListParameterName { get; }
    string ItemNameParameterName { get; }
    #endregion query parameters

    #region Item Web API Endpoints
    string ItemWebApiEndpoint { get; }
    string ItemWebApiActionsEndpoint { get; }
    #endregion Item Web API Endpoints

    #region Item Web API Actions
    string ItemWebApiGetPublicKeyAction { get; }
    string ItemWebApiGetRenderingAction { get; }

    string ItemWebApiGetHashFormediaContentAction { get; }
    string UrlForHashingParameterName { get; }
    #endregion Item Web API Actions

    #region Paging
    string PageNumberParameterName { get; }
    string ItemsPerPageParameterName { get; }
    #endregion Paging
  }
}

