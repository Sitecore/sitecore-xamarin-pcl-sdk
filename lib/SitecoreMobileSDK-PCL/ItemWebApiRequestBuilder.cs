

namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;


    public class ItemWebApiRequestBuilder
    {
        public ItemWebApiRequestBuilder ()
        {
        }

        public IGetItemRequestParametersBuilder<IReadItemsByIdRequest> RequestWithId( string itemId )
        {
            return null;
        }

        public IGetItemRequestParametersBuilder<IReadItemsByPathRequest> RequestWithPath( string itemPath )
        {
            return null;
        }
    }
}

