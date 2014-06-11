using System;

namespace MobileSDKUnitTest.Mock
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    public class MockGetItemsByQueryParameters : IReadItemsByQueryRequest
    {
        public IItemSource ItemSource { get; set; }

        public ISessionConfig SessionSettings { get; set; }
        
        public IQueryParameters QueryParameters { get; set; }

        public string SitecoreQuery { get; set; }
    }
}

