using System;

namespace Sitecore.MobileSDK.UrlBuilder.WebApi
{
    public interface IWebApiUrlParameters
    {
        string DatabaseParameterName { get; }
        string LanguageParameterName { get; }
        string VersionParameterName { get; }

        string ItemIdParameterName { get; }

        string ItemWebApiEndpoint { get; }
    }
}

