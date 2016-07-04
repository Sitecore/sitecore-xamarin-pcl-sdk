namespace Sitecore.MobileSDK.UrlBuilder.SSC
{
  public interface ISSCUrlParameters
  {
    #region Authentication
    string ItemSSCAuthEndpoint { get; }
    string ItemSSCItemsEndpoint { get; }
    string ItemSSCLoginAction  { get; }
    string ItemSSCLogoutAction { get; }
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
    string ItemSSCChildrenAction { get; }
    #endregion Item Identifiers

    #region Search
    string RunStoredSearchAction { get; }
    string RunStoredQueryAction { get; }
    string ItemSearchAction { get; }
    string SitecoreSearchParameterName { get; }
    string SortingParameterName { get; }
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
    string ItemSSCEndpoint { get; }
    string ItemSSCActionsEndpoint { get; }
    #endregion Item Web API Endpoints

    #region Item Web API Actions
    string ItemSSCGetPublicKeyAction { get; }
    string ItemSSCGetRenderingAction { get; }

    string ItemSSCGetHashFormediaContentAction { get; }
    string UrlForHashingParameterName { get; }
    #endregion Item Web API Actions

    #region Paging
    string PageNumberParameterName { get; }
    string ItemsPerPageParameterName { get; }
    #endregion Paging

    string IncludeStandardTemplateFieldsParameterName { get; }
  }
}

