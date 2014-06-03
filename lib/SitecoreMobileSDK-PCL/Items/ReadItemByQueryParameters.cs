
namespace Sitecore.MobileSDK
{
    using System;
    using Sitecore.MobileSDK.PublicKey;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;

    public class ReadItemByQueryParameters : IGetItemByQueryRequest
    {
        public ReadItemByQueryParameters(
            ISessionConfig sessionSettings, 
            IItemSource itemSource, 
            string sitecoreQuery)
        {
            this.SessionSettings = sessionSettings;
            this.ItemSource = itemSource;
            this.SitecoreQuery = sitecoreQuery;
        }

        public string SitecoreQuery { get; private set; }

        public IItemSource ItemSource { get; private set; }

        public ISessionConfig SessionSettings { get; private set; }
    }
}

