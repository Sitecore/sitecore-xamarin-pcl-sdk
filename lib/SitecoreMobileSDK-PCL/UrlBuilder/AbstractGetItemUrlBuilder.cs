using Sitecore.MobileSDK.UrlBuilder;
using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;


namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;


    public abstract class AbstractGetItemUrlBuilder<TRequest>
        where TRequest : IBaseGetItemRequest
    {
        public AbstractGetItemUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
        {
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;

            this.Validate();
        }

        public virtual string GetUrlForRequest(TRequest request)
        {
            string baseUrl = this.GetCommonPartForRequest( request );
            string specificParameters = this.GetSpecificPartForRequest( request );

            string result = baseUrl + this.restGrammar.FieldSeparator + specificParameters;
            return result.ToLowerInvariant();
        }

        private string GetCommonPartForRequest(TRequest request)
        {
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
                queryParamsUrl;

            return result;
        }

        protected abstract string GetSpecificPartForRequest(TRequest request);


        private void Validate()
        {
            if (null == this.restGrammar)
            {
                throw new ArgumentNullException ("[GetItemUrlBuilder] restGrammar cannot be null"); 
            }
            else if (null == this.webApiGrammar)
            {
                throw new ArgumentNullException ("[GetItemUrlBuilder] webApiGrammar cannot be null");
            }
        }


        protected IRestServiceGrammar restGrammar;
        protected IWebApiUrlParameters webApiGrammar;
    }
}

