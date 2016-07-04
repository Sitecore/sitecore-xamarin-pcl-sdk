namespace Sitecore.MobileSDK.UrlBuilder.SSC
{
  public class SSCUrlParameters : ISSCUrlParameters
  {
    public static SSCUrlParameters ItemSSCV2UrlParameters()
    {
      SSCUrlParameters result = new SSCUrlParameters();
      result.DatabaseParameterName = "database";
      result.LanguageParameterName = "language";
      result.VersionParameterName = "version";

      result.FieldsListParameterName = "fields";
      result.TemplateParameterName = "template";
      result.ItemNameParameterName = "name";
      result.SortingParameterName = "sorting";

      result.RenderingIdParameterName = "renderingId";

      result.ItemIdParameterName = "id";
      result.ItemPathParameterName = "path";

      result.SitecoreSearchParameterName = "term";

      result.SitecoreQueryParameterName = "query";

      result.ItemSSCEndpoint = "/sitecore/api/ssc";

      result.ItemSSCActionsEndpoint = "-/actions";

      result.ItemSSCItemsEndpoint = "/item";
      result.ItemSSCAuthEndpoint = "/auth";
      result.ItemSSCLoginAction = "/login";
      result.ItemSSCLogoutAction = "/logout";
      result.ItemSSCChildrenAction = "/children";
      result.ItemSearchAction = "/search";


      result.ItemSSCGetRenderingAction = "/getrenderinghtml";

      result.PageNumberParameterName = "page";
      result.ItemsPerPageParameterName = "pageSize";
      result.IncludeStandardTemplateFieldsParameterName = "includeStandardTemplateFields";

      result.ItemSSCGetHashFormediaContentAction = "/getsignedmediaurl";
      result.UrlForHashingParameterName = "url";

      result.RunStoredSearchAction = "/search";
      result.RunStoredQueryAction = "/query";

      return result;
    }

    private SSCUrlParameters()
    {
    }

    public string ItemSSCAuthEndpoint { get; private set; }
    public string ItemSSCLoginAction  { get; private set; }
    public string ItemSSCLogoutAction { get; private set; }

    public string DatabaseParameterName { get; private set; }
    public string LanguageParameterName { get; private set; }
    public string VersionParameterName  { get; private set; }

    public string TemplateParameterName { get; private set; }
    public string FieldsListParameterName { get; private set; }
    public string ItemNameParameterName { get; private set; }

    public string RenderingIdParameterName { get; private set; }

    public string ItemIdParameterName { get; private set; }
    public string ItemPathParameterName { get; private set; }
    public string SitecoreQueryParameterName { get; private set; }

    public string ItemSSCEndpoint { get; private set; }
    public string ItemSSCItemsEndpoint { get; private set; }
    public string ItemSSCActionsEndpoint { get; private set; }

    public string ItemSSCAuthenticateAction { get; private set; }
    public string ItemSSCGetPublicKeyAction { get; private set; }
    public string ItemSSCGetRenderingAction { get; private set; }
    public string ItemSSCGetHashFormediaContentAction { get; private set; }
    public string UrlForHashingParameterName { get; private set; }

    public string ItemSSCChildrenAction { get; private set; }

    public string PageNumberParameterName { get; private set; }
    public string ItemsPerPageParameterName { get; private set; }

    public string RunStoredSearchAction { get; private set; }
    public string RunStoredQueryAction { get; private set; }
    public string SitecoreSearchParameterName { get; private set; }
    public string ItemSearchAction { get; private set; }

    public string IncludeStandardTemplateFieldsParameterName { get; private set; }

    public string SortingParameterName { get; private set; }
  }
}

