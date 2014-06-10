

namespace Sitecore.MobileSdkUnitTest
{
    using System;
    using NUnit.Framework;

    using MobileSDKUnitTest.Mock;

    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    [TestFixture]
    public class ItemByPathUrlBuilderTests
    {
        private ItemByPathUrlBuilder builder;
        private ISessionConfig sessionConfig;
        private QueryParameters payload;

        [SetUp]
        public void SetUp()
        {
            IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
            IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

            this.builder = new ItemByPathUrlBuilder(restGrammar, webApiGrammar);

            SessionConfigPOD mutableSession = new SessionConfigPOD ();
            mutableSession.InstanceUrl = "http://mobiledev1ua1.dk.sitecore.net";
            mutableSession.ItemWebApiVersion = "v2";
            mutableSession.Site = "";
            this.sessionConfig = mutableSession;

            this.payload = new QueryParameters( PayloadType.Content );
        }

        [TearDown]
        public void TearDown()
        {
            this.builder = null;
            this.sessionConfig = null;
            this.payload = null;
        }

        [Test]
        public void TestBuildWithValidPath()
        {
            MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
            mutableParameters.ItemSource = ItemSource.DefaultSource ();
            mutableParameters.ItemPath = "/path/TO/iTEm";
            mutableParameters.SessionSettings = this.sessionConfig;

            IReadItemsByPathRequest request = mutableParameters;

            string result = this.builder.GetUrlForRequest(request);
            string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2%2fpath%2fto%2fitem?sc_database=web&sc_lang=en";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestBuildWithoutPayloadCausesArgumentNullException()
        {
            MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
            mutableParameters.ItemSource = ItemSource.DefaultSource ();
            mutableParameters.ItemPath = "/path/TO/iTEm";
            mutableParameters.SessionSettings = this.sessionConfig;
            mutableParameters.QueryParameters = null;

            IReadItemsByPathRequest request = mutableParameters;

            Assert.Throws<ArgumentNullException>( () => this.builder.GetUrlForRequest( request ) );
        }



        [Test]
        public void TestBuildWithUnEscapedPath()
        {
            MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
            mutableParameters.ItemSource = ItemSource.DefaultSource ();
            mutableParameters.ItemPath = "/path TO iTEm";
            mutableParameters.SessionSettings = this.sessionConfig;

            IReadItemsByPathRequest request = mutableParameters;

            string result = this.builder.GetUrlForRequest(request);
            string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2%2fpath%20to%20item?sc_database=web&sc_lang=en";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestBuildWithEmptyConfig()
        {
            TestDelegate action = () => this.builder.GetUrlForRequest(null);
            Assert.Throws<ArgumentNullException>(action);
        }

        [Test]
        public void TestPathMustStartWithSlash()
        {
            MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
            mutableParameters.ItemSource = ItemSource.DefaultSource ();
            mutableParameters.ItemPath = "path without starting slash";
            mutableParameters.SessionSettings = this.sessionConfig;

            IReadItemsByPathRequest request = mutableParameters;

            TestDelegate action = () => this.builder.GetUrlForRequest(request);
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        public void TestBuildWithEmptyPathCausesArgumentNullException()
        {
            MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
            mutableParameters.ItemSource = ItemSource.DefaultSource ();
            mutableParameters.ItemPath = "";
            mutableParameters.SessionSettings = this.sessionConfig;

            IReadItemsByPathRequest request = mutableParameters;

            TestDelegate action = () => this.builder.GetUrlForRequest(request);
            Assert.Throws<ArgumentNullException>(action);
        }


        [Test]
        public void TestBuildWithWhitespacePathCausesArgumentNullException()
        {
            MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
            mutableParameters.ItemSource = ItemSource.DefaultSource ();
            mutableParameters.ItemPath = "\r\n\t";
            mutableParameters.SessionSettings = this.sessionConfig;

            IReadItemsByPathRequest request = mutableParameters;

            TestDelegate action = () => this.builder.GetUrlForRequest(request);
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
