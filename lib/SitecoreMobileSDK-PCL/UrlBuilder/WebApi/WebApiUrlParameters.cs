namespace Sitecore.MobileSDK.UrlBuilder.WebApi
{
  public class WebApiUrlParameters : IWebApiUrlParameters
  {
    public static WebApiUrlParameters ItemWebApiV2UrlParameters()
    {
      WebApiUrlParameters result = new WebApiUrlParameters();
      result.DatabaseParameterName = "database";
      result.LanguageParameterName = "language";
      result.VersionParameterName = "version";

      result.FieldsListParameterName = "fields";
      result.TemplateParameterName = "template";
      result.ItemNameParameterName = "name";

      result.RenderingIdParameterName = "renderingId";

      result.ItemIdParameterName = "id";
      result.ItemPathParameterName = "path";

      result.SitecoreQueryParameterName = "query";

      result.ItemWebApiEndpoint = "/sitecore/api/ssc";

      result.ItemWebApiActionsEndpoint = "-/actions";

      result.ItemWebApiItemsEndpoint = "/item";
      result.ItemWebApiAuthEndpoint = "/auth";
      result.ItemWebApiLoginAction = "/login";
      result.ItemWebApiLogoutAction = "/logout";
      result.ItemWebApiChildrenAction = "/children";

      result.ItemWebApiGetRenderingAction = "/getrenderinghtml";

      result.PageNumberParameterName = "page";
      result.ItemsPerPageParameterName = "pageSize";

      result.ItemWebApiGetHashFormediaContentAction = "/getsignedmediaurl";
      result.UrlForHashingParameterName = "url";

      result.RunStoredSearchParameterName = "/query";

      return result;
    }

    private WebApiUrlParameters()
    {
    }

    public string ItemWebApiAuthEndpoint { get; private set; }
    public string ItemWebApiLoginAction  { get; private set; }
    public string ItemWebApiLogoutAction { get; private set; }

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

    public string ItemWebApiEndpoint { get; private set; }
    public string ItemWebApiItemsEndpoint { get; private set; }
    public string ItemWebApiActionsEndpoint { get; private set; }

    public string ItemWebApiAuthenticateAction { get; private set; }
    public string ItemWebApiGetPublicKeyAction { get; private set; }
    public string ItemWebApiGetRenderingAction { get; private set; }
    public string ItemWebApiGetHashFormediaContentAction { get; private set; }
    public string UrlForHashingParameterName { get; private set; }

    public string ItemWebApiChildrenAction { get; private set; }

    public string PageNumberParameterName { get; private set; }
    public string ItemsPerPageParameterName { get; private set; }

    public string RunStoredSearchParameterName { get; private set; }
  }
}

