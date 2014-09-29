using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK.API.Request.Parameters;
using SitecoreMobileSDKMockObjects;
using Sitecore.MobileSDK.API.Items;

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.API;


  [TestFixture]
  public class PagingUrlBuilderTest
  {
    private ItemByIdUrlBuilder builderForId;
    private ISessionConfig sessionConfig;
    private ISessionConfig sitecoreShellConfig;
    private IItemSource defaultSource;


    [SetUp]
    public void SetUp()
    {
      IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
      IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

      this.builderForId = new ItemByIdUrlBuilder(restGrammar, webApiGrammar);
      this.defaultSource = new ItemSourcePOD(null, null, null);

      SessionConfigPOD mutableSessionConfig = new SessionConfigPOD();
      mutableSessionConfig.ItemWebApiVersion = "v1";
      mutableSessionConfig.InstanceUrl = "tumba.yumba";
      mutableSessionConfig.Site = null;
      this.sessionConfig = mutableSessionConfig;


      mutableSessionConfig = new SessionConfigPOD();
      mutableSessionConfig.ItemWebApiVersion = "v234";
      mutableSessionConfig.InstanceUrl = "trololo.net";
      mutableSessionConfig.Site = "/sitecore/shell";
      this.sitecoreShellConfig = mutableSessionConfig;
    }


    [Test]
    public void TestValidRequestWithId()
    {
      IPagingParameters paging = new MutablePagingParameters(3, 5);
      var request = new ReadItemsByIdParameters(this.sessionConfig, this.defaultSource, null, paging, "{item-id}");

      string result = this.builderForId.GetUrlForRequest(request);
      string expected = "http://tumba.yumba/-/item/v1?sc_itemid=%7bitem-id%7d&page=3&pageSize=5";

      Assert.AreEqual(expected, result);
    }
  }
}

