namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.MockObjects;

  [TestFixture]
  public class SetDefaultSessionSettingsTest
  {
    private TestEnvironment testData;
    private ISitecoreSSCReadonlySession sessionAuthenticatedUser;
    IReadItemsByIdRequest requestWithItemId;

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();

      this.sessionAuthenticatedUser =
        SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Admin)
          .DefaultDatabase("web")
          .DefaultLanguage("en")
          .BuildReadonlySession();

      this.requestWithItemId = ItemSSCRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
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

      var request = ItemSSCRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Database(Db)
        .Language(Language)
        .Version(Version)
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

      var session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .DefaultDatabase(Db)
        .DefaultLanguage("da")
        .BuildReadonlySession();


      var request = ItemSSCRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Language(Language)
        .Version(Version)
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

      var session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .DefaultDatabase(source.Database)
        .DefaultLanguage(source.Language)
        .BuildReadonlySession();

      var request = ItemSSCRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Version(Version)
        .Database(Db)
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

      var requestBuilder = ItemSSCRequestBuilder.ReadItemsRequestWithPath(testData.Items.ItemWithVersions.Path);
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

      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path)
       .Database("master")
       .Database(Db)
       .Build());
      Assert.AreEqual("ReadItemByPathRequestBuilder.Database : Property cannot be assigned twice.", exception.Message);
    }

    [Test]
    public void TestGetItemByIdWithNullDatabaseDoNotReturnsError()
    {
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id)
                            .Database(null)
                            .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestGetItemByPathWithEmptyDatabaseReturnsError()
    {

      var requestBuilder = ItemSSCRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path);
      Exception exception = Assert.Throws<ArgumentException>(() => requestBuilder.Database(" ").Build());
      Assert.AreEqual("ReadItemByPathRequestBuilder.Database : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestOverrideLanguageWithEmptyValueInRequestByIdDoNotReturnsError()
    {
      const string Language = "";

      var request = ItemSSCRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
                                            .Language(Language)
                                            .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestOverrideVersionWithZeroValueInRequestByPathReturnsError()
    {
      var requestBuilder = ItemSSCRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path);
      Exception exception = Assert.Throws<ArgumentException>(() => requestBuilder.Version(0).Build());
      Assert.AreEqual("ReadItemByPathRequestBuilder.Version : Positive number expected", exception.Message);
    }

    [Test]
    public void TestOverrideVersionWithNegativeValueInRequestByPathReturnsError()
    {
      var requestBuilder = ItemSSCRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path);
      Exception exception = Assert.Throws<ArgumentException>(() => requestBuilder.Version(-1).Build());
      Assert.AreEqual("ReadItemByPathRequestBuilder.Version : Positive number expected", exception.Message);
    }

    private async Task<ScItemsResponse> GetItemByIdWithItemSource(ItemSource itemSource)
    {
      var session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
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
        request = ItemSSCRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
        .Version(itemSource.VersionNumber.Value)
        .Build();
      }

      var response = await session.ReadItemAsync(request);
      return response;
    }
  }
}
