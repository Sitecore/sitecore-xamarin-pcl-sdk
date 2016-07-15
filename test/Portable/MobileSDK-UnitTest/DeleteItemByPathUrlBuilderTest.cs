namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items.Delete;
  using Sitecore.MobileSDK.MockObjects;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;

  [TestFixture]
  public class DeleteItemByPathUrlBuilderTest
  {
    private SessionConfig sessionConfig;

    private string path;
    private string database;

    private IDeleteItemsUrlBuilder<IDeleteItemsByPathRequest> builder;

    [SetUp]
    public void Setup()
    {
      this.sessionConfig = new MutableSessionConfig("http://testurl");

      this.path = "/sitecore/content/Home/Android/Folder for deleting/1";
      this.database = "master";

      this.builder = new DeleteItemByPathUrlBuilder(RestServiceGrammar.ItemSSCV2Grammar(),SSCUrlParameters.ItemSSCV2UrlParameters());
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionConfig = null;

      this.path = null;
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
    public void TestNullSession()
    {
      TestDelegate action = () =>
      {
        var parameters = new DeleteItemByPathParameters(null, this.path, this.database);

        this.builder.GetUrlForRequest(parameters);
      };

      Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void TestNullPath()
    {
      TestDelegate action = () =>
      {
        var parameters = new DeleteItemByPathParameters(this.sessionConfig, this.database, null);

        this.builder.GetUrlForRequest(parameters);
      };

      Assert.Throws<ArgumentNullException>(action);
    }

    [Test]
    public void TestCorrectPath()
    {
      var parameters = new DeleteItemByPathParameters(this.sessionConfig, null, this.path);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1%2fsitecore%2fcontent%2fhome%2fandroid%2ffolder%20for%20deleting%2f1", url);
    }

    [Test]
    public void TestCorrectPathWithDatabase()
    {
      var parameters = new DeleteItemByPathParameters(this.sessionConfig, this.database, this.path);

      var url = this.builder.GetUrlForRequest(parameters);

      Assert.AreEqual("http://testurl/-/item/v1%2fsitecore%2fcontent%2fhome%2fandroid%2ffolder%20for%20deleting%2f1?sc_database=master", url);
    }
      
  }
}

