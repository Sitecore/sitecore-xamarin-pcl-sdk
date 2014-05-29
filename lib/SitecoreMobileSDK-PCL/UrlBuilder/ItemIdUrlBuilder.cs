namespace Sitecore.MobileSDK.UrlBuilder
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
            if (null == itemId)
            {
                throw new ArgumentNullException("WebApiUrlBuilder.GetUrlForRequest() : item id cannot be null");
            }

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
            
            ReadItemByIdParameters config = (ReadItemByIdParameters) request;

            this.ValidateId(config.ItemId);
            string escapedId = Uri.EscapeDataString(config.ItemId);

            result += this.restGrammar.HostAndArgsSeparator + this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator + escapedId;

            return result.ToLower();
        }
    }
}
