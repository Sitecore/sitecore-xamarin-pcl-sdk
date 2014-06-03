namespace Sitecore.MobileSDK
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.PublicKey;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;

    public class ReadItemsByIdParameters : IReadItemsByIdRequest
    {
        public ReadItemsByIdParameters(ISessionConfig sessionSettings, IItemSource itemSource, string itemId)
        {
            this.SessionSettings = sessionSettings;
            this.ItemSource = itemSource;
            this.ItemId = itemId;
        }

        public string ItemId { get; private set; }

        public IItemSource ItemSource { get; private set; }

        public ISessionConfig SessionSettings { get; private set; }
    }
}

