namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
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
      this.testData = TestEnvironment.DefaultTestEnvironment();
      BuildVersionItemRequest(testData.AuthenticatedInstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
    }

    private void BuildVersionItemRequest(string instanceUrl, string username, string password, string site = null)
    {
      this.sessionConfig = new SessionConfig(instanceUrl, username, password, site);
      
      this.requestWithItemId = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.ItemWithVersions.Id).Build();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.sessionConfig = null;
    }

    [Test]
    public async void TestGetItemWithNotExistedLanguage()
    {
      const string Db = "web";
      const string Language = "da";
      var itemSource = new ItemSource(Db, Language, "1");
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
      testData.AssertItemSourcesAreEqual(itemSource, resultItem.Source);
      //Assert.AreEqual("", resultItem.Fields["Title"].RawValue);
    }

    private async Task<ScItemsResponse> GetItemByIdWithItemSource(ItemSource itemSource)
    {
      var session = new ScApiSession(this.sessionConfig, itemSource);
      var response = await session.ReadItemAsync(this.requestWithItemId);
      return response;
    }

    [Test]
    public void TestGetItemWithNullLanguage()
    {
      const string Db = "master";
      try
      {
        var itemSource = new ItemSource(Db, null, "1");
      }
      catch (ArgumentNullException exception)
      {
        Assert.True(exception.Message.Contains("Value cannot be null"));
        return;
      }
      Assert.Fail("Exception not thrown");
    }

    [Test]
    public void TestGetItemWithNullDb()
    {
      const string Db = null;      
      try
      {
         var itemSource = new ItemSource(Db, "en", "1");
      }
      catch (ArgumentNullException exception)
      {
        Assert.True(exception.Message.Contains("Value cannot be null"));
        return;
      }
      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemWithMasterDbLanguageAndVersion()
    {
      const string Db = "master";
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Database(Db).Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];

      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      //Assert.AreEqual("Sitecore master", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithWebCaseInsensetive()
    {
      const string Db = "wEB";
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Database(Db).Build();
      var response = await session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];

      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      //Assert.AreEqual("Sitecore", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithCoreDbLanguageAndVersion()
    {
      const string Db = "CoRE";
      var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Database(Db).Build();
      var response = await session.ReadItemAsync(request);

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
      //Assert.AreEqual("Welcome to Sitecore", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithNotExistedVersion()
    {
      const string Db = "web";
      const string Language = "da";
      const string Version = "12";

      var itemSource = new ItemSource(Db, Language, Version);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource(Db, Language, "1");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      //Assert.AreEqual("Danish version 2 web", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithDefaultDbInvalidLanguageAndNotExistedVersion()
    {
      const string Db = "web";
      const string Language = "UKRAINIAN";
      const string Version = "12";

      var itemSource = new ItemSource(Db, Language, Version);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource(Db, "en", "1");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      //Assert.AreEqual("English version 2 web", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithNotExistedDb()
    {
      const string Database = "new_database";
      try
      {
        var session = new ScApiSession(this.sessionConfig, ItemSource.DefaultSource());
        
        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Database(Database).Build();
        await session.ReadItemAsync(request);
      }
      catch (ParserException exception)
      {
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));

        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("Could not find configuration node: databases/database[@id='" + Database + "']"));
        return;
      }
      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemWithInvalidLanguage()
    {
      const string Db = "web";
      const string Language = "#%^^&";

      var itemSource = new ItemSource(Db, Language);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource(Db, "en", "1");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      //Assert.AreEqual("English version 2 web", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithInvalidVersion()
    {
      const string Db = "web";
      const string Language = "da";
      const string Version = "Version";

      try
      {
        var itemSource = new ItemSource(Db, Language, Version);
        await this.GetItemByIdWithItemSource(itemSource);
      }
      catch (ParserException exception)
      {
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));

        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("Cannot recognize item version."));

        return;
      }
      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemWithInvalidDb()
    {
      const string Db = "@#er$#";
      const string Language = "da";

      try
      {
        var itemSource = new ItemSource(Db, Language);
        await this.GetItemByIdWithItemSource(itemSource);
      }
      catch (ParserException exception)
      {
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));

        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("Could not find configuration node: databases/database[@id='" + Db + "']"));

        return;
      }
      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemWithShellSite()
    {
      const string Site = "/sitecore/shell";
      this.BuildVersionItemRequest(this.testData.AuthenticatedInstanceUrl, this.testData.Users.Creatorex.Username, this.testData.Users.Creatorex.Password, Site);
      var response = await this.GetItemByIdWithItemSource(ItemSource.DefaultSource());

      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemWithWebSite()
    {
      const string Site = "/sitecore/website";
      this.BuildVersionItemRequest(this.testData.AuthenticatedInstanceUrl, this.testData.Users.Creatorex.Username, this.testData.Users.Creatorex.Password, Site);
      var response = await this.GetItemByIdWithItemSource(ItemSource.DefaultSource());

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
    }

    [Test]
    public async void TestGetItemWithShellSiteWithoutDomain()
    {
      const string Site = "/website";
      this.BuildVersionItemRequest(this.testData.AuthenticatedInstanceUrl, this.testData.Users.Creatorex.Username, this.testData.Users.Creatorex.Password, Site);
      var response = await this.GetItemByIdWithItemSource(ItemSource.DefaultSource());

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
    }
    [Test]
    public async void TestGetItemWithEmptySite()
    {
      const string Site = "";
      this.BuildVersionItemRequest(this.testData.AuthenticatedInstanceUrl, this.testData.Users.Creatorex.Username, this.testData.Users.Creatorex.Password, Site);
      var response = await this.GetItemByIdWithItemSource(ItemSource.DefaultSource());

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);
    }
    [Test]
    public async void TestGetItemWithInvalidSite()
    {
      const string Site = "/@$%/";
      try
      {
        this.BuildVersionItemRequest(this.testData.AuthenticatedInstanceUrl, this.testData.Users.Creatorex.Username, this.testData.Users.Creatorex.Password, Site);
        await this.GetItemByIdWithItemSource(ItemSource.DefaultSource());
      }
      catch (ParserException exception)
      {
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));

        Assert.AreEqual("Newtonsoft.Json.JsonReaderException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("Unexpected character encountered while parsing value: <. Path '', line 0, position 0"));

        return;
      }
      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemWithNullSite()
    {
      this.BuildVersionItemRequest(this.testData.AuthenticatedInstanceUrl, this.testData.Users.Creatorex.Username, this.testData.Users.Creatorex.Password, null);
      var response = await this.GetItemByIdWithItemSource(ItemSource.DefaultSource());

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
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource("web", Language, Version);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      //Assert.AreEqual("Danish version 2 web", resultItem.Fields["Title"].RawValue);
    }

    [Test]
    public async void TestGetItemWithEmptyLanguage()
    {
      const string Db = "master";
      const string Language = "";
      const string Version = "1";

      var itemSource = new ItemSource(Db, Language, Version);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource(Db,"en", Version);
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      //Assert.AreEqual("English version 1 master", resultItem.Fields["Title"].RawValue);
    }
    [Test]
    public async void TestGetItemWithEmptyVersion()
    {
      const string Db = "master";
      const string Language = "da";
      const string Version = "";

      var itemSource = new ItemSource(Db, Language, Version);
      var response = await this.GetItemByIdWithItemSource(itemSource);

      testData.AssertItemsCount(1, response);
      ISitecoreItem resultItem = response.Items[0];
      testData.AssertItemsAreEqual(testData.Items.ItemWithVersions, resultItem);

      var expectedSource = new ItemSource(Db, Language,"2");
      testData.AssertItemSourcesAreEqual(expectedSource, resultItem.Source);
      //Assert.AreEqual("Danish version 2 master", resultItem.Fields["Title"].RawValue);
    }

  }
}
