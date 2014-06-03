

namespace Sitecore.MobileSDK.Items
{
    using Sitecore.MobileSDK.PublicKey;
    using Sitecore.MobileSDK.SessionSettings;

    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;

    public class ReadItemByPathParameters : IReadItemsByPathRequest
    {
        public ReadItemByPathParameters(
            ISessionConfig sessionSettings, 
            IItemSource itemSource, 
            string itemPath)
        {
            this.SessionSettings = sessionSettings;
            this.ItemSource = itemSource;
            this.ItemPath = itemPath;
        }

        public string ItemPath { get; private set; }

        public IItemSource ItemSource { get; private set; }

        public ISessionConfig SessionSettings { get; private set; }
    }
}