namespace MobileSDK_UnitTest_Desktop
{
    using System;
    using MobileSDKUnitTest.Mock;
    using NUnit.Framework;

    using Sitecore.MobileSDK.UrlBuilder;

    [TestFixture]
    public class WebApiUrlBuilderTests
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
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "https://localhost:80",
                WebApiVersion = "v1"
            };

            IRequestConfig itemInfo = mockParams;

            string result = this.builder.GetUrlForRequest(itemInfo);
            string expected = "https://localhost:80/-/item/v1";

            Assert.AreEqual(expected, result);
        }


        [Test]
        public void TestCapitalizedUrlSchemeIsHandledCorrectly()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "HTTP://localhost:80",
                WebApiVersion = "v1"
            };

            IRequestConfig itemInfo = mockParams;

            string result = this.builder.GetUrlForRequest(itemInfo);
            string expected = "http://localhost:80/-/item/v1";

            Assert.AreEqual(expected, result);
        }


        [Test]
        public void TestUrlSchemeIsHttpByDefault()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "SITECORE.net",
                WebApiVersion = "V100500",
            };

            IRequestConfig itemInfo = mockParams;

            string result = this.builder.GetUrlForRequest(itemInfo);
            string expected = "http://sitecore.net/-/item/v100500";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestBuildInvalidParams()
        {
            TestDelegate action = () => this.builder.GetUrlForRequest(null);

            Assert.Throws<ArgumentNullException>(action);
        }

        public void TestUrlBuilderDoesNotExcapesHost()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "SITECORE.net(((}}}",
                WebApiVersion = "V{1}",
            };

            IRequestConfig itemInfo = mockParams;

            string result = this.builder.GetUrlForRequest(itemInfo);
            string expected = "http://sitecore.net(((}}}/-/item/v%7b1%7d";

            Assert.AreEqual(expected, result);
        }
    }
}
