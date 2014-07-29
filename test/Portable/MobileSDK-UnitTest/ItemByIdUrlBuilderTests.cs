
namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


  [TestFixture]
  public class ItemByIdUrlBuilderTests
  {
    private ItemByIdUrlBuilder builder;
    private ISessionConfig sessionConfig;
    private ISessionConfig sitecoreShellConfig;
    private QueryParameters payload;

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

      this.payload = new QueryParameters( PayloadType.Min, null, null );
    }

    [TearDown]
    public void TearDown()
    {
      this.builder = null;
      this.sessionConfig = null;
    }

    [Test]
    public void TestNullPayloadIsNotReplacedWithDefault()
    {
      MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemId = "{   xxx   }";
      mutableParameters.QueryParameters = new QueryParameters(null, null, null);

      IReadItemsByIdRequest parameters = mutableParameters;

      string result = this.builder.GetUrlForRequest(parameters);
      string expected = "http://sitecore.net/-/item/v1?sc_database=web&language=en&sc_itemid=%7b%20%20%20xxx%20%20%20%7d";

      Assert.AreEqual(expected, result);
    }


    [Test]
    public void TestNullPayloadStructIsIgnored()
    {
      MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemId = "{   xxx   }";
      mutableParameters.QueryParameters = null;

      IReadItemsByIdRequest parameters = mutableParameters;

      string result = this.builder.GetUrlForRequest(parameters);
      string expected = "http://sitecore.net/-/item/v1?sc_database=web&language=en&sc_itemid=%7b%20%20%20xxx%20%20%20%7d";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestInvaliItemIdCausesArgumentException()
    {
      MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemId = "someInvalidItemId";
      mutableParameters.QueryParameters = this.payload;

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
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByIdRequest parameters = mutableParameters;

      string result = this.builder.GetUrlForRequest(parameters);
      string expected = "http://sitecore.net/-/item/v1?sc_database=web&language=en&payload=min&sc_itemid=%7b%20%20%20xxx%20%20%20%7d";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestUrlBuilderAddsItemSource()
    {
      MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
      mutableParameters.SessionSettings = this.sitecoreShellConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemId = "{   xxx   }";
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByIdRequest parameters = mutableParameters;

      string result = this.builder.GetUrlForRequest(parameters);
      string expected = "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v234%2fsitecore%2fshell?sc_database=web&language=en&payload=min&sc_itemid=%7b%20%20%20xxx%20%20%20%7d";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestUrlBuilderThrowsExceptionForNullItemId()
    {
      MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemId = null;
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByIdRequest parameters = mutableParameters;

      TestDelegate action = () => this.builder.GetUrlForRequest(parameters);

      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestBracesIdWithoutTextIsInvalid()
    {
      MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
      mutableParameters.SessionSettings = this.sessionConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemId = "{}";
      mutableParameters.QueryParameters = this.payload;

      IReadItemsByIdRequest parameters = mutableParameters;

      TestDelegate action = () => this.builder.GetUrlForRequest(parameters);
      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestOptionalSourceInSessionAndUserRequest()
    {
      var connection = new SessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{xxx-yyy-zzz}").Build();
      var requestMerger = new UserRequestMerger(connection, null);
      var mergedRequest = requestMerger.FillReadItemByIdGaps(request);

      var urlBuilder = new ItemByIdUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1?sc_itemid=%7bxxx-yyy-zzz%7d";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestOptionalSourceAndExplicitPayload()
    {
      var connection = new SessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{xxx-yyy-zzz}")
        .Payload(PayloadType.Full)
        .Build();
      var requestMerger = new UserRequestMerger(connection, null);
      var mergedRequest = requestMerger.FillReadItemByIdGaps(request);

      var urlBuilder = new ItemByIdUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1?payload=full&sc_itemid=%7bxxx-yyy-zzz%7d";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestExplicitDatabase()
    {
      var connection = new SessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{xxx-yyy-zzz}")
        .Database("master")
        .Build();
      var requestMerger = new UserRequestMerger(connection, null);
      var mergedRequest = requestMerger.FillReadItemByIdGaps(request);

      var urlBuilder = new ItemByIdUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1?sc_database=master&sc_itemid=%7bxxx-yyy-zzz%7d";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestDatabaseAndExplicitLanguageAndPayload()
    {
      var connection = new SessionConfig("localhost");

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{xxx-yyy-zzz}")
        .Language("da")
        .Payload(PayloadType.Content)
        .Build();
      var requestMerger = new UserRequestMerger(connection, null);
      var mergedRequest = requestMerger.FillReadItemByIdGaps(request);

      var urlBuilder = new ItemByIdUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());

      string result = urlBuilder.GetUrlForRequest(mergedRequest);
      string expected = "http://localhost/-/item/v1?language=da&payload=content&sc_itemid=%7bxxx-yyy-zzz%7d";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestDuplicateFieldsCauseException()
    {
      MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
      mutableParameters.SessionSettings = this.sitecoreShellConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemId = "{   xxx   }";

      string[] fields = { "x", "y", "x" };
      IQueryParameters duplicatedFields = new QueryParameters(null, null, fields);
      mutableParameters.QueryParameters = duplicatedFields;


      IReadItemsByIdRequest parameters = mutableParameters;
      Assert.Throws<ArgumentException>(() =>this.builder.GetUrlForRequest(parameters));
    }


    [Test]
    public void TestDuplicateFieldsWithDifferentCaseCauseException()
    {
      MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
      mutableParameters.SessionSettings = this.sitecoreShellConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemId = "{   xxx   }";

      string[] fields = { "x", "y", "X" };
      IQueryParameters duplicatedFields = new QueryParameters(null, null, fields);
      mutableParameters.QueryParameters = duplicatedFields;


      IReadItemsByIdRequest parameters = mutableParameters;
      Assert.Throws<ArgumentException>(() =>this.builder.GetUrlForRequest(parameters));
    }

    [Test]
    public void TestNullFieldEntriesCaseCauseException()
    {
      MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
      mutableParameters.SessionSettings = this.sitecoreShellConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemId = "{   xxx   }";

      string[] fields = { "x", "y", null };
      IQueryParameters duplicatedFields = new QueryParameters(null, null, fields);
      mutableParameters.QueryParameters = duplicatedFields;


      IReadItemsByIdRequest parameters = mutableParameters;
      Assert.Throws<ArgumentException>(() =>this.builder.GetUrlForRequest(parameters));
    }

    [Test]
    public void TestEmptyFieldEntriesCaseCauseException()
    {
      MockGetItemsByIdParameters mutableParameters = new MockGetItemsByIdParameters();
      mutableParameters.SessionSettings = this.sitecoreShellConfig;
      mutableParameters.ItemSource = ItemSource.DefaultSource();
      mutableParameters.ItemId = "{   xxx   }";

      string[] fields = { "x", "y", "" };
      IQueryParameters duplicatedFields = new QueryParameters(null, null, fields);
      mutableParameters.QueryParameters = duplicatedFields;


      IReadItemsByIdRequest parameters = mutableParameters;
      Assert.Throws<ArgumentException>(() =>this.builder.GetUrlForRequest(parameters));
    }
  }
}
