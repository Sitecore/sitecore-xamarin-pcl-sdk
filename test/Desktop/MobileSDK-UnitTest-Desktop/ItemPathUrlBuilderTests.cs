namespace MobileSDK_UnitTest_Desktop
{
    using System;
    using MobileSDKUnitTest.Mock;
    using NUnit.Framework;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder;

    [TestFixture]
    public class ItemPathUrlBuilderTests
    {
        private ItemPathUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
            IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

            this.builder = new ItemPathUrlBuilder(restGrammar, webApiGrammar);
        }

        [TearDown]
        public void TearDown()
        {
            this.builder = null;
        }

        [Test]
        public void TestBuildWithValidPath()
        {
            ReadItemByPathParameters parameters = new ReadItemByPathParameters("http://mobiledev1ua1.dk.sitecore.net", "v2", "/path/to/item", null);

            string result = this.builder.GetUrlForRequest(parameters);
            string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2/path/to/item";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestBuildWithUnEscapedPath()
        {
            ReadItemByPathParameters parameters = new ReadItemByPathParameters("http://mobiledev1ua1.dk.sitecore.net", "v2", "/path to item", null);

            string result = this.builder.GetUrlForRequest(parameters);
            string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2/path%20to%20item";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestBuildWithEmptyConfig()
        {
            TestDelegate action = () => this.builder.GetUrlForRequest(null);
            Assert.Throws<ArgumentNullException>(action);
        }

        [Test]
        public void TestBuildWithInvalidPath()
        {
            ReadItemByPathParameters parameters = new ReadItemByPathParameters("http://mobiledev1ua1.dk.sitecore.net", "v2", "path", null);

            TestDelegate action = () => this.builder.GetUrlForRequest(parameters);
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void TestBuildWithEmptyPath()
        {
            ReadItemByPathParameters parameters = new ReadItemByPathParameters("http://mobiledev1ua1.dk.sitecore.net", "v2", "", null);

            TestDelegate action = () => this.builder.GetUrlForRequest(parameters);
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
