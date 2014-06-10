namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;
    using System.Collections.Generic;
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

            string result = this.PayloadTypeToRestArgumentStatement( queryParameters.Payload );
            return result;
        }


        private string PayloadTypeToRestArgumentStatement(PayloadType payload)
        {
            string strPayload = this.PayloadTypeToString( payload );
            string result = string.Empty;

            if ( null != strPayload )
            {
                result = this.webApiGrammar.PayloadParameterName + this.restGrammar.KeyValuePairSeparator + strPayload;
            }

            return result;
        }

        private string PayloadTypeToString(PayloadType payload)
        {
            var enumNamesMap = new Dictionary<PayloadType, string>();
            enumNamesMap.Add( PayloadType.Content, "content" );
            enumNamesMap.Add( PayloadType.Full   , "full"    );
            enumNamesMap.Add( PayloadType.Min    , "min"     );

            string result = enumNamesMap[payload];
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
