


namespace Sitecore.MobileSdkUnitTest
{
    using System;
    using NUnit.Framework;

    using MobileSDKUnitTest.Mock;

    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;


    [TestFixture]
    public class ItemByQueryUrlBuilderTest
    {
        private ItemByQueryUrlBuilder builder;
        private ISessionConfig sessionConfig;

        [SetUp]
        public void SetUp()
        {
            IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
            IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

            this.builder = new ItemByQueryUrlBuilder(restGrammar, webApiGrammar);

            SessionConfigPOD mutableSession = new SessionConfigPOD ();
            mutableSession.InstanceUrl = "http://mobiledev1ua1.dk.sitecore.net:722";
            mutableSession.ItemWebApiVersion = "v13";
            mutableSession.Site = null;
            this.sessionConfig = mutableSession;
        }

        [TearDown]
        public void TearDown()
        {
            this.builder = null;
            this.sessionConfig = null;
        }


        [Test]
        public void TestBuildWithFastQuery()
        {
            MockGetItemsByQueryParameters mutableParameters = new MockGetItemsByQueryParameters ();
            mutableParameters.ItemSource = ItemSource.DefaultSource ();
            mutableParameters.SitecoreQuery = "fast:/sitecore/content/Home/Products/*[@@name = 'Hammer']";
            mutableParameters.SessionSettings = this.sessionConfig;

            IReadItemsByQueryRequest request = mutableParameters;

            string result = this.builder.GetUrlForRequest(request);
            string expected = "http://mobiledev1ua1.dk.sitecore.net:722/-/item/v13?sc_database=web&sc_lang=en&query=fast%3a%2fsitecore%2fcontent%2fhome%2fproducts%2f%2a%5b%40%40name%20%3d%20%27hammer%27%5d";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestBuildWithRegularQuery()
        {
            MockGetItemsByQueryParameters mutableParameters = new MockGetItemsByQueryParameters ();
            mutableParameters.ItemSource = ItemSource.DefaultSource ();
            mutableParameters.SitecoreQuery = "/Sitecore/Content/*";
            mutableParameters.SessionSettings = this.sessionConfig;

            IReadItemsByQueryRequest request = mutableParameters;

            string result = this.builder.GetUrlForRequest(request);
            string expected = "http://mobiledev1ua1.dk.sitecore.net:722/-/item/v13?sc_database=web&sc_lang=en&query=%2fsitecore%2fcontent%2f%2a";

            Assert.AreEqual(expected, result);
        }
    }

}

