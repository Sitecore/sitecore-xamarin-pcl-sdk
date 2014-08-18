namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.Items;


  [TestFixture]
  public class SetDefaultSessionSettingsTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiReadonlySession sessionAuthenticatedUser;
    IReadItemsByIdRequest requestWithItemId;

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();

      this.sessionAuthenticatedUser =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Admin)
          .DefaultDatabase("web")
          .DefaultLanguage("en")
          .BuildReadonlySession();

      this.requestWithItemId = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Payload(PayloadType.Content)
        .Build();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.sessionAuthenticatedUser.Dispose();
      this.sessionAuthenticatedUser = null;
    }

    [Test]
    public async void TestGetItemWithDbLanguageAndVersionFromSession()
    {
      const string Db = "web";
      const string Language = "da";
      const int Version = 1;
      var itemSource = new ItemSource(Db, Language, Version);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      var resultItem = response[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var source = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(source, resultItem.Source);
      Assert.AreEqual("Danish version 2 web", resultItem["Title"].RawValue);
      Assert.AreEqual(Db, resultItem.Source.Database);
      Assert.AreEqual(Version, resultItem.Source.VersionNumber.Value);
      Assert.AreEqual(Language, resultItem.Source.Language);
    }

    [Test]
    public async void TestGetItemWithDefaultDbLanguageAndVersion()
    {
      var response = await this.GetItemByIdWithItemSource(LegacyConstants.DefaultSource());
      const string Db = "web";
      const string Language = "en";
      const int Version = 2;
      testData.AssertItemsCount(1, response);
      var resultItem = response[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var source = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(source, resultItem.Source);
      Assert.AreEqual("English version 2 web", resultItem["Title"].RawValue);
    }
    [Test]
    public async void TestGetItemWithDbLanguageAndVersionFromRequest()
    {
      const string Db = "master";
      const string Language = "da";
      const int Version = 1;

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Database(Db)
        .Language(Language)
        .Version(Version)
        .Payload(PayloadType.Content)
        .Build();
      var response = await sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var source = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(source, resultItem.Source);
      Assert.AreEqual("Danish version 1 master", resultItem["Title"].RawValue);
    }
    [Test]
    public async void TestOverrideLanguageInRequestById()
    {
      const string Db = "master";
      const string Language = "en";
      const int Version = 2;

      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .DefaultDatabase(Db)
        .DefaultLanguage("da")
        .BuildReadonlySession();


      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Language(Language)
        .Version(Version)
        .Payload(PayloadType.Content)
        .Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var sourceExpected = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(sourceExpected, resultItem.Source);
      Assert.AreEqual("English version 2 master", resultItem["Title"].RawValue);
    }

    [Test]
    public async void TestOverrideVersionAndDbInRequestById()
    {
      const string Db = "master";
      const string Language = "en";
      const int Version = 2;
      var source = new ItemSource("web", Language, 1);

      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .DefaultDatabase(source.Database)
        .DefaultLanguage(source.Language)
        .BuildReadonlySession();

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Version(Version)
        .Database(Db)
        .Payload(PayloadType.Content)
        .Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      var sourceExpected = new ItemSource(Db, Language, Version);
      testData.AssertItemSourcesAreEqual(sourceExpected, resultItem.Source);
      Assert.AreEqual("English version 2 master", resultItem["Title"].RawValue);
    }

    [Test]
    public async void TestOverrideDatabaseInRequestByPath()
    {
      const string Db = "master";

      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.ItemWithVersions.Path);
      var request = requestBuilder.Database(Db).Build();
      var response = await sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response[0];

      var expectedSource = new ItemSource(Db, LegacyConstants.DefaultSource().Language, 2);
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
    }

    [Test]
    public void TestOverrideDatabaseInRequestByPathSeveralTimesReturnsError()
    {
      const string Db = "web";

      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path)
       .Database("master")
       .Database(Db)
       .Payload(PayloadType.Content).Build());
      Assert.AreEqual("ReadItemByPathRequestBuilder.Database : Property cannot be assigned twice.", exception.Message);
    }

    [Test]
    public void TestGetItemByIdWithNullDatabaseDoNotReturnsError()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id)
                            .Database(null)
                            .Payload(PayloadType.Content)
                            .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestGetItemByPathWithEmptyDatabaseReturnsError()
    {

      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path);
      Exception exception = Assert.Throws<ArgumentException>(() => requestBuilder.Database(" ").Payload(PayloadType.Content).Build());
      Assert.AreEqual("ReadItemByPathRequestBuilder.Database : The input cannot be empty.", exception.Message);
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
    public void TestOverrideLanguageWithEmptyValueInRequestByIdDoNotReturnsError()
    {
      const string Language = "";

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
                                            .Language(Language)
                                            .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestOverrideVersionWithZeroValueInRequestByPathReturnsError()
    {
      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path);
      Exception exception = Assert.Throws<ArgumentException>(() => requestBuilder.Version(0).Build());
      Assert.AreEqual("ReadItemByPathRequestBuilder.Version : Positive number expected", exception.Message);
    }

    [Test]
    public void TestOverrideVersionWithNegativeValueInRequestByPathReturnsError()
    {
      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path);
      Exception exception = Assert.Throws<ArgumentException>(() => requestBuilder.Version(-1).Build());
      Assert.AreEqual("ReadItemByPathRequestBuilder.Version : Positive number expected", exception.Message);
    }

    [Test]
    public async void TestOverrideLanguageInRequestByQuery()
    {
      const string Language = "da";

      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home/*");
      var request = requestBuilder
        .Language(Language)
        .Build();
      var response = await sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(4, response);
      var resultItem = response[3];
      var expectedSource = new ItemSource(LegacyConstants.DefaultSource().Database, Language, 1);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
    }

    [Test]
    public async void TestGetItemByQueryWithCorrectField()
    {
      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore//*[@Title='English version 2 web']");
      var request = requestBuilder.Payload(PayloadType.Content).Build();
      var response = await sessionAuthenticatedUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response[0];
      var expectedSource = new ItemSource(LegacyConstants.DefaultSource().Database, LegacyConstants.DefaultSource().Language, 2);
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
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .DefaultLanguage(itemSource.Language)
        .DefaultDatabase(itemSource.Database)
        .BuildReadonlySession();

      IReadItemsByIdRequest request = null;

      if (null == itemSource.VersionNumber)
      {
        request = this.requestWithItemId;
      }
      else
      {
        request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Payload(PayloadType.Content)
        .Version(itemSource.VersionNumber.Value)
        .Build();
      }

      var response = await session.ReadItemAsync(request);
      return response;
    }
  }
}
