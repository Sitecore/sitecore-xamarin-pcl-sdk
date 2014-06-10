using System;

namespace Sitecore.MobileSDK.UrlBuilder.WebApi
{
    public interface IWebApiUrlParameters
    {
        string DatabaseParameterName { get; }
        string LanguageParameterName { get; }
        string VersionParameterName { get; }
        string PayloadParameterName { get; }

        string ItemIdParameterName { get; }
        string SitecoreQueryParameterName { get; }

        string ItemWebApiEndpoint { get; }
    }
}

