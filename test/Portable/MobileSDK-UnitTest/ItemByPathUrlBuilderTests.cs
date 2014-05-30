namespace MobileSDK_UnitTest_Desktop
{
    using System;
    using MobileSDKUnitTest.Mock;
    using NUnit.Framework;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;

    [TestFixture]
    public class ItemByPathUrlBuilderTests
    {
        private ItemByPathUrlBuilder builder;
        private ISessionConfig sessionConfig;

        [SetUp]
        public void SetUp()
        {
            IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
            IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

            this.builder = new ItemByPathUrlBuilder(restGrammar, webApiGrammar);
            this.sessionConfig = new SessionConfig("http://sitecore.net", "admin", "b");
        }

        [TearDown]
        public void TearDown()
        {
            this.builder = null;
            this.sessionConfig = null;
        }
//
//        [Test]
//        public void TestBuildWithValidPath()
//        {
//            IGetItemByPathRequest request = null;
//
//            ReadItemByPathParameters parameters = new ReadItemByPathParameters("http://mobiledev1ua1.dk.sitecore.net", "v2", "/path/to/item", null);
//
//            string result = this.builder.GetUrlForRequest(parameters);
//            string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2/path/to/item";
//
//            Assert.AreEqual(expected, result);
//        }
//
//        [Test]
//        public void TestBuildWithUnEscapedPath()
//        {
//            ReadItemByPathParameters parameters = new ReadItemByPathParameters("http://mobiledev1ua1.dk.sitecore.net", "v2", "/path to item", null);
//
//            string result = this.builder.GetUrlForRequest(parameters);
//            string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2/path%20to%20item";
//
//            Assert.AreEqual(expected, result);
//        }
//
//        [Test]
//        public void TestBuildWithEmptyConfig()
//        {
//            TestDelegate action = () => this.builder.GetUrlForRequest(null);
//            Assert.Throws<ArgumentNullException>(action);
//        }
//
//        [Test]
//        public void TestBuildWithInvalidPath()
//        {
//            ReadItemByPathParameters parameters = new ReadItemByPathParameters("http://mobiledev1ua1.dk.sitecore.net", "v2", "path", null);
//
//            TestDelegate action = () => this.builder.GetUrlForRequest(parameters);
//            Assert.Throws<ArgumentException>(action);
//        }
//
//        [Test]
//        public void TestBuildWithEmptyPath()
//        {
//            ReadItemByPathParameters parameters = new ReadItemByPathParameters("http://mobiledev1ua1.dk.sitecore.net", "v2", "", null);
//
//            TestDelegate action = () => this.builder.GetUrlForRequest(parameters);
//            Assert.Throws<ArgumentNullException>(action);
//        }
    }
}
