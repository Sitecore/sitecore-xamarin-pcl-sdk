namespace MobileSDK_UnitTest_Desktop
{
    using System;
    using NUnit.Framework;
    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
    using Sitecore.MobileSDK.UrlBuilder.Rest;
    using Sitecore.MobileSDK.UrlBuilder.WebApi;

    [TestFixture]
    public class QueryParametersUrlBuilderTest
    {
        private QueryParametersUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
            IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

            this.builder = new QueryParametersUrlBuilder(restGrammar, webApiGrammar);
        }

        [TearDown]
        public void TearDown()
        {
            this.builder = null;
        }

        [Test]
        public void TestBuildValidQueryParams()
        {
            string result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Content));
            Assert.AreEqual("payload=content", result);

            result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Full));
            Assert.AreEqual("payload=full", result);

            result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Min));
            Assert.AreEqual("payload=min", result);

            result = this.builder.BuildUrlString(new QueryParameters(PayloadType.None));
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void TestBuildNullQueryParams()
        {
            Assert.Throws<ArgumentNullException>(() => this.builder.BuildUrlString(null));
        }
    }
}