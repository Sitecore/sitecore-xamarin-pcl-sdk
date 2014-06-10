

namespace Sitecore.MobileSDK.UrlBuilder.ItemById
{
    using System;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;
    using Sitecore.MobileSDK.Utils;


    public class ItemByIdUrlBuilder : AbstractGetItemUrlBuilder
    {
        public ItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
            : base( restGrammar, webApiGrammar )
        {
        }

        public string GetUrlForRequest(IReadItemsByIdRequest request)
        {
            this.ValidateId(request.ItemId);
            string escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId);

            SessionConfigUrlBuilder sessionBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
            string result = sessionBuilder.BuildUrlString(request.SessionSettings);

            QueryParametersUrlBuilder queryParametersUrlBuilder = new QueryParametersUrlBuilder(this.restGrammar, this.webApiGrammar);
            string queryParamsUrl = queryParametersUrlBuilder.BuildUrlString(request.QueryParameters);

            ItemSourceUrlBuilder sourceBuilder = new ItemSourceUrlBuilder (this.restGrammar, this.webApiGrammar, request.ItemSource);
            string itemSourceArgs = sourceBuilder.BuildUrlQueryString ();

            result += 
                this.restGrammar.HostAndArgsSeparator + 
                itemSourceArgs +
                this.restGrammar.FieldSeparator +
                queryParamsUrl +
                this.restGrammar.FieldSeparator + 
                this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator +  escapedId;

            return result.ToLowerInvariant();
        }

        private void ValidateId(string itemId)
        {
            ItemIdValidator.ValidateItemId (itemId);
        }
    }
}
