namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using SitecoreMobileSDKMockObjects;

  [TestFixture]
  public class DeleteItemByIdUrlBuilderTest
  {
    private SessionConfig sessionConfig;
    private ScopeParameters scopeParameters;

    private string id;
    private string database;

    private DeleteItemsByIdUrlBuilder builder;

    [SetUp]
    public void Setup()
    {
      this.sessionConfig = new MutableSessionConfig("http://testurl", null, null);
      this.scopeParameters = new ScopeParameters();

      this.id = "{B0ED4777-1F5D-478D-AF47-145CCA9E4311}";
      this.database = "master";

      this.builder = new DeleteItemsByIdUrlBuilder(RestServiceGrammar.ItemWebApiV2Grammar(), WebApiUrlParameters.ItemWebApiV2UrlParameters());
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
        var parameters = new DeleteItemByIdParameters(null, this.scopeParameters, this.id, this.database);

        this.builder.GetUrlForRequest(parameters);
      };

      Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void TestNullId()
    {
      TestDelegate action = () =>
      {
        var parameters = new DeleteItemByIdParameters(this.sessionConfig, this.scopeParameters, null, this.database);

        this.builder.GetUrlForRequest(parameters);
      };

      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestCorrectId()
    {
      var parameters = new DeleteItemByIdParameters(this.sessionConfig, this.scopeParameters, null, this.id);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1?sc_itemid=%7BB0ED4777-1F5D-478D-AF47-145CCA9E4311%7D", url);
    }

    [Test]
    public void TestCorrectIdWithDatabase()
    {
      var parameters = new DeleteItemByIdParameters(this.sessionConfig, this.scopeParameters, this.database, this.id);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1?sc_itemid=%7BB0ED4777-1F5D-478D-AF47-145CCA9E4311%7D&sc_database=master", url);
    }

    [Test]
    public void TestCorrectIdWithDatabaseAndScope()
    {
      scopeParameters.AddScope(ScopeType.Children);

      var parameters = new DeleteItemByIdParameters(this.sessionConfig, this.scopeParameters, this.database, this.id);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1?sc_itemid=%7BB0ED4777-1F5D-478D-AF47-145CCA9E4311%7D&scope=c&sc_database=master", url);
    }

    [Test]
    public void TestCorrectIdWithScope()
    {
      scopeParameters.AddScope(ScopeType.Children);

      var parameters = new DeleteItemByIdParameters(this.sessionConfig, this.scopeParameters, null, this.id);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1?sc_itemid=%7BB0ED4777-1F5D-478D-AF47-145CCA9E4311%7D&scope=c", url);
    }
  }
}

