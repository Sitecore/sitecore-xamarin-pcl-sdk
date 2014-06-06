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
    public async void TestGetItemWithDbLanguageAndVersionFromSession()
    {
      const string Db = "web";
      const string Language = "da";
      const string Version = "1";
      var itemSource = new ItemSource(Db, Language, Version);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      Assert.AreEqual(Version, resultItem.Source.Version);
      Assert.AreEqual(Language, resultItem.Source.Language);
      Assert.AreEqual(Db, resultItem.Source.Database);
      //Assert.AreEqual("Danish version 2 web", resultItem.Fields["Title"].RawValue);
    }
    [Test]
    public async void TestGetItemWithDefaultDbLanguageAndVersion()
    {
      var response = await this.GetItemByIdWithItemSource(ItemSource.DefaultSource());
      const string Db = "web";
      const string Language = "en";
      const string Version = "1";
      testData.AssertItemsCount(1, response);
      var resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var source = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(source, resultItem.Source);
      //Assert.AreEqual("English version 2 web", resultItem.Fields["Title"].RawValue);
    }
    [Test]
    public async void TestGetItemWithDbLanguageAndVersionFromRequest()
    {
      const string Db = "master";
      const string Language = "da";
      const string Version = "1";

      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithId(testData.Items.ItemWithVersions.Id).Database(Db).Language(Language).Version(Version).Build();
      var response = await session.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var source = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(source, resultItem.Source);
      //Assert.AreEqual("Danish version 1 master", resultItem.Fields["Title"].RawValue);
    }
    [Test]
    public async void TestGetItemWithOverridenLanguageFromRequest()
    {
      const string Db = "master";
      const string Language = "en";
      const string Version = "2";
      var source = new ItemSource(Db, "da", Version);
      var session = new ScApiSession(this.sessionConfig, source);
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithId(testData.Items.ItemWithVersions.Id).Language(Language).Build();
      var response = await session.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      ScItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var sourceExpected = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(sourceExpected, resultItem.Source);
      //Assert.AreEqual("English version 2 master", resultItem.Fields["Title"].RawValue);
    }

    private async Task<ScItemsResponse> GetItemByIdWithItemSource(ItemSource itemSource)
    {
      var session = new ScApiSession(this.sessionConfig, itemSource);
      var response = await session.ReadItemByIdAsync(this.requestWithItemId);
      return response;
    }



  }
}
