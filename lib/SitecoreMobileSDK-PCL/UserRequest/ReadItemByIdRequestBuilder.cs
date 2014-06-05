
namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;

    public class ReadItemByIdRequestBuilder : AbstractGetItemRequestBuilder<IReadItemsByIdRequest>
    {
        public ReadItemByIdRequestBuilder (string itemId)
        {
            ItemIdValidator.ValidateItemId (itemId);

            this.itemId = itemId;
        }

        public override IReadItemsByIdRequest Build()
        {
            ReadItemsByIdParameters result = new ReadItemsByIdParameters (null, this.itemSourceAccumulator, this.itemId);
            return result;
        }

        private string itemId;
    }
}

