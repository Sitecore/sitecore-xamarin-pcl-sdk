

namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;


    public class ItemWebApiRequestBuilder
    {
        private ItemWebApiRequestBuilder()
        {
        }

        public static IGetItemRequestParametersBuilder<IReadItemsByIdRequest> ReadItemsRequestWithId(string itemId)
        {
            return new ReadItemByIdRequestBuilder(itemId);
        }

        public static IGetItemRequestParametersBuilder<IReadItemsByPathRequest> ReadItemsRequestWithPath(string itemPath)
        {
            return new ReadItemByPathRequestBuilder(itemPath);
        }

        public static IGetItemRequestParametersBuilder<IReadItemsByQueryRequest> ReadItemsRequestWithSitecoreQuery(string sitecoreQuery)
        {
            return new ReadItemByQueryRequestBuilder(sitecoreQuery);
        }
    }
}

