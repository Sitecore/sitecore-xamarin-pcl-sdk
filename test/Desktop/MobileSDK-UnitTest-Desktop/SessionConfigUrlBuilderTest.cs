
namespace MobileSDK_UnitTest_Desktop
{
    using System;
    using NUnit.Framework;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;


    [TestFixture]
    public class SessionConfigUrlBuilderTest
    {

        [Test]
        public void TestBuildBaseUrlWithSite()
        {
            SessionConfigUrlBuilder builder = new SessionConfigUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());
            ISessionConfig data = new SessionConfig("localhost", "admin", "b", "/sitecore/shell", "v1");

            string result = builder.BuildUrlString(data);
            string expected = "http://localhost/sitecore/shell/-/item/v1";
        }
    }
}
