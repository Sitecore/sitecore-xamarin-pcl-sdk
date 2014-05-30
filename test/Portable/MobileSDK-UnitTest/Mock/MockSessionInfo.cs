
namespace MobileSDKUnitTest.Mock
{
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;

    public class SessionConfigPOD : ISessionConfig
    {
        public string InstanceUrl { get; set; }

        public string ItemWebApiVersion { get; set; }

        public string Site { get; set; }
    }
}
