﻿namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;
    using Sitecore.MobileSDK.SessionSettings;

    public class ItemByIdUrlBuilder 
    {
        public ItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
        {
            this.restGrammar = restGrammar;
            this.webApiGrammar = webApiGrammar;
        }


        public string GetUrlForRequest(IGetItemByIdRequest request)
        {
            this.ValidateId(request.ItemId);
            string escapedId = Uri.EscapeDataString(request.ItemId);

            SessionConfigUrlBuilder sessionBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
            string result = sessionBuilder.BuildUrlString(request.SessionSettings);

            ItemSourceUrlBuilder sourceBuilder = new ItemSourceUrlBuilder (this.restGrammar, this.webApiGrammar, request.ItemSource);
            string itemSourceArgs = sourceBuilder.BuildUrlQueryString ();

            result += 
                this.restGrammar.HostAndArgsSeparator + 
                itemSourceArgs + 
                this.restGrammar.FieldSeparator + 
                this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator +  escapedId;

            return result.ToLowerInvariant();
        }

        private void ValidateId(string itemId)
        {
            if (null == itemId)
            {
                throw new ArgumentNullException("ItemByIdUrlBuilder.GetUrlForRequest() : item id cannot be null");
            }

            bool hasOpeningBrace = itemId.StartsWith("{");
            bool hasClosingBrace = itemId.EndsWith("}");
            bool isValidId = hasOpeningBrace && hasClosingBrace;
            if (!isValidId)
            {
                throw new ArgumentException("ItemByIdUrlBuilder.GetUrlForRequest() : item id must have curly braces '{}'");
            }
        }

        private IRestServiceGrammar restGrammar;
        private IWebApiUrlParameters webApiGrammar;
    }
}
