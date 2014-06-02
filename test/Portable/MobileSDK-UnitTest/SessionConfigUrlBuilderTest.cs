
namespace Sitecore.MobileSdkUnitTest
{
    using System;
    using MobileSDKUnitTest.Mock;
    using NUnit.Framework;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;


    [TestFixture]
    public class SessionConfigUrlBuilderTest
    {
        [Test]
        public void TestBuildBaseUrlWithSite()
        {
            SessionConfigUrlBuilder builder = new SessionConfigUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());
            SessionConfigPOD mockConfig = new SessionConfigPOD();
            mockConfig.InstanceUrl = "localhost";
            mockConfig.ItemWebApiVersion = "v1";
            mockConfig.Site = "/sitecore/shell";

            string result = builder.BuildUrlString(mockConfig);
            string expected = "http://localhost/-/item/v1%2fsitecore%2fshell";

            Assert.AreEqual (expected, result);
        }

        public void TestBuildBaseUrlWithoutSite()
        {
            SessionConfigUrlBuilder builder = new SessionConfigUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());
            SessionConfigPOD mockConfig = new SessionConfigPOD();
            mockConfig.InstanceUrl = "localhost";
            mockConfig.ItemWebApiVersion = "v1";
            mockConfig.Site = null;

            string result = builder.BuildUrlString(mockConfig);
            string expected = "http://localhost/-/item/v1";

            Assert.AreEqual (expected, result);
        }

        [Test]
        public void TestBuildNullSessionConfig()
        {
            SessionConfigUrlBuilder builder = new SessionConfigUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());
            TestDelegate action = () => builder.BuildUrlString(null);

            Assert.Throws<ArgumentNullException>(action);
        }

		[Test]
		public void TestBuildWithInvalidSite()
		{
			SessionConfigUrlBuilder builder = new SessionConfigUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());
			SessionConfigPOD mockConfig = new SessionConfigPOD();
			mockConfig.InstanceUrl = "localhost";
			mockConfig.ItemWebApiVersion = "v1";

			TestDelegate action = () => mockConfig.Site = "sitecore/shell";
			Assert.Throws<ArgumentException>(action, "site must starts with '/'");
		}
    }
}

