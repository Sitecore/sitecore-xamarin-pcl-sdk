using System;

namespace Sitecore.MobileSDK.UrlBuilder.WebApi
{
    public class WebApiUrlParameters : IWebApiUrlParameters
    {
        public static WebApiUrlParameters ItemWebApiV2UrlParameters()
        {
            WebApiUrlParameters result = new WebApiUrlParameters ();
            result.DatabaseParameterName = "sc_database";
            result.LanguageParameterName = "sc_lang";
            result.VersionParameterName  = "sc_itemversion";

            result.ItemWebApiEndpoint = "/-/item/";
            result.ItemIdParameterName = "sc_itemid";

            return result;
        }

        private WebApiUrlParameters ()
        {
        }

        public string ItemIdParameterName { get; private set;}
        public string DatabaseParameterName { get; private set;}
        public string LanguageParameterName { get; private set;}
        public string VersionParameterName  { get; private set;}
        public string ItemWebApiEndpoint   { get; private set;}

    }
}

