

namespace MobileSDKUnitTest.Mock
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;

    public class MockGetItemsByPathParameters : IGetItemByPathRequest
    {
        public IItemSource ItemSource { get; private set; }

        public ISessionConfig SessionSettings { get; private set; }

        public string ItemPath { get; private set; }
    }
}
