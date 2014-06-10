

namespace Sitecore.MobileSDK.UrlBuilder.ItemById
{
    using System;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;
    using Sitecore.MobileSDK.Utils;


    public class ItemByIdUrlBuilder : AbstractGetItemUrlBuilder<IReadItemsByIdRequest>
    {
        public ItemByIdUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
            : base( restGrammar, webApiGrammar )
        {
        }

        protected override string GetSpecificPartForRequest(IReadItemsByIdRequest request)
        {
            string escapedId = UrlBuilderUtils.EscapeDataString(request.ItemId);
            string result = this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator +  escapedId;

            return result;
        }

        protected override void ValidateSpecificRequest(IReadItemsByIdRequest request)
        {
            ItemIdValidator.ValidateItemId(request.ItemId);
        }
    }
}
