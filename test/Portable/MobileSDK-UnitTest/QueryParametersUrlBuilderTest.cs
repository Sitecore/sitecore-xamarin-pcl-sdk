namespace MobileSDK_UnitTest_Desktop
{
  using NUnit.Framework;
  using Sitecore.MobileSDK.API.Request.Parameters;
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
    public void TestQueryParametersWithNullFieldsProduceNullString()
    {
      string result = this.builder.BuildUrlString(new QueryParameters(null));
      Assert.AreEqual(string.Empty, result);
    }

    [Test]
    public void TestNullQueryParamsProduceNullString()
    {
      string result = this.builder.BuildUrlString(null);
      Assert.IsNull(result);
    }

    [Test]
    public void TestSimpleFieldListOfNamesIsProcessedCorrectly()
    {
      string[] fields = { "abra", "shwabra", "kadabra" };

      string result = this.builder.BuildUrlString(new QueryParameters(fields));
      Assert.AreEqual("payload=full&fields=abra|shwabra|kadabra", result);
    }


    [Test]
    public void TestFieldListOfNamesIsUrlEncodedCorrectly()
    {
      string[] fields = { "Слава Україні!", "Героям слава!" };

      string result = this.builder.BuildUrlString(new QueryParameters(fields));
      string expected = "payload=content&fields=%d0%a1%d0%bb%d0%b0%d0%b2%d0%b0%20%d0%a3%d0%ba%d1%80%d0%b0%d1%97%d0%bd%d1%96%21|%d0%93%d0%b5%d1%80%d0%be%d1%8f%d0%bc%20%d1%81%d0%bb%d0%b0%d0%b2%d0%b0%21";
      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestFieldsOnly()
    {
      string[] fields = { "Слава Україні!", "{0000-1111-2222}", "Героям слава!" };

      string result = this.builder.BuildUrlString(new QueryParameters(fields));
      string expected = "fields=%d0%a1%d0%bb%d0%b0%d0%b2%d0%b0%20%d0%a3%d0%ba%d1%80%d0%b0%d1%97%d0%bd%d1%96%21|%7b0000-1111-2222%7d|%d0%93%d0%b5%d1%80%d0%be%d1%8f%d0%bc%20%d1%81%d0%bb%d0%b0%d0%b2%d0%b0%21";
      Assert.AreEqual(expected, result);
    }
  }
}