


namespace Sitecore.MobileSDK.UrlBuilder.ItemByQuery
{
    using System;

    using Sitecore.MobileSDK.Utils;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;


    public class ItemByQueryUrlBuilder
    {
        public ItemByQueryUrlBuilder (IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
        {
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;

            this.Validate();
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
            else if (null == request.SitecoreQuery)
            {
                throw new ArgumentNullException ("ItemByPathUrlBuilder.GetUrlForRequest() : request.SitecoreQuery cannot be null");
            }
        }
            
        private void Validate()
        {
            if (null == this.restGrammar)
            {
                throw new ArgumentNullException ("[SessionConfigUrlBuilder] restGrammar cannot be null");
            }
            else if (null == this.webApiGrammar)
            {
                throw new ArgumentNullException ("[SessionConfigUrlBuilder] webApiGrammar cannot be null");
            }
        }


        private IRestServiceGrammar restGrammar;
        private IWebApiUrlParameters webApiGrammar;
    }
}

