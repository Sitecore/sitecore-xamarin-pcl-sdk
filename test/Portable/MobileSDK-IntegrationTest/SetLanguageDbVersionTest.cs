using MobileSDKUnitTest.Mock;

namespace MobileSDKIntegrationTest
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
  public class SetLanguageDbVersionTest
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
    public async void TestGetItemWithNotExistentLanguage()
    {
      const string Language = "da";
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithId(testData.Items.Home.Id).Language(Language).Build();
      var response = await session.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      //Assert.AreEqual("", resultItem.Fields["Title"].RawValue);

    }

    private async Task<ScItemsResponse> GetItemByIdWithItemSource(ItemSource itemSource)
    {
      var session = new ScApiSession(this.sessionConfig, itemSource);
      var response = await session.ReadItemByIdAsync(this.requestWithItemId);
      return response;
    }
  }
}
