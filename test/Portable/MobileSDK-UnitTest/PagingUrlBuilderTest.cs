namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.MockObjects;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;

  [TestFixture]
  public class PagingUrlBuilderTest
  {
    private ItemByIdUrlBuilder builderForId;
    private ItemByPathUrlBuilder builderForPath;

    private ISessionConfig sessionConfig;
    private ISessionConfig sitecoreShellConfig;
    private IItemSource defaultSource;


    [SetUp]
    public void SetUp()
    {
      IRestServiceGrammar restGrammar = RestServiceGrammar.ItemSSCV2Grammar();
      ISSCUrlParameters webApiGrammar = SSCUrlParameters.ItemSSCV2UrlParameters();

      this.builderForId = new ItemByIdUrlBuilder(restGrammar, webApiGrammar);
      this.builderForPath = new ItemByPathUrlBuilder(restGrammar, webApiGrammar);

      this.defaultSource = new ItemSourcePOD(null, null, null);

      SessionConfigPOD mutableSessionConfig = new SessionConfigPOD();
      mutableSessionConfig.ItemSSCVersion = "v1";
      mutableSessionConfig.InstanceUrl = "tumba.yumba";
      mutableSessionConfig.Site = null;
      this.sessionConfig = mutableSessionConfig;


      mutableSessionConfig = new SessionConfigPOD();
      mutableSessionConfig.ItemSSCVersion = "v234";
      mutableSessionConfig.InstanceUrl = "trololo.net";
      mutableSessionConfig.Site = "/sitecore/shell";
      this.sitecoreShellConfig = mutableSessionConfig;
    }

    [TearDown]
    public void TearDown()
    {
      this.builderForId = null;
      this.builderForPath = null;

      this.sessionConfig = null;
      this.sitecoreShellConfig = null;
      this.defaultSource = null;
    }

    #region By Id
    [Test]
    public void TestValidRequestWithId()
    {
      IPagingParameters paging = new MutablePagingParameters(3, 5);

      var request = new ReadItemsByIdParameters(this.sessionConfig, this.defaultSource, null, paging, true, "{item-id}");

      string result = this.builderForId.GetUrlForRequest(request);
      string expected = "http://tumba.yumba/-/item/v1?sc_itemid=%7bitem-id%7d&page=3&pageSize=5";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestValidRequestWithIdForShellSite()
    {
      IPagingParameters paging = new MutablePagingParameters(1, 10);
      var request = new ReadItemsByIdParameters(this.sitecoreShellConfig, this.defaultSource, null, paging, true, "{item-id}");

      string result = this.builderForId.GetUrlForRequest(request);
      string expected = "http://trololo.net/-/item/v234%2fsitecore%2fshell?sc_itemid=%7bitem-id%7d&page=1&pageSize=10";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestPagingCanBeOmittedForId()
    {
      IPagingParameters paging = null;
      var request = new ReadItemsByIdParameters(this.sessionConfig, this.defaultSource, null, paging, true, "{item-id}");

      string result = this.builderForId.GetUrlForRequest(request);
      string expected = "http://tumba.yumba/-/item/v1?sc_itemid=%7bitem-id%7d";

      Assert.AreEqual(expected, result);
    }
    #endregion By Id

    #region By Path
    [Test]
    public void TestValidRequestWithPath()
    {
      IPagingParameters paging = new MutablePagingParameters(3, 5);
      var request = new ReadItemByPathParameters(this.sessionConfig, this.defaultSource, null, paging, "/sitecore/content");

      string result = this.builderForPath.GetUrlForRequest(request);
      string expected = "http://tumba.yumba/-/item/v1%2fsitecore%2fcontent?page=3&pageSize=5";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestValidRequestWithPathForShellSite()
    {
      IPagingParameters paging = new MutablePagingParameters(1, 10);
      var request = new ReadItemByPathParameters(this.sitecoreShellConfig, this.defaultSource, null, paging, "/x/y/z");

      string result = this.builderForPath.GetUrlForRequest(request);
      string expected = "http://trololo.net/-/item/v234%2fsitecore%2fshell%2fx%2fy%2fz?page=1&pageSize=10";

      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestPagingCanBeOmittedForPath()
    {
      IPagingParameters paging = null;
      var request = new ReadItemByPathParameters(this.sessionConfig, this.defaultSource, null, paging, "/root");

      string result = this.builderForPath.GetUrlForRequest(request);
      string expected = "http://tumba.yumba/-/item/v1%2froot";

      Assert.AreEqual(expected, result);
    }
    #endregion By Path

    #region Validation
    [Test]
    public void TestNegativePageNumberIsNotAllowed()
    {
      IPagingParameters paging = new MutablePagingParameters(-1, 5);
      var request = new ReadItemByPathParameters(this.sessionConfig, this.defaultSource, null, paging, "/sitecore/content");

      Assert.Throws<ArgumentException>(() => this.builderForPath.GetUrlForRequest(request));
    }

    #endregion Validation
  }
}

