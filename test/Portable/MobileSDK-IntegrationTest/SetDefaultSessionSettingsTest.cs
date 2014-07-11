namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  [TestFixture]
  public class SetDefaultSessionSettingsTest
  {
    private TestEnvironment testData;
    private ScApiSession sessionAuthenticatedUser;
    private SessionConfig sessionConfig;
    IReadItemsByIdRequest requestWithItemId;

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();

      this.sessionConfig = new SessionConfig(testData.InstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      this.sessionAuthenticatedUser = new ScApiSession(sessionConfig, ItemSource.DefaultSource());
      this.requestWithItemId = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Payload(PayloadType.Content)
        .Build();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.sessionConfig = null;
      this.sessionAuthenticatedUser = null;
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
      var resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var source = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(source, resultItem.Source);
      Assert.AreEqual("Danish version 2 web", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithDefaultDbLanguageAndVersion()
    {
      var response = await this.GetItemByIdWithItemSource(ItemSource.DefaultSource());
      const string Db = "web";
      const string Language = "en";
      const string Version = "2";
      testData.AssertItemsCount(1, response);
      var resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var source = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(source, resultItem.Source);
      Assert.AreEqual("English version 2 web", resultItem.FieldWithName("Title").RawValue);
    }
    [Test]
    public async void TestGetItemWithDbLanguageAndVersionFromRequest()
    {
      const string Db = "master";
      const string Language = "da";
      const string Version = "1";

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Database(Db)
        .Language(Language)
        .Version(Version)
        .Payload(PayloadType.Content)
        .Build();
      var response = await sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var source = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(source, resultItem.Source);
      Assert.AreEqual("Danish version 1 master", resultItem.FieldWithName("Title").RawValue);
    }
    [Test]
    public async void TestOverrideLanguageInRequestById()
    {
      const string Db = "master";
      const string Language = "en";
      const string Version = "2";
      var source = new ItemSource(Db, "da", Version);

      var session = new ScApiSession(sessionConfig, source);
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Language(Language)
        .Payload(PayloadType.Content)
        .Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var sourceExpected = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(sourceExpected, resultItem.Source);
      Assert.AreEqual("English version 2 master", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestOverrideVersionAndDbInRequestById()
    {
      const string Db = "master";
      const string Language = "en";
      const string Version = "2";
      var source = new ItemSource("web", Language, "1");
      var session = new ScApiSession(this.sessionConfig, source);
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Version(Version)
        .Database(Db)
        .Payload(PayloadType.Content)
        .Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var sourceExpected = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(sourceExpected, resultItem.Source);
      Assert.AreEqual("English version 2 master", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestOverrideDatabaseInRequestByPath()
    {
      const string Db = "master";

      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.ItemWithVersions.Path);
      var request = requestBuilder.Database(Db).Build();
      var response = await sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response.Items[0];

      var expectedSource = new ItemSource(Db, ItemSource.DefaultSource().Language, "2");
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
    }

    [Test]
    public async void TestOverrideDatabaseInRequestByPathSeveralTimes()
    {
      const string Db = "web";

      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path)
       .Database("master")
       .Database(Db)
       .Payload(PayloadType.Content).Build());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Database : The database cannot be assigned twice", exception.Message);
    }

    [Test]
    public void TestGetItemByIdWithNullDatabase()
    {

      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id);
      Exception exception = Assert.Throws<ArgumentException>(() => requestBuilder.Database(null).Payload(PayloadType.Content).Build());
      Assert.AreEqual("System.ArgumentException", exception.GetType().ToString());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Database : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestGetItemByPathWithEmptyDatabase()
    {

      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path);
      Exception exception = Assert.Throws<ArgumentException>(() => requestBuilder.Database(" ").Payload(PayloadType.Content).Build());
      Assert.AreEqual("System.ArgumentException", exception.GetType().ToString());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Database : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public async void TestGetItemInRequestByQueryAsConcatenationString()
    {
      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Path + "/*");
      var request = requestBuilder.Database("master").Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);

      this.testData.AssertItemsCount(4, response);
    }

    [Test]
    public async void TestOverrideLanguageWithEmptyValueInRequestById()
    {
      const string Language = "";

      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id);
      Exception exception = Assert.Throws<ArgumentException>(() => requestBuilder.Language(Language).Build());
      Assert.AreEqual("System.ArgumentException", exception.GetType().ToString());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Language : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public async void TestOverrideVersionWithEmptyValueInRequestByPath()
    {
      const string Version = "";

      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path);
      Exception exception = Assert.Throws<ArgumentException>(() => requestBuilder.Version(Version).Build());
      Assert.AreEqual("System.ArgumentException", exception.GetType().ToString());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Version : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public async void TestOverrideLanguageAndVersionInRequestByQuery()
    {
      const string Language = "da";
      const string Version = "1";

      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home/*");
      var request = requestBuilder
        // @adk : does not compile by design
        //        .Version(Version)
        .Language(Language)
        .Build();
      var response = await sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(4, response);
      var resultItem = response.Items[3];
      var expectedSource = new ItemSource(ItemSource.DefaultSource().Database, Language, Version);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
    }

    [Test]
    public async void TestGetItemByQueryWithCorrectField()
    {
      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore//*[@Title='English version 2 web']");
      var request = requestBuilder.Payload(PayloadType.Content).Build();
      var response = await sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response.Items[0];
      var expectedSource = new ItemSource(ItemSource.DefaultSource().Database, ItemSource.DefaultSource().Language, "2");
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
    }

    [Test]
    public async void TestGetItemByQueryWithNotCorrectField()
    {
      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content//*[@Title='DANISH version 2 web']");
      var request = requestBuilder.Language("da").Database("master").Build();
      var response = await sessionAuthenticatedUser.ReadItemAsync(request);

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
