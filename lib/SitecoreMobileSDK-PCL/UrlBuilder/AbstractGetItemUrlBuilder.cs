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

        #region Entry Point
        public virtual string GetUrlForRequest(TRequest request)
        {
            this.ValidateRequest( request );

            string baseUrl = this.GetCommonPartForRequest( request );
            string specificParameters = this.GetSpecificPartForRequest( request );

            string result = baseUrl + this.restGrammar.FieldSeparator + specificParameters;
            return result.ToLowerInvariant();
        }

        protected virtual void ValidateRequest(TRequest request)
        {
            this.ValidateCommonRequest( request );
            this.ValidateSpecificRequest( request );
        }
        #endregion Entry Point

        #region Common Logic
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

        private void ValidateCommonRequest(TRequest request)
        {
            if ( null == request )
            {
                throw new ArgumentNullException( "AbstractGetItemUrlBuilder.GetUrlForRequest() : request cannot be null" );
            }
            else if ( null == request.ItemSource )
            {
                throw new ArgumentNullException( "AbstractGetItemUrlBuilder.GetUrlForRequest() : request.ItemSource cannot be null" );
            }
            else if ( null == request.SessionSettings )
            {
                throw new ArgumentNullException( "AbstractGetItemUrlBuilder.GetUrlForRequest() : request.SessionSettings cannot be null" );
            }
            else if ( null == request.QueryParameters )
            {
                throw new ArgumentNullException( "AbstractGetItemUrlBuilder.QueryParameters() : request.SessionSettings cannot be null" );
            }
        }

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
        #endregion Common Logic

        #region Abstract Methods
        protected abstract string GetSpecificPartForRequest(TRequest request);

        protected abstract void ValidateSpecificRequest(TRequest request);
        #endregion Abstract Methods

        #region instance variables
        protected IRestServiceGrammar restGrammar;
        protected IWebApiUrlParameters webApiGrammar;
        #endregion instance variables
    }
}

