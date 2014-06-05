using MobileSDKUnitTest.Mock;

namespace MobileSdk_IntegrationTest_Desktop
{
  using System;
  using System.Threading.Tasks;
  using MobileSDKIntegrationTest;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;

  [TestFixture]
  public class SetDefaultSessionSettingsTest
  {
    private TestEnvironment testData;
    private SessionConfig sessionConfig;
    IReadItemsByIdRequest requestWithItemId;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      sessionConfig = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      var requestBuilder = new ItemWebApiRequestBuilder();
      requestWithItemId = requestBuilder.RequestWithId(testData.Items.ItemWithVersions.Id).Build();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.sessionConfig = null;
    }

    [Test]
    public async void TestGetItemWithDbLanguageAndVersion()
    {
      const string Db = "web";
      const string Language = "da";
      const string Version = "1";
      var itemSource = new ItemSource(Db, Language, Version);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      //Assert.AreEqual(version, resultItem.Version);
      //Assert.AreEqual("Danish version 1 master", resultItem.Fields["Title"]);
    }
    [Test]
    public async void TestGetItemWithNotExistentLanguage()
    {
      const string Language = "da";
      var itemSource = new ItemSource("web", Language);
      var session = new ScApiSession(this.sessionConfig, itemSource);
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithId(testData.Items.Home.Id).Build();
      var response = await session.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      //Assert.AreEqual("en", resultItem.Language);

    }

    private async Task<ScItemsResponse> GetItemByIdWithItemSource(ItemSource itemSource)
    {
      var session = new ScApiSession(this.sessionConfig, itemSource);
      var response = await session.ReadItemByIdAsync(this.requestWithItemId);
      return response;
    }
  }
}
