namespace MobileSDK_UnitTest_Desktop
{
    using System;
    using MobileSDKUnitTest.Mock;
    using NUnit.Framework;
    using Sitecore.MobileSDK.UrlBuilder;

    [TestFixture]
    public class ItemByIdUrlBuilderTests
    {
        private ItemIdUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
            IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

            this.builder = new ItemIdUrlBuilder(restGrammar, webApiGrammar);
        }

        [TearDown]
        public void TearDown()
        {
            this.builder = null;
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

        [Test]
        public void TestUrlBuilderHandlesEmptyItemId()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "SITECORE.net",
                WebApiVersion = "V{1}",
            };

            IRequestConfig itemInfo = mockParams;

            TestDelegate action = () =>
            {
                string result = this.builder.GetUrlForRequest(itemInfo);
            };

            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
