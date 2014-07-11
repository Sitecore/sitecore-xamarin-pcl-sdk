using System;

namespace Sitecore.MobileSDK.UrlBuilder.WebApi
{
  public class WebApiUrlParameters : IWebApiUrlParameters
  {
    public static WebApiUrlParameters ItemWebApiV2UrlParameters()
    {
      WebApiUrlParameters result = new WebApiUrlParameters();
      result.DatabaseParameterName = "sc_database";
      result.LanguageParameterName = "language";
      result.VersionParameterName = "sc_itemversion";

      result.PayloadParameterName = "payload";
      result.ScopeParameterName = "scope";
      result.FieldsListParameterName = "fields";
      result.TemplateParameterName = "template";
      result.ItemNameParameterName = "name";

      result.ItemIdParameterName = "sc_itemid";
      result.SitecoreQueryParameterName = "query";

      result.ItemWebApiEndpoint = "/-/item/";

      result.ItemWebApiActionsEndpoint = "-/actions";
      result.ItemWebApiAuthenticateAction = "/authenticate";
      result.ItemWebApiGetPublicKeyAction = "/getpublickey";

      return result;
    }

    private WebApiUrlParameters()
    {
    }

    public string DatabaseParameterName { get; private set; }
    public string LanguageParameterName { get; private set; }
    public string VersionParameterName { get; private set; }

    public string PayloadParameterName { get; private set; }
    public string TemplateParameterName { get; private set; }
    public string ScopeParameterName { get; private set; }
    public string FieldsListParameterName { get;  private set; }
    public string ItemNameParameterName { get;  private set; }

    public string ItemIdParameterName { get; private set; }
    public string SitecoreQueryParameterName { get; private set; }

    public string ItemWebApiEndpoint { get; private set; }
    public string ItemWebApiActionsEndpoint { get; private set; }

    public string ItemWebApiAuthenticateAction { get; private set; }
    public string ItemWebApiGetPublicKeyAction { get; private set; }
  }
}

