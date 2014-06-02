using System;

namespace MobileSDKUnitTest.Mock
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;

    public class MockGetItemsByQueryParameters : IGetItemByQueryRequest
    {
        public IItemSource ItemSource { get; set; }

        public ISessionConfig SessionSettings { get; set; }

        public string SitecoreQuery { get; set; }
    }
}

