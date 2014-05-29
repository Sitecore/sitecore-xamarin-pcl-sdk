﻿namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;

    public class ItemIdUrlBuilder : WebApiUrlBuilder
    {
        public ItemIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
            : base(restGrammar, webApiGrammar)
        {
        }

        public void ValidateId(string itemId)
        {
            bool hasOpeningBrace = itemId.StartsWith("{");
            bool hasClosingBrace = itemId.EndsWith("}");
            bool isValidId = hasOpeningBrace && hasClosingBrace;
            if (!isValidId)
            {
                throw new ArgumentException("WebApiUrlBuilder.GetUrlForRequest() : item id must have curly braces '{}'");
            }
        }

        public override string GetUrlForRequest(IRequestConfig request)
        {
            string result = base.GetUrlForRequest(request);
            
            this.ValidateId(request.ItemId);
            string escapedId = Uri.EscapeDataString(request.ItemId);

            result += this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator + escapedId;

            return result.ToLower();
        }
    }
}
