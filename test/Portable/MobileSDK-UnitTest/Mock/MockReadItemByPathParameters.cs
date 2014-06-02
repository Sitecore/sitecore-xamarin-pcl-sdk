

namespace MobileSDKUnitTest.Mock
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;

    public class MockGetItemsByPathParameters : IGetItemByPathRequest
    {
        public IItemSource ItemSource { get; set; }

        public ISessionConfig SessionSettings { get; set; }

        public string ItemPath { get; set; }
    }
}
