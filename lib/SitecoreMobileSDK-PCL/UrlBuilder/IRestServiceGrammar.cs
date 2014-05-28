using System;

namespace Sitecore.MobileSDK.UrlBuilder
{
    public interface IRestServiceGrammar
    {
        string KeyValuePairSeparator { get; }
        string FieldSeparator        { get; }
        string HostAndArgsSeparator { get; }
    }
}

