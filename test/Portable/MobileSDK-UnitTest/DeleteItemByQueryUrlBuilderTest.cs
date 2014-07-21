namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UrlBuilder.Rest;

  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using SitecoreMobileSDKMockObjects;

  [TestFixture]
  public class DeleteItemByQueryUrlBuilderTest
  {
    private SessionConfig sessionConfig;
    private ScopeParameters scopeParameters;

    private string query;
    private string fastQuery;
    private string database;

    private IDeleteItemsUrlBuilder<IDeleteItemsByQueryRequest> builder;

    [SetUp]
    public void Setup()
    {
      this.sessionConfig = new MutableSessionConfig("http://testurl", null, null);
      this.scopeParameters = new ScopeParameters();

      this.query = "/Sitecore/Content/*";
      this.fastQuery = "fast:/sitecore/content/Home/Products/*[@@name = 'Hammer']";
      this.database = "master";

      this.builder = new DeleteItemByQueryUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionConfig = null;
      this.scopeParameters = null;

      this.query = null;
      this.fastQuery = null;
      this.database = null;

      this.builder = null;
    }

    [Test]
    public void TestNullRequest()
    {
      TestDelegate action = () => this.builder.GetUrlForRequest(null);

      Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void TestNullSessionInParams()
    {
      TestDelegate action = () =>
      {
        var parameters = new DeleteItemByQueryParameters(null, this.scopeParameters, this.database, this.query);

        this.builder.GetUrlForRequest(parameters);
      };

      Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void TestNullQuery()
    {
      TestDelegate action = () =>
      {
        var parameters = new DeleteItemByQueryParameters(this.sessionConfig, this.scopeParameters, this.database, null);

        this.builder.GetUrlForRequest(parameters);
      };

      Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void TestCorrectQuery()
    {
      var parameters = new DeleteItemByQueryParameters(this.sessionConfig, this.scopeParameters, null, this.query);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1?query=%2fSitecore%2fContent%2f%2a", url);
    }

    [Test]
    public void TestCorrectQueryWithDatabase()
    {
      var parameters = new DeleteItemByQueryParameters(this.sessionConfig, this.scopeParameters, this.database, this.query);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1?query=%2fSitecore%2fContent%2f%2a&sc_database=master", url);
    }

    [Test]
    public void TestCorrectQueryWithDatabaseAndScope()
    {
      scopeParameters.AddScope(ScopeType.Children);

      var parameters = new DeleteItemByQueryParameters(this.sessionConfig, this.scopeParameters, this.database, this.query);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1?query=%2fSitecore%2fContent%2f%2a&scope=c&sc_database=master", url);
    }

    [Test]
    public void TestCorrectQueryWithScope()
    {
      scopeParameters.AddScope(ScopeType.Children);

      var parameters = new DeleteItemByQueryParameters(this.sessionConfig, this.scopeParameters, null, this.query);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1?query=%2fSitecore%2fContent%2f%2a&scope=c", url);
    }

    [Test]
    public void TestCorrectFastQuery()
    {
      scopeParameters.AddScope(ScopeType.Children);

      var parameters = new DeleteItemByQueryParameters(this.sessionConfig, this.scopeParameters, null, this.fastQuery);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1?query=fast%3a%2fsitecore%2fcontent%2fHome%2fProducts%2f%2a%5b%40%40name%20%3d%20%27Hammer%27%5d&scope=c", url);
    }
  }
}

