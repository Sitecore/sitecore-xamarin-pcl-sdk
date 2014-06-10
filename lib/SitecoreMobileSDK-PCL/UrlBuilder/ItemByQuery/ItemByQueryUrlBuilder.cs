


namespace Sitecore.MobileSDK.UrlBuilder.ItemByQuery
{
    using System;

    using Sitecore.MobileSDK.Utils;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;


    public class ItemByQueryUrlBuilder : AbstractGetItemUrlBuilder
    {
        public ItemByQueryUrlBuilder (IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
            : base( restGrammar, webApiGrammar )
        {
        }

        public string GetUrlForRequest(IReadItemsByQueryRequest request)
        {
            this.ValidateRequest (request);

            SessionConfigUrlBuilder sessionBuilder = new SessionConfigUrlBuilder (this.restGrammar, this.webApiGrammar);
            string urlBase = sessionBuilder.BuildUrlString (request.SessionSettings);

            ItemSourceUrlBuilder sourceBuilder = new ItemSourceUrlBuilder (this.restGrammar, this.webApiGrammar, request.ItemSource);
            string source = sourceBuilder.BuildUrlQueryString ();


            string escapedQuery = UrlBuilderUtils.EscapeDataString (request.SitecoreQuery);

            string result = 
                urlBase +
                this.restGrammar.HostAndArgsSeparator +
                source +
                this.restGrammar.FieldSeparator +
                this.webApiGrammar.SitecoreQueryParameterName + this.restGrammar.KeyValuePairSeparator + escapedQuery;

            return result.ToLowerInvariant ();
        }

        private void ValidateRequest(IReadItemsByQueryRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException ("ItemByPathUrlBuilder.GetUrlForRequest() : request cannot be null");
            }

            SitecoreQueryValidator.ValidateSitecoreQuery (request.SitecoreQuery);
        }
    }
}

