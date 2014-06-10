
namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    public class ReadItemByQueryRequestBuilder : AbstractGetItemRequestBuilder<IReadItemsByQueryRequest>
    {
        public ReadItemByQueryRequestBuilder(string sitecoreQuery)
        {
            SitecoreQueryValidator.ValidateSitecoreQuery(sitecoreQuery);

            this.sitecoreQuery = sitecoreQuery;
        }

        public override IReadItemsByQueryRequest Build()
        {
            var result = new ReadItemByQueryParameters(null, this.itemSourceAccumulator, this.queryParameters, this.sitecoreQuery);
            return result;
        }

        private string sitecoreQuery;
    }
}

