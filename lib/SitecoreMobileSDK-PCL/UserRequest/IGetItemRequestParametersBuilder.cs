﻿
namespace Sitecore.MobileSDK
{
    using System;
    using System.Collections.Generic;

    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    public interface IGetItemRequestParametersBuilder<T>
        where T : class
    {
        IGetItemRequestParametersBuilder<T> Database (string sitecoreDatabase);
        IGetItemRequestParametersBuilder<T> Language (string itemLanguage);
        IGetItemRequestParametersBuilder<T> Version (string itemVersion);
        IGetItemRequestParametersBuilder<T> Payload(PayloadType payload);
        IGetItemRequestParametersBuilder<T> LoadFields( ICollection<string> fields );

        T Build();
    }
}

