namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.Items;
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
    public async void TestGetItemWithNotExistedLanguage()
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
    public void TestGetItemWithNullLanguage()
    {
      const string Db = "master";

      TestDelegate testCode = () =>
      {
        var itemSource = new ItemSource(Db, null, "1");
        Assert.IsNull(itemSource, "unreachable code");
        Assert.Fail("unreachable code");
      };
      Exception exception = Assert.Throws<ArgumentNullException>(testCode);
    }

    [Test]
    public void TestGetItemWithNullDb()
    {
      const string Db = null;

      TestDelegate testCode = () =>
      {
        var itemSource = new ItemSource(Db, "en", "1");
        Assert.IsNull(itemSource, "unreachable code");
        Assert.Fail("unreachable code");
      };
      Exception exception = Assert.Throws<ArgumentNullException>(testCode);
    }

    [Test]
    public async void TestGetItemWithMasterDbLanguageAndVersion()
    {
      const string Db = "master";
      var session = this.CreateAdminSession();
      var response = await this.GetHomeItem(session, Db);

      testData.AssertItemsCount(1, response);

      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      Assert.AreEqual("Sitecore master", resultItem.FieldWithName("Title").RawValue);
    }

    private async Task<ScItemsResponse> GetHomeItem(ScApiSession session, string db = null)
    {
      this.homeItemRequestBuilder.Database(db);
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

      var expectedSource = new ItemSource(Db, Language, "1");
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

      var expectedSource = new ItemSource(Db, "en", "1");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("English version 2 web", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public void TestGetItemWithNotExistedDb()
    {
      const string Database = "new_database";
      var requestBuilder = new ReadItemByIdRequestBuilder(testData.Items.Home.Id).Database(Database);
      var session = this.CreateAdminSession();


      TestDelegate testCode = () =>
      {
        var task = GetItemByIdWithRequestBuilder(requestBuilder, session);
        Task.WaitAll(task);
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

      var expectedSource = new ItemSource(Db, "en", "1");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("English version 2 web", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public void TestGetItemWithInvalidVersion()
    {
      const string Db = "web";
      const string Language = "da";
      const string Version = "Version";
      var itemSource = new ItemSource(Db, Language, Version);
      var session = this.CreateAdminSession(itemSource);


      TestDelegate testCode = () =>
      {
        var task = session.ReadItemAsync(this.requestWithVersionsItemId);
        Task.WaitAll(task);
      };
      Exception exception = Assert.Throws<ParserException>(testCode);

      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Cannot recognize item version."));
    }

    [Test]
    public void TestGetItemWithInvalidDb()
    {
      const string Db = "@#er$#";
      const string Language = "da";
      var itemSource = new ItemSource(Db, Language);
      var session = this.CreateAdminSession(itemSource);


      TestDelegate testCode = () =>
      {
        var task = session.ReadItemAsync(this.requestWithVersionsItemId);
        Task.WaitAll(task);
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
      const string Site = "/sitecore/website";

      var session = this.CreateCreatorexSession(Site);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
    }

    [Test]
    public async void TestGetItemWithShellSiteWithoutDomain()
    {
      const string Site = "/website";

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

      TestDelegate testCode = () =>
      {
        var task = session.ReadItemAsync(this.requestWithVersionsItemId);
        Task.WaitAll(task);
      };
      Exception exception = Assert.Throws<ParserException>(testCode);


      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("Newtonsoft.Json.JsonReaderException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Unexpected character encountered while parsing value: <. Path '', line 0, position 0"));
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
    public async void TestGetItemWithEmptyDb()
    {
      const string Db = "";
      const string Language = "da";
      const string Version = "1";

      var itemSource = new ItemSource(Db, Language, Version);
      var session = this.CreateAdminSession(itemSource);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource("web", Language, Version);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("Danish version 2 web", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithEmptyLanguage()
    {
      const string Db = "master";
      const string Language = "";
      const string Version = "1";

      var itemSource = new ItemSource(Db, Language, Version);
      var session = this.CreateAdminSession(itemSource);
      var response = await session.ReadItemAsync(this.requestWithVersionsItemId);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource(Db, "en", Version);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("English version 1 master", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithEmptyVersion()
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

      var expectedSource = new ItemSource(Db, Language, "2");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      Assert.AreEqual("Danish version 2 master", resultItem.FieldWithName("Title").RawValue);
    }

    private ScApiSession CreateAdminSession(ItemSource itemSource = null)
    {
      var session = this.testData.GetSession(this.testData.InstanceUrl, this.testData.Users.Admin.Username, this.testData.Users.Admin.Password, itemSource);
      return session;
    }

    private ScApiSession CreateCreatorexSession(string site)
    {
      var session = this.testData.GetSession(this.testData.InstanceUrl, this.testData.Users.Creatorex.Username, this.testData.Users.Creatorex.Password, ItemSource.DefaultSource(), site);
      return session;
    }

    private static async Task<ScItemsResponse> GetItemByIdWithRequestBuilder(IGetItemRequestParametersBuilder<IReadItemsByIdRequest> requestBuilder, ScApiSession session)
    {
      var request = requestBuilder.Build();
      var response = await session.ReadItemAsync(request);
      return response;
    }
  }
}
