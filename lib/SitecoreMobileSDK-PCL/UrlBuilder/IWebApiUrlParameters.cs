using System;

namespace Sitecore.MobileSDK
{
    public interface IWebApiUrlParameters
    {
        string DatabaseParameterName { get; }
        string LanguageParameterName { get; }
        string VersionParameterName { get; }
    }
}

