


namespace Sitecore.MobileSDK.UrlBuilder.ItemByQuery
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;

    public class ItemByQueryUrlBuilder
    {
        public ItemByQueryUrlBuilder (IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
        {
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;

            this.Validate();
        }

        public string GetUrlForRequest(IGetItemByQueryRequest request)
        {
            this.ValidateRequest ();

            throw new Exception ("Not implemented");
        }

        private void ValidateRequest(IGetItemByQueryRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("ItemByPathUrlBuilder.GetUrlForRequest() : request cannot be null");
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

