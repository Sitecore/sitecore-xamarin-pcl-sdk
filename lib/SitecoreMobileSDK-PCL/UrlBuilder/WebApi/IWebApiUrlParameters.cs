﻿using System;

namespace Sitecore.MobileSDK.UrlBuilder.WebApi
{
    public interface IWebApiUrlParameters
    {
        #region Item Source
        string DatabaseParameterName { get; }
        string LanguageParameterName { get; }
        string VersionParameterName { get; }
        #endregion Item Source


        #region Item Identifiers
        string SitecoreQueryParameterName { get; }
        string ItemIdParameterName { get; }
        #endregion Item Identifiers

        #region query parameters
        string PayloadParameterName { get; }
        string FieldsListParameterName { get; }
        #endregion query parameters

        #region Item Web API Actions
        string ItemWebApiEndpoint { get; }
        #endregion Item Web API Actions
    }
}

