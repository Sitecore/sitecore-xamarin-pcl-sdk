

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;




  [TestFixture]
  public class ItemByQueryUrlBuilderTest
  {
    private ItemByQueryUrlBuilder builder;
    private ISessionConfig sessionConfig;
    private QueryParameters payload;

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

      this.payload = new QueryParameters( PayloadType.Full, null, null );
    }

    [TearDown]
    public void TearDown()
    {
      this.builder = null;
      this.sessionConfig = null;
      this.payload = null;
    }

    [Test]
    public void TestNullPayloadIsNotReplacedWithDefault()
    {
      MockGetItemsByQueryParameters mutableParameters = new MockGetItemsByQueryParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.SitecoreQuery = "/Sitecore/Content/*";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = new QueryParameters(null, null, null);

      IReadItemsByQueryRequest request = mutableParameters;

      string result = this.builder.GetUrlForRequest(request);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:722/-/item/v13?sc_database=web&language=en&query=%2fSitecore%2fContent%2f%2a";

      Assert.AreEqual(expected, result);
    }


    [Test]
    public void TestNullPayloadStructIsIgnored()
    {
      MockGetItemsByQueryParameters mutableParameters = new MockGetItemsByQueryParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.SitecoreQuery = "/Sitecore/Content/*";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = null;

      IReadItemsByQueryRequest request = mutableParameters;

      string result = this.builder.GetUrlForRequest(request);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:722/-/item/v13?sc_database=web&language=en&query=%2fSitecore%2fContent%2f%2a";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestBuildWithFastQuery()
    {
      MockGetItemsByQueryParameters mutableParameters = new MockGetItemsByQueryParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.SitecoreQuery = "fast:/sitecore/content/Home/Products/*[@@name = 'Hammer']";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByQueryRequest request = mutableParameters;

      string result = this.builder.GetUrlForRequest(request);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:722/-/item/v13?sc_database=web&language=en&payload=full&query=fast%3a%2fsitecore%2fcontent%2fHome%2fProducts%2f%2a%5b%40%40name%20%3d%20%27Hammer%27%5d";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestBuildWithRegularQuery()
    {
      MockGetItemsByQueryParameters mutableParameters = new MockGetItemsByQueryParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.SitecoreQuery = "/Sitecore/Content/*";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByQueryRequest request = mutableParameters;

      string result = this.builder.GetUrlForRequest(request);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:722/-/item/v13?sc_database=web&language=en&payload=full&query=%2fSitecore%2fContent%2f%2a";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestQueryCannotBeNull()
    {
      MockGetItemsByQueryParameters mutableParameters = new MockGetItemsByQueryParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.SitecoreQuery = null;
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByQueryRequest request = mutableParameters;

      Assert.Throws<ArgumentException> (() => this.builder.GetUrlForRequest (request));
    }

    [Test]
    public void TestQueryCannotBeEmpty()
    {
      MockGetItemsByQueryParameters mutableParameters = new MockGetItemsByQueryParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.SitecoreQuery = "";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByQueryRequest request = mutableParameters;

      Assert.Throws<ArgumentException>(() => this.builder.GetUrlForRequest(request));
    }

    [Test]
    public void TestQueryCannotBeWhitespace()
    {
      MockGetItemsByQueryParameters mutableParameters = new MockGetItemsByQueryParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.SitecoreQuery = "   \t \r \n   ";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByQueryRequest request = mutableParameters;

      Assert.Throws<ArgumentException>(() => this.builder.GetUrlForRequest(request));
    }
  
    [Test]
    public void TestOptionalSourceInSessionAndUserRequest()
    {
      var anonymous = SessionConfig.NewAnonymousSessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/home/*").Build();
      var requestMerger = new UserRequestMerger(anonymous, null);
      var mergedRequest = requestMerger.FillReadItemByQueryGaps(request);

      var urlBuilder = new ItemByQueryUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1?query=%2fsitecore%2fcontent%2fhome%2f%2a";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestOptionalSourceAndExplicitPayload()
    {
      var anonymous = SessionConfig.NewAnonymousSessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/home/*")
        .Payload(PayloadType.Full)
        .Build();
      var requestMerger = new UserRequestMerger(anonymous, null);
      var mergedRequest = requestMerger.FillReadItemByQueryGaps(request);

      var urlBuilder = new ItemByQueryUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1?payload=full&query=%2fsitecore%2fcontent%2fhome%2f%2a";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestExplicitDatabase()
    {
      var anonymous = SessionConfig.NewAnonymousSessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/home/*")
        .Database("master")
        .Build();
      var requestMerger = new UserRequestMerger(anonymous, null);
      var mergedRequest = requestMerger.FillReadItemByQueryGaps(request);

      var urlBuilder = new ItemByQueryUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1?sc_database=master&query=%2fsitecore%2fcontent%2fhome%2f%2a";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestDatabaseAndExplicitLanguageAndPayload()
    {
      var anonymous = SessionConfig.NewAnonymousSessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/home/*")
        .Language("da")
        .Payload(PayloadType.Content)
        .Build();
      var requestMerger = new UserRequestMerger(anonymous, null);
      var mergedRequest = requestMerger.FillReadItemByQueryGaps(request);

      var urlBuilder = new ItemByQueryUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1?language=da&payload=content&query=%2fsitecore%2fcontent%2fhome%2f%2a";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestDuplicateFieldsCauseException()
    {
      var mutableParameters = new MockGetItemsByQueryParameters();
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.SitecoreQuery = "/aaa/bbb/ccc/*";

      string[] fields = { "x", "y", "x" };
      IQueryParameters duplicatedFields = new QueryParameters(null, null, fields);
      mutableParameters.QueryParameters = duplicatedFields;


      IReadItemsByQueryRequest parameters = mutableParameters;
      Assert.Throws<ArgumentException>(() =>this.builder.GetUrlForRequest(parameters));
    }


    [Test]
    public void TestDuplicateFieldsWithDifferentCaseCauseException()
    {
      var mutableParameters = new MockGetItemsByQueryParameters();
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.SitecoreQuery = "/aaa/bbb/ccc/*";


      string[] fields = { "x", "y", "X" };
      IQueryParameters duplicatedFields = new QueryParameters(null, null, fields);
      mutableParameters.QueryParameters = duplicatedFields;


      IReadItemsByQueryRequest parameters = mutableParameters;
      Assert.Throws<ArgumentException>(() =>this.builder.GetUrlForRequest(parameters));
    }
  }
}

