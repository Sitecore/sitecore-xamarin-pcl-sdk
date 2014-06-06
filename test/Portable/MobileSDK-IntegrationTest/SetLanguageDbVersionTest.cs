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


    [Test]
    public async void TestGetItemWithMasterDbLanguage()
    {
      const string Db = "master";
      const string Language = "da";
      var itemSource = new ItemSource(Db, Language);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      //Assert.AreEqual(version, resultItem.Version);
      //Assert.AreEqual("Danish version 2 master", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithCoreDbLanguageAndVersion()
    {
      const string Db = "CoRE";
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithId(testData.Items.Home.Id).Database(Db).Build();
      var response = await session.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      var expectedItem = new TestEnvironment.Item();
      expectedItem.DisplayName = testData.Items.Home.DisplayName;
      expectedItem.Id = testData.Items.Home.Id;
      expectedItem.Path = testData.Items.Home.Path;
      expectedItem.Template = "Sitecore Client/Home";
      testData.AssertItemsAreEqual(expectedItem, resultItem);
    }

    [Test]
    public async void TestGetItemWithDefaultDbNotExistedLanguageAndVersion()
    {
      const string Db = "master";
      const string Language = "language";
      const string Version = "2";

      var itemSource = new ItemSource(Db, Language, Version);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      //Assert.AreEqual("English version 2 master", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithDefaultDbLanguageAndNotExistedVersion()
    {
      const string Db = "web";
      const string Language = "da";
      const string Version = "12";

      var itemSource = new ItemSource(Db, Language, Version);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      //Assert.AreEqual("Danish version 2 web", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithDefaultDbNotExistedLanguageAndNotExistedVersion()
    {
      const string Db = "web";
      const string Language = "UKRAINIAN";
      const string Version = "12";

      var itemSource = new ItemSource(Db, Language, Version);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      //Assert.AreEqual("English version 2 web", resultItem.Fields["Title"].RawValue);
    }

  }
}
