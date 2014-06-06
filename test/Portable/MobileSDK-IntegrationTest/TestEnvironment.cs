
namespace MobileSDKIntegrationTest
{
    public class TestEnvironment
    {
        public static TestEnvironment DefaultTestEnvironment()
        {
            TestEnvironment result = new TestEnvironment();

            result.AnonymousInstanceURL = "http://mobiledev1ua1.dk.sitecore.net:750";
            result.AuthenticatedInstanceURL = "http://mobiledev1ua1.dk.sitecore.net:7119";
            result.AdminUsername = "sitecore\\admin";
            result.AdminPassword = "b";
            result.HomeItemId = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";
            result.HomeItemPath = "/sitecore/content/Home";

            return result;
        }

        private TestEnvironment ()
        {
        }

        public string AnonymousInstanceURL { get; private set; }
        public string AuthenticatedInstanceURL { get; private set; }
        public string AdminUsername { get; private set; }
        public string AdminPassword { get; private set; }
        public string HomeItemId { get; private set; }
        public string HomeItemPath { get; private set; }
    }
}

