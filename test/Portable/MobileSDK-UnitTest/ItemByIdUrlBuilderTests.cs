namespace Sitecore.MobileSdkUnitTest
{
    using System;
    using NUnit.Framework;

    using MobileSDKUnitTest.Mock;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;

    [TestFixture]
    public class ItemByIdUrlBuilderTests
    {
        private ItemByIdUrlBuilder builder;
        private ISessionConfig sessionConfig;
        private ISessionConfig sitecoreShellConfig;


        [SetUp]
        public void SetUp()
        {
            IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
            IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

            this.builder = new ItemByIdUrlBuilder(restGrammar, webApiGrammar);

            SessionConfigPOD mutableSessionConfig = new SessionConfigPOD();
            mutableSessionConfig.ItemWebApiVersion = "v1";
            mutableSessionConfig.InstanceUrl = "sitecore.net";
            mutableSessionConfig.Site = null;
            this.sessionConfig = mutableSessionConfig;


            mutableSessionConfig = new SessionConfigPOD();
            mutableSessionConfig.ItemWebApiVersion = "v234";
            mutableSessionConfig.InstanceUrl = "mobiledev1ua1.dk.sitecore.net:7119";
            mutableSessionConfig.Site = "/sitecore/shell";
            this.sitecoreShellConfig = mutableSessionConfig;
        }

        [TearDown]
        public void TearDown()
        {
            this.builder = null;
            this.sessionConfig = null;
        }

		[Test]
		public void TestInvaliSite()
		{
			MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
			mutableParameters.SessionSettings = this.sessionConfig;
			mutableParameters.ItemSource = ItemSource.DefaultSource();
			mutableParameters.ItemId = "someInvalidItemId";

			IReadItemsByIdRequest parameters = mutableParameters;

			TestDelegate action = () => this.builder.GetUrlForRequest(parameters);
			Assert.Throws<ArgumentException>(action);
		}

        [Test]
        public void TestUrlBuilderExcapesArgs()
        {
            MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
            mutableParameters.SessionSettings = this.sessionConfig;
            mutableParameters.ItemSource = ItemSource.DefaultSource();
            mutableParameters.ItemId = "{   xxx   }";

            IReadItemsByIdRequest parameters = mutableParameters;

            string result = this.builder.GetUrlForRequest(parameters);
            string expected = "http://sitecore.net/-/item/v1?sc_database=web&sc_lang=en&sc_itemid=%7b%20%20%20xxx%20%20%20%7d";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestUrlBuilderAddsItemSource()
        {
            MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
            mutableParameters.SessionSettings = this.sitecoreShellConfig;
            mutableParameters.ItemSource = ItemSource.DefaultSource();
            mutableParameters.ItemId = "{   xxx   }";

            IReadItemsByIdRequest parameters = mutableParameters;

            string result = this.builder.GetUrlForRequest(parameters);
            string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell?sc_database=web&sc_lang=en&sc_itemid=%7b%20%20%20xxx%20%20%20%7d";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestUrlBuilderHandlesNullItemId()
        {
            MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
            mutableParameters.SessionSettings = this.sessionConfig;
            mutableParameters.ItemSource = null;
            mutableParameters.ItemId = null;

            IReadItemsByIdRequest parameters = mutableParameters;

            TestDelegate action = () => this.builder.GetUrlForRequest(parameters);

            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
