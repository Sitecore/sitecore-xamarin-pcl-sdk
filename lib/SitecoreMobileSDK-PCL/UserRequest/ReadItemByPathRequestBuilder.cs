using Sitecore.MobileSDK.Items;


namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;

    public class ReadItemByPathRequestBuilder : AbstractGetItemRequestBuilder<IReadItemsByPathRequest>
    {
        public ReadItemByPathRequestBuilder (string itemPath)
        {
            ItemPathValidator.ValidateItemPath (itemPath);

            this.itemPath = itemPath;
        }

        public override IReadItemsByPathRequest Build()
        {
            var result = new ReadItemByPathParameters (null, this.itemSourceAccumulator, this.itemPath);
            return result;
        }

        private string itemPath;
    }
}

