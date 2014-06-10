namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;

    public class QueryParametersUrlBuilder
    {
        public QueryParametersUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
        {
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;
        }

        public string BuildUrlString(IQueryParameters queryParameters)
        {
            this.Validate(queryParameters);

            string result = string.Empty;

            if (queryParameters.Payload != PayloadType.None)
            {
                string payload = string.Empty;
                switch (queryParameters.Payload)
                {
                    case PayloadType.Content:
                        payload = "content";
                        break;
                    case PayloadType.Full:
                        payload = "full";
                        break;
                    case PayloadType.Min:
                        payload = "min";
                        break;
                }

                result += this.webApiGrammar.PayloadParameterName + this.restGrammar.KeyValuePairSeparator + payload;
            }

            return result;
        }

        private void Validate(IQueryParameters queryParameters)
        {
            if (null == queryParameters)
            {
                throw new ArgumentNullException("QueryParameters cannot be null");
            }
        }

        private IRestServiceGrammar restGrammar;
        private IWebApiUrlParameters webApiGrammar;
    }
}
