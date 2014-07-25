

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using MobileSDKUnitTest.Mock;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UserRequest;
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

      this.payload = new QueryParameters( PayloadType.Content, null, null );
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
      MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.ItemPath = "/path/TO/iTEm";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = new QueryParameters(null, null, null);

      IReadItemsByPathRequest request = mutableParameters;

      string result = this.builder.GetUrlForRequest(request);
      string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2%2fpath%2fto%2fitem?sc_database=web&language=en";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestNullPayloadStructIsIgnored()
    {
      MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.ItemPath = "/path/TO/iTEm";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = null;

      IReadItemsByPathRequest request = mutableParameters;

      string result = this.builder.GetUrlForRequest(request);
      string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2%2fpath%2fto%2fitem?sc_database=web&language=en";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestBuildWithValidPath()
    {
      MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.ItemPath = "/path/TO/iTEm";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByPathRequest request = mutableParameters;

      string result = this.builder.GetUrlForRequest(request);
      string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2%2fpath%2fto%2fitem?sc_database=web&language=en&payload=content";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestBuildWithoutPayloadIsAllowed()
    {
      MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.ItemPath = "/path/TO/iTEm";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = null;

      IReadItemsByPathRequest request = mutableParameters;
      string result = this.builder.GetUrlForRequest(request);
      string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2%2fpath%2fto%2fitem?sc_database=web&language=en";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestBuildWithUnEscapedPath()
    {
      MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.ItemPath = "/path TO iTEm";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByPathRequest request = mutableParameters;

      string result = this.builder.GetUrlForRequest(request);
      string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2%2fpath%20to%20item?sc_database=web&language=en&payload=content";

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
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByPathRequest request = mutableParameters;

      TestDelegate action = () => this.builder.GetUrlForRequest(request);
      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestBuildWithEmptyPathCausesArgumentException()
    {
      MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.ItemPath = "";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByPathRequest request = mutableParameters;

      TestDelegate action = () => this.builder.GetUrlForRequest(request);
      Assert.Throws<ArgumentException>(action);
    }


    [Test]
    public void TestBuildWithWhitespacePathCausesArgumentException()
    {
      MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.ItemPath = "\r\n\t";
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.QueryParameters = this.payload;


      IReadItemsByPathRequest request = mutableParameters;

      TestDelegate action = () => this.builder.GetUrlForRequest(request);
      Assert.Throws<ArgumentException>(action);
    }


    [Test]
    public void TestBuildRequestWithPathAndFieldList()
    {
      MockGetItemsByPathParameters mutableParameters = new MockGetItemsByPathParameters ();
      mutableParameters.ItemSource = ItemSource.DefaultSource ();
      mutableParameters.ItemPath = "/path/TO/iTEm";
      mutableParameters.SessionSettings = this.sessionConfig;

      QueryParameters fieldsList = new QueryParameters(PayloadType.Default, null, new string[2]{ "x", "y" });
      mutableParameters.QueryParameters = fieldsList;

      IReadItemsByPathRequest request = mutableParameters;

      string result = this.builder.GetUrlForRequest(request);
      string expected = "http://mobiledev1ua1.dk.sitecore.net/-/item/v2%2fpath%2fto%2fitem?sc_database=web&language=en&payload=min&fields=x|y";

      Assert.AreEqual(expected, result);
    }


    [Test]
    public void TestOptionalSourceInSessionAndUserRequest()
    {
      var connection = new SessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/oO").Build();
      var requestMerger = new UserRequestMerger(connection, null);
      var mergedRequest = requestMerger.FillReadItemByPathGaps(request);

      var urlBuilder = new ItemByPathUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1%2fsitecore%2fcontent%2foo";

      Assert.AreEqual(expected, result);
    }


    [Test]
    public void TestOptionalSourceAndExplicitPayload()
    {
      var connection = new SessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/oO")
        .Payload(PayloadType.Full)
        .Build();
      var requestMerger = new UserRequestMerger(connection, null);
      var mergedRequest = requestMerger.FillReadItemByPathGaps(request);

      var urlBuilder = new ItemByPathUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1%2fsitecore%2fcontent%2foo?payload=full";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestExplicitDatabase()
    {
      var connection = new SessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/oO")
        .Database("master")
        .Build();
      var requestMerger = new UserRequestMerger(connection, null);
      var mergedRequest = requestMerger.FillReadItemByPathGaps(request);

      var urlBuilder = new ItemByPathUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1%2fsitecore%2fcontent%2foo?sc_database=master";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestDatabaseAndExplicitLanguageAndPayload()
    {
      var connection = new SessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/oO")
        .Language("da")
        .Payload(PayloadType.Content)
        .Build();
      var requestMerger = new UserRequestMerger(connection, null);
      var mergedRequest = requestMerger.FillReadItemByPathGaps(request);

      var urlBuilder = new ItemByPathUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1%2fsitecore%2fcontent%2foo?language=da&payload=content";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestDuplicateFieldsCauseException()
    {
      var mutableParameters = new MockGetItemsByPathParameters();
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemPath = "/aaa/bbb/ccc/ddd";

      string[] fields = { "x", "y", "x" };
      IQueryParameters duplicatedFields = new QueryParameters(null, null, fields);
      mutableParameters.QueryParameters = duplicatedFields;


      IReadItemsByPathRequest parameters = mutableParameters;
      Assert.Throws<ArgumentException>(() =>this.builder.GetUrlForRequest(parameters));
    }


    [Test]
    public void TestDuplicateFieldsWithDifferentCaseCauseException()
    {
      var mutableParameters = new MockGetItemsByPathParameters();
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemPath = "/aaa/bbb/ccc/ddd";


      string[] fields = { "x", "y", "X" };
      IQueryParameters duplicatedFields = new QueryParameters(null, null, fields);
      mutableParameters.QueryParameters = duplicatedFields;


      IReadItemsByPathRequest parameters = mutableParameters;
      Assert.Throws<ArgumentException>(() =>this.builder.GetUrlForRequest(parameters));
    }
  }
}
