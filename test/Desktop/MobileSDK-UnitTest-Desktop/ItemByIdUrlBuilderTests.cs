namespace MobileSDK_UnitTest_Desktop
{
    using System;
    using MobileSDKUnitTest.Mock;
    using NUnit.Framework;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.UrlBuilder;

    [TestFixture]
    public class ItemByIdUrlBuilderTests
    {
        private ItemByIdUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
            IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

            this.builder = new ItemByIdUrlBuilder(restGrammar, webApiGrammar);
        }

        [TearDown]
        public void TearDown()
        {
            this.builder = null;
        }

        [Test]
        public void TestInvalidItemId()
        {
            ReadItemByIdParameters parameters = new ReadItemByIdParameters("SITECORE.net", "V100500", "/path/to/item", null);

            TestDelegate action = () => this.builder.GetUrlForRequest(parameters);
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void TestUrlBuilderExcapesArgs()
        {
            ReadItemByIdParameters parameters = new ReadItemByIdParameters("SITECORE.net", "V{1}", "{   xxx   }", null);

            string result = this.builder.GetUrlForRequest(parameters);
            string expected = "http://sitecore.net/-/item/v%7b1%7d?sc_itemid=%7b%20%20%20xxx%20%20%20%7d";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestUrlBuilderHandlesNullItemId()
        {
            ReadItemByIdParameters parameters = new ReadItemByIdParameters("SITECORE.net", "V{1}", null, null);
            TestDelegate action = () =>
            {
                string result = this.builder.GetUrlForRequest(parameters);
            };

            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
