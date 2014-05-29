

namespace MobileSDKUnitTest.Mock
{
    using Sitecore.MobileSDK.UrlBuilder;

    public class MockReadItemByIdParameters : IRequestConfig
    {
        public string InstanceUrl { get; set; }

        public string WebApiVersion { get; set; }

        public string ItemId { get; set; }
    }
}
