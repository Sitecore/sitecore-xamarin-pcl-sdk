

namespace MobileSDKUnitTest.Mock
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;

    public class MockGetItemsByIdParameters : IGetItemByIdRequest
    {
        public IItemSource ItemSource { get; set; }

        public ISessionConfig SessionSettings { get; set; }

        public string ItemId { get; set; }
    }
}
