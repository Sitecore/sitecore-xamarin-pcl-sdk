using System;

namespace Sitecore.MobileSDK
{
    public interface IGetItemRequestParametersBuilder<T>
        where T : class
    {
        IGetItemRequestParametersBuilder<T> Database (string sitecoreDatabase);
        IGetItemRequestParametersBuilder<T> Language (string itemLanguage);
        IGetItemRequestParametersBuilder<T> Version (string itemVersion);

        T Build();
    }
}

