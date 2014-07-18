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
    private IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
    private IWebApiUrlParameters webApiUrlParameters = WebApiUrlParameters.ItemWebApiV2UrlParameters();

    private SessionConfig sessionConfig = new MutableSessionConfig("http://testurl", null, null);
    private ScopeParameters scopeParameters = new ScopeParameters();

    private string id = "{B0ED4777-1F5D-478D-AF47-145CCA9E4311}";
    private string database = "master";

    [Test]
    public void TestNullRequest()
    {
      TestDelegate action = () => new DeleteItemsByIdUrlBuilder(this.restGrammar, this.webApiUrlParameters)
        .GetUrlForRequest(null);

      Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void TestNullSessionInParams()
    {
      TestDelegate action = () =>
      {
        var parameters = new DeleteItemByIdParameters(null, this.scopeParameters, this.id, this.database);

        new DeleteItemsByIdUrlBuilder(this.restGrammar, this.webApiUrlParameters)
        .GetUrlForRequest(parameters);
      };

      Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void TestNullIdInParams()
    {
      TestDelegate action = () =>
      {
        var parameters = new DeleteItemByIdParameters(this.sessionConfig, this.scopeParameters, null, this.database);

        new DeleteItemsByIdUrlBuilder(this.restGrammar, this.webApiUrlParameters)
        .GetUrlForRequest(parameters);
      };

      Assert.Throws<ArgumentException>(action);
    }

    [Test]
    public void TestCorrectParams()
    {
      var parameters = new DeleteItemByIdParameters(this.sessionConfig, this.scopeParameters, null, this.id);

      var url = new DeleteItemsByIdUrlBuilder(this.restGrammar, this.webApiUrlParameters)
      .GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1?sc_itemid=%7BB0ED4777-1F5D-478D-AF47-145CCA9E4311%7D", url);
    }
  }
}

