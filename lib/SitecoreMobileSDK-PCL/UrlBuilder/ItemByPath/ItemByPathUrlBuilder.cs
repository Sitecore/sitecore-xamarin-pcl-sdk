

namespace Sitecore.MobileSDK.UrlBuilder.ItemByPath
{
    using System;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;
    using Sitecore.MobileSDK.Utils;
    using Sitecore.MobileSDK.SessionSettings;


    public class ItemByPathUrlBuilder : AbstractGetItemUrlBuilder<IReadItemsByPathRequest>
    {
        public ItemByPathUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
            : base( restGrammar, webApiGrammar )
        {
        }

        protected override string GetSpecificPartForRequest(IReadItemsByPathRequest request)
        {
            throw new InvalidOperationException("ItemByPathUrlBuilder.GetSpecificPartForRequest() - Unexpected instruction");
        }

        public override string GetUrlForRequest(IReadItemsByPathRequest request)
        {
            this.ValidateRequest (request);
            string escapedPath = UrlBuilderUtils.EscapeDataString(request.ItemPath);

            SessionConfigUrlBuilder sessionBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
            string result = sessionBuilder.BuildUrlString(request.SessionSettings);

            ItemSourceUrlBuilder sourceBuilder = new ItemSourceUrlBuilder (this.restGrammar, this.webApiGrammar, request.ItemSource);
            string itemSourceArgs = sourceBuilder.BuildUrlQueryString ();

            result += 
                escapedPath + 
                this.restGrammar.HostAndArgsSeparator + 
                itemSourceArgs;

            return result.ToLowerInvariant();
        }

        private void ValidateRequest(IReadItemsByPathRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("ItemByPathUrlBuilder.GetUrlForRequest() : request cannot be null");
            }

            this.ValidatePath (request.ItemPath);
        }

        private void ValidatePath(string itemPath)
        {
            ItemPathValidator.ValidateItemPath (itemPath);
        }            
    }
}
