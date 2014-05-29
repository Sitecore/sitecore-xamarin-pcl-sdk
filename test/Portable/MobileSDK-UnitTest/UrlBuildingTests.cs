namespace MobileSDK_UnitTest_Desktop
{
    using System;
    using MobileSDKUnitTest.Mock;
    using NUnit.Framework;

    using Sitecore.MobileSDK.UrlBuilder;

    [TestFixture]
    public class UrlBuildingTests
    {
        private WebApiUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
            IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

            this.builder = new WebApiUrlBuilder(restGrammar, webApiGrammar);
        }

        [TearDown]
        public void TearDown()
        {
            this.builder = null;
        }

        [Test]
        public void TestBuildValidParams()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters();
            mockParams.InstanceUrl = "https://localhost:80";
            mockParams.WebApiVersion = "v1";
            mockParams.ItemId = "{000-111-222-333}";

            IRequestConfig itemInfo = mockParams;

            string result = this.builder.GetUrlForRequest(itemInfo);
            string expected = "https://localhost:80/-/item/v1?sc_itemid=%7b000-111-222-333%7d";

            Assert.AreEqual(expected, result);
        }


        [Test]
        public void TestCapitalizedUrlSchemeIsHandledCorrectly()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters();
            mockParams.InstanceUrl = "HTTP://localhost:80";
            mockParams.WebApiVersion = "v1";
            mockParams.ItemId = "{000-111-222-333}";

            IRequestConfig itemInfo = mockParams;

            string result = this.builder.GetUrlForRequest(itemInfo);
            string expected = "http://localhost:80/-/item/v1?sc_itemid=%7b000-111-222-333%7d";

            Assert.AreEqual(expected, result);
        }


        [Test]
        public void TestUrlSchemeIsHttpByDefault()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "SITECORE.net",
                WebApiVersion = "V100500",
                ItemId = "{TEST-GUID-666-13-666}"
            };

            IRequestConfig itemInfo = mockParams;

            string result = this.builder.GetUrlForRequest(itemInfo);
            string expected = "http://sitecore.net/-/item/v100500?sc_itemid=%7btest-guid-666-13-666%7d";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestBuildInvalidParams()
        {
            TestDelegate action = () => this.builder.GetUrlForRequest(null);

            Assert.Throws<ArgumentNullException>(action);
        }

        [Test]
        public void TestInvalidItemId()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "SITECORE.net",
                WebApiVersion = "V100500",
                ItemId = "/path/to/item"
            };

            IRequestConfig itemInfo = mockParams;
            TestDelegate action = () => this.builder.GetUrlForRequest(itemInfo);

            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void TestUrlBuilderExcapesArgs()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "SITECORE.net",
                WebApiVersion = "V{1}",
                ItemId = "{   xxx   }"
            };

            IRequestConfig itemInfo = mockParams;

            string result = this.builder.GetUrlForRequest(itemInfo);
            string expected = "http://sitecore.net/-/item/v%7b1%7d?sc_itemid=%7b%20%20%20xxx%20%20%20%7d";

            Assert.AreEqual(expected, result);
        }


        public void TestUrlBuilderDoesNotExcapesHost()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "SITECORE.net(((}}}",
                WebApiVersion = "V{1}",
                ItemId = "{   xxx   }"
            };

            IRequestConfig itemInfo = mockParams;

            string result = this.builder.GetUrlForRequest(itemInfo);
            string expected = "http://sitecore.net(((}}}/-/item/v%7b1%7d?sc_itemid=%7b%20%20%20xxx%20%20%20%7d";

            Assert.AreEqual(expected, result);
        }
    }
}
