namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.UserRequest;
  using Sitecore.MobileSDK.Session;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  [TestFixture]
  public class SetLanguageDbVersionTest
  {
    private TestEnvironment testData;
    private IReadItemsByIdRequest requestWithVersionsItemId;
    private ReadItemByIdRequestBuilder homeItemRequestBuilder;

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();

      var builder = new ReadItemByIdRequestBuilder(this.testData.Items.ItemWithVersions.Id).Payload(PayloadType.Content);
      this.requestWithVersionsItemId = builder.Build();

      homeItemRequestBuilder = new ReadItemByIdRequestBuilder(this.testData.Items.Home.Id);
      homeItemRequestBuilder.Payload(PayloadType.Content);
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
    }

    [Test]
    public async void TestGetItemWithNotExistentLanguage()
    {
      const string Db = "web";
      const string Language = "da";
      var itemSource = new ItemSource(Db, Language, "1");
      var session = this.CreateAdminSession(itemSource);
      var response = await this.GetHomeItem(session);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      Assert.AreEqual(testData.Items.Home.Id, resultItem.Id);
      testData.AssertItemSourcesAreEqual(itemSource, resultItem.Source);
      Assert.AreEqual("", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithNullLanguage()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Admin)
          .DefaultDatabase("master")
          .BuildReadonlySession();

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
        .Version("1")
        .Build();

      var itemRequest = await session.ReadItemAsync(request);
      Assert.IsNotNull(itemRequest);
      Assert.AreEqual(1, itemRequest.ResultCount);
    }

    [Test]
    public async void TestGetItemWithNullDb()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Admin)
          .DefaultLanguage("en")
          .BuildReadonlySession();


      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
        .Version("1")
        .Build();

      var itemRequest = await session.ReadItemAsync(request);
      Assert.IsNotNull(itemRequest);
      Assert.AreEqual(1, itemRequest.ResultCount);
    }

    [Test]
    public async void TestGetItemWithMasterDb()
    {
      const string Db = "master";
      var session = this.CreateAdminSession();
      var response = await this.GetHomeItem(session, Db);

      testData.AssertItemsCount(1, response);

      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      Assert.AreEqual("Sitecore master", resultItem.FieldWithName("Title").RawValue);
    }

    private async Task<ScItemsResponse> GetHomeItem(IReadItemActions session, string db = null)
    {
      if (db != null)
      {
        this.homeItemRequestBuilder.Database(db);
      }
      var response = await GetItemByIdWithRequestBuilder(this.homeItemRequestBuilder, session);
      return response;
    }

    [Test]
    public async void TestGetItemWithWebCaseInsensetive()
    {
      const string Db = "wEB";
      var session = this.CreateAdminSession();
      var response = await this.GetHomeItem(session, Db);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];

      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      Assert.AreEqual("Sitecore", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithCoreDbLanguageAndVersion()
    {
      const string Db = "CoRE";
      var session = this.CreateAdminSession();
      var response = await this.GetHomeItem(session, Db);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      var expectedItem = new TestEnvironment.Item
      {
        DisplayName = this.testData.Items.Home.DisplayName,
        Id = this.testData.Items.Home.Id,
        Path = this.testData.Items.Home.Path,
        Template = "Sitecore Client/Home"
      };
      testData.AssertItemsAreEqual(expectedItem, resultItem);
      Assert.AreEqual("Welcome to Sitecore", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithNotExistedVersion()
    {
      const string Db = "web";
      const string Language = "da";
      const string Version = "12";

      var itemSource = new ItemSource(Db, Language, Version);
      var session = this.CreateAdminSession(itemSource);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource(Db, Language, "2");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("Danish version 2 web", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithDefaultDbInvalidLanguageAndNotExistedVersion()
    {
      const string Db = "web";
      const string Language = "UKRAINIAN";
      const string Version = "12";

      var itemSource = new ItemSource(Db, Language, Version);
      var session = this.CreateAdminSession(itemSource);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource(Db, "en", "2");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("English version 2 web", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public void TestGetItemWithNotExistedDb()
    {
      const string Database = "new_database";
      var requestBuilder = new ReadItemByIdRequestBuilder(testData.Items.Home.Id).Database(Database);
      var session = this.CreateAdminSession();


      TestDelegate testCode = async () =>
      {
        var task = GetItemByIdWithRequestBuilder(requestBuilder, session);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);


      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Could not find configuration node: databases/database[@id='" + Database + "']"));
    }

    [Test]
    public async void TestGetItemWithInvalidLanguage()
    {
      const string Db = "web";
      const string Language = "#%^^&";

      var itemSource = new ItemSource(Db, Language);
      var session = this.CreateAdminSession(itemSource);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource(Db, "en", "2");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("English version 2 web", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public void TestGetItemWithInvalidDb()
    {
      const string Db = "@#er$#";
      const string Language = "da";
      var itemSource = new ItemSource(Db, Language);
      var session = this.CreateAdminSession(itemSource);


      TestDelegate testCode = async () =>
      {
        var task = session.ReadItemAsync(this.requestWithVersionsItemId);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);

      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Could not find configuration node: databases/database[@id='" + Db + "']"));
    }

    [Test]
    public async void TestGetItemWithShellSite()
    {
      var site = testData.ShellSite;
      var session = this.CreateCreatorexSession(site);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemWithWebSite()
    {
      const string Site = "/";

      var session = this.CreateCreatorexSession(Site);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
    }

    [Test]
    public async void TestGetItemWithShellSiteWithoutDomain()
    {
      const string Site = "/";

      var session = this.CreateCreatorexSession(Site);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
    }

    [Test]
    public async void TestGetItemWithEmptySite()
    {
      const string Site = "";
      var session = this.CreateCreatorexSession(Site);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
    }
    [Test]
    public void TestGetItemWithInvalidSite()
    {
      const string Site = "/@$%/";
      var session = this.CreateCreatorexSession(Site);

      TestDelegate testCode = async () =>
      {
        var task = session.ReadItemAsync(this.requestWithVersionsItemId);
        await task;
      };
      Exception exception = Assert.Throws<RsaHandshakeException>(testCode);
      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.RsaHandshakeException", exception.GetType().ToString());
      Assert.AreEqual("[Sitecore Mobile SDK] Public key not received properly", exception.Message);
      Assert.AreEqual("System.Xml.XmlException", exception.InnerException.GetType().ToString());


      // Windows : "For security reasons DTD is prohibited in this XML document."
      // iOS : {System.Xml.XmlException: Document Type Declaration (DTD) is prohibited in this XML.  Line 1, position 10.
      Assert.True(exception.InnerException.Message.Contains("is prohibited in this XML"));
      Assert.True(exception.InnerException.Message.Contains("DTD"));
    }

    [Test]
    public async void TestGetItemWithNullSite()
    {
      var session = this.CreateCreatorexSession(null);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
    }

    [Test]
    public async void TestGetItemWithEmptyDbInItemSource()
    {
      const string Db = "";
      const string Language = "da";
      const string Version = "1";

      var itemSource = new ItemSource(Db, Language, Version);
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          //          .DefaultDatabase("master") // throws exception
          .DefaultLanguage("da")
          .BuildReadonlySession();


      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.ItemWithVersions.Id)
        .Payload(PayloadType.Content)
        .Version("1")
        .Build();



      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource("web", Language, Version);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("Danish version 2 web", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithEmptyLanguageInItemSource()
    {
      const string Db = "master";
      const string Language = "";
      const string Version = "1";

      var itemSource = new ItemSource(Db, Language, Version);
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          .DefaultDatabase("master")
        //.DefaultLanguage(""); // throws exception
          .BuildReadonlySession();


      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.ItemWithVersions.Id)
        .Payload(PayloadType.Content)
        .Version("1")
        .Build();



      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource(Db, "en", Version);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("English version 1 master", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithEmptyVersionInItemSource()
    {
      const string Db = "master";
      const string Language = "da";
      const string Version = "";

      var itemSource = new ItemSource(Db, Language, Version);
      var session = this.CreateAdminSession(itemSource);

      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      Assert.AreEqual(Db, resultItem.Source.Database);
      Assert.AreEqual(Language, resultItem.Source.Language);
      var expectedSource = new ItemSource(Db, Language, "2");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("Danish version 2 master", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public void TestGetItemWithNullVersionInRequestByPath()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Version(null).Build());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Version : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestGetItemWithSpacesInVersionInRequestById()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Version(" ").Build());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Version : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestGetItemWithEmpryLanguageInRequestByQuery()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Id).Language("").Build());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Language : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestGetItemWithSpacesInLanguageInRequestByPath()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Language("   ").Build());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Language : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestGetItemWithNullLanguageInRequestById()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Language(null).Build());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Language : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestGetItemWithEmptyDatabaseInRequestById()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Database("").Build());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Database : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestGetItemWithNullDatabaseInRequestByPath()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Database(null).Build());
      Assert.AreEqual("System.ArgumentException", exception.GetType().ToString());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Database : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestGetItemWithSpacesInDatabaseInRequestByQuery()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Id)
        .Database(" 	")
        .Build());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Database : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestGetItemByPathWithOverrideLanguageTwice()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path)
        .Language("da")
        .Language("en")
        .Build());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Language : The language cannot be assigned twice", exception.Message);
    }

    [Test]
    public void TestGetItemByIdWithOverrideVersionTwice()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => 
        ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.ItemWithVersions.Id)
          .Version("2")
          .Version("1")
          .Build());
      Assert.AreEqual("AbstractGetItemRequestBuilder.Version : The item's version cannot be assigned twice", exception.Message);
    }

    private ISitecoreWebApiReadonlySession CreateAdminSession(ItemSource itemSource = null)
    {
      var builder = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin);

      if (null != itemSource)
      {
        builder.DefaultDatabase(itemSource.Database).DefaultLanguage(itemSource.Language);
      }

      var session = builder.BuildReadonlySession();
      return session;
    }

    private ISitecoreWebApiReadonlySession CreateCreatorexSession(string site)
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(this.testData.Users.Creatorex)
          .Site(site)
          .DefaultDatabase("web")
          .DefaultLanguage("en")
          .BuildReadonlySession();

      return session;
    }

    private static async Task<ScItemsResponse> GetItemByIdWithRequestBuilder(IBaseRequestParametersBuilder<IReadItemsByIdRequest> requestBuilder, IReadItemActions session)
    {
      var request = requestBuilder.Build();
      var response = await session.ReadItemAsync(request);

      return response;
    }
  }
}
