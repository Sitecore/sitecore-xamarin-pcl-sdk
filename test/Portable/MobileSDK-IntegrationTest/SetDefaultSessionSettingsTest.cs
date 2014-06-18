namespace MobileSDKIntegrationTest
{
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;

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
      requestWithItemId = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).Build();
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
      ISitecoreItem resultItem = response.Items[0];
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
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).Database(Db).Language(Language).Version(Version).Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var source = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(source, resultItem.Source);
      //Assert.AreEqual("Danish version 1 master", resultItem.Fields["Title"].RawValue);
    }
    [Test]
    public async void TestOverrideLanguageInRequestById()
    {
      const string Db = "master";
      const string Language = "en";
      const string Version = "2";
      var source = new ItemSource(Db, "da", Version);
      var session = new ScApiSession(this.sessionConfig, source);
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).Language(Language).Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var sourceExpected = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(sourceExpected, resultItem.Source);
      //Assert.AreEqual("English version 2 master", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestOverrideVersionAndDbInRequestById()
    {
      const string Db = "master";
      const string Language = "en";
      const string Version = "2";
      var source = new ItemSource("web", Language, "1");
      var session = new ScApiSession(this.sessionConfig, source);
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).Version(Version).Database(Db).Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var sourceExpected = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(sourceExpected, resultItem.Source);
      //Assert.AreEqual("English version 2 master", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestOverrideDatabaseInRequestByPath()
    {
      const string Db = "master";
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.ItemWithVersions.Path).Database(Db).Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];

      var expectedSource = new ItemSource(Db, ItemSource.DefaultSource().Language,"2");
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      testData.AssertItemSourcesAreEqual(expectedSource,resultItem.Source);
    }

    [Test]
    public async void TestOverrideDatabaseInRequestByPathSeveralTimes()
    {
      const string Db = "web";
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.ItemWithVersions.Path).Database("master").Database(null).Database(Db).Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];

      var expectedSource = new ItemSource(Db, ItemSource.DefaultSource().Language, "1");
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
    }

    [Test]
    public async void TestGetItemInRequestByQueryAsConcatenationString()
    {
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Path+"/*").Database("master").Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(4, response);
    }

    [Test]
    public async void TestOverrideDbLanguageAndVersionWithEmptyValuesInRequestById()
    {
      const string Db = "";
      const string Language = "";
      const string Version = "";
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id).Database(Db).Language(Language).Version(Version).Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      var expectedSource = new ItemSource(ItemSource.DefaultSource().Database, ItemSource.DefaultSource().Language, "1");
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
    }

    [Test]
    public async void TestOverrideLanguageAndVersionInRequestByQuery()
    {
      const string Language = "da";
      const string Version = "1";
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home/*").Version(Version).Language(Language).Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(4, response);
      ISitecoreItem resultItem = response.Items[3];
      var expectedSource = new ItemSource(ItemSource.DefaultSource().Database, Language, Version);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
    }

    [Test]
    public async void TestItemByQueryWithSpecifiedFieldCorrectValue()
    {
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore//*[@Title='English version 2 web']").Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      var expectedSource = new ItemSource(ItemSource.DefaultSource().Database, ItemSource.DefaultSource().Language, "1");
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
    }

    [Test]
    public async void TestItemByQueryWithSpecifiedFieldNotCorrectValue()
    {
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content//*[@Title='Danish version 2 web']").Language("da").Database("master").Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(0, response); 
    }

    private async Task<ScItemsResponse> GetItemByIdWithItemSource(ItemSource itemSource)
    {
      var session = new ScApiSession(this.sessionConfig, itemSource);
      var response = await session.ReadItemAsync(this.requestWithItemId);
      return response;
    }
  }
}
