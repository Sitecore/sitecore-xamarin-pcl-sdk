using System;

namespace Sitecore.MobileSDK.UrlBuilder
{
    public interface IWebApiUrlParameters
    {
        string DatabaseParameterName { get; }
        string LanguageParameterName { get; }
        string VersionParameterName { get; }
    }
}

