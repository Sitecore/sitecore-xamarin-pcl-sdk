
namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;

    public class ReadItemByQueryRequestBuilder : AbstractGetItemRequestBuilder<IReadItemsByQueryRequest>
    {
        public ReadItemByQueryRequestBuilder (string sitecoreQuery)
        {
            this.sitecoreQuery = sitecoreQuery;
        }

        public override IReadItemsByQueryRequest Build()
        {
            var result = new ReadItemByQueryParameters (null, this.itemSourceAccumulator, this.sitecoreQuery);
            return result;
        }

        private string sitecoreQuery;
    }
}

