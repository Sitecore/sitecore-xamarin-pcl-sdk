

namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;


    public class ItemWebApiRequestBuilder
    {
        public ItemWebApiRequestBuilder ()
        {
        }

        public IGetItemRequestParametersBuilder<IReadItemsByIdRequest> RequestWithId( string itemId )
        {
            return new ReadItemByIdRequestBuilder( itemId );
        }

        public IGetItemRequestParametersBuilder<IReadItemsByPathRequest> RequestWithPath( string itemPath )
        {
            return new ReadItemByPathRequestBuilder( itemPath );
        }

        public IGetItemRequestParametersBuilder<IReadItemsByQueryRequest> RequestWithSitecoreQuery( string sitecoreQuery )
        {
            return new ReadItemByQueryRequestBuilder( sitecoreQuery );
        }
    }
}

