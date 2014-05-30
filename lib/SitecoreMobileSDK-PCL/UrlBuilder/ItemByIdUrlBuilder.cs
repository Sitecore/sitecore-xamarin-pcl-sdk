namespace Sitecore.MobileSDK.UrlBuilder
{
    using System;
    using Sitecore.MobileSDK.SessionSettings;

    public class ItemByIdUrlBuilder : WebApiUrlBuilder
    {
        public ItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
            : base(restGrammar, webApiGrammar)
        {
        }

        public string GetUrlForRequest(IGetItemByIdRequest request)
        {
            SessionConfigUrlBuilder sessionBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
            string result = sessionBuilder.BuildUrlString(request.SessionSettings);

            this.ValidateId(request.ItemId);

            string escapedId = Uri.EscapeDataString(request.ItemId);
            
            result += this.restGrammar.HostAndArgsSeparator + this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator + escapedId;

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
    }
}
