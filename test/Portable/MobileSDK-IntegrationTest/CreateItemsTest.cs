﻿namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using SitecoreMobileSDKMockObjects;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class CreateItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiSession session;
    private ISitecoreWebApiSession noThrowCleanupSession;


    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = this.CreateSession();


      // Same as this.session
      var cleanupSession = this.CreateSession();
      this.noThrowCleanupSession = new NoThrowWebApiSession(cleanupSession);
    }

    private ISitecoreWebApiSession CreateSession()
    {
      var result = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .BuildSession();

      return result;
    }

    public async Task<ScDeleteItemsResponse> RemoveAll()
    {
      await this.DeleteAllItems("master");
      return await this.DeleteAllItems("web");
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;

      this.session.Dispose();
      this.session = null;

      this.noThrowCleanupSession.Dispose();
      this.noThrowCleanupSession = null;
    }

    [Test]
    public async void TestCreateItemByIdWithoutFieldsSet()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Create by parent id");

      var request = this.CreateByIdRequestBuilder()
         .ItemName(expectedItem.DisplayName)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      this.GetAndCheckItem(expectedItem, resultItem);
    }

    [Test]
    public async void TestCreateItemByIdWithOverridenDatabaseAndLanguageInRequest()
    {
      await this.RemoveAll();
      const string Db = "web";
      const string Language = "da";
      var expectedItem = this.CreateTestItem("Create danish version in web from request");

      var adminSession =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Admin)
          .Site(testData.ShellSite)
          .DefaultDatabase("master")
          .DefaultLanguage("en")
          .BuildSession();

      var request = ItemWebApiRequestBuilder.CreateItemRequestWithId(testData.Items.CreateItemsHere.Id)
        .ItemTemplatePath(expectedItem.Template)
         .ItemName(expectedItem.DisplayName)
         .Database(Db)
         .Language(Language)
         .Build();

      var createResponse = await adminSession.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(Db, resultItem.Source.Database);
      Assert.AreEqual(Language, resultItem.Source.Language);
    }

    [Test]
    public async void TestCreateItemByPathWithDatabaseAndLanguageInSession()
    {
      await this.RemoveAll();
      const string Db = "web";
      const string Language = "da";
      var expectedItem = this.CreateTestItem("Create danish version in web from session");

      var adminSession =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Admin)
          .Site(testData.ShellSite)
          .DefaultDatabase(Db)
          .DefaultLanguage(Language)
          .BuildSession();

      var request = ItemWebApiRequestBuilder.CreateItemRequestWithPath(testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(expectedItem.Template)
         .ItemName(expectedItem.DisplayName)
         .Build();

      var createResponse = await adminSession.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(Db, resultItem.Source.Database);
      Assert.AreEqual(Language, resultItem.Source.Language);
    }


    [Test]
    public async void TestCreateItemByPathAndTemplateIdWithoutFieldsSet()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Create by parent path and template ID");

      var request = this.CreateByIdRequestBuilder()
         .ItemName(expectedItem.DisplayName)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      this.GetAndCheckItem(expectedItem, resultItem);
    }


    [Test]
    public async void TestCreateItemByPathWithSpecifiedFields()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Create with fields");
      const string CreatedTitle = "Created title";
      const string CreatedText = "Created text";
      var request = this.CreateByPathRequestBuilder()
         .ItemName(expectedItem.DisplayName)
         .AddFieldsRawValuesByName("Title", CreatedTitle)
         .AddFieldsRawValuesByName("Text", CreatedText)
         .AddFields("Text", "Title")
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(CreatedTitle, resultItem.FieldWithName("Title").RawValue);
      Assert.AreEqual(CreatedText, resultItem.FieldWithName("Text").RawValue);

      this.GetAndCheckItem(expectedItem, resultItem);
    }

    [Test]
    public async void TestCreateItemByIdWithInternationalNameAndFields()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("International Слава Україні ウクライナへの栄光 عالمي");
      const string CreatedTitle = "ఉక్రెయిన్ కు గ్లోరీ Ruhm für die Ukraine";
      const string CreatedText = "युक्रेन गौरव גלורי לאוקראינה";
      var request = this.CreateByIdRequestBuilder()
         .ItemName(expectedItem.DisplayName)
         .AddFieldsRawValuesByName("Title", CreatedTitle)
         .AddFieldsRawValuesByName("Text", CreatedText)
         .Payload(PayloadType.Content)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(CreatedTitle, resultItem.FieldWithName("Title").RawValue);
      Assert.AreEqual(CreatedText, resultItem.FieldWithName("Text").RawValue);

      this.GetAndCheckItem(expectedItem, resultItem);
    }

    [Test]
    public async void TestCreateItemByIdWithNotExistentFields()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Set not existent field");
      const string CreatedTitle = "Existent title";
      const string CreatedTexttt = "Not existent texttt";
      var request = this.CreateByIdRequestBuilder()
         .Payload(PayloadType.Content)
         .ItemName(expectedItem.DisplayName)
         .AddFieldsRawValuesByName("Title", CreatedTitle)
         .AddFieldsRawValuesByName("Texttt", CreatedTexttt)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(CreatedTitle, resultItem.FieldWithName("Title").RawValue);

      this.GetAndCheckItem(expectedItem, resultItem);
    }

    [Test]
    public async void TestCreateItemByPathAndSetFieldWithSpacesInName()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Set standard field value");
      const string FieldName = "__Standard values";
      const string FieldValue = "Created standard value 000!! ))";
      var request = CreateByPathRequestBuilder()
         .AddFields(FieldName)
         .ItemName(expectedItem.DisplayName)
         .AddFieldsRawValuesByName(FieldName, FieldValue)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(FieldValue, resultItem.FieldWithName(FieldName).RawValue);

      this.GetAndCheckItem(expectedItem, resultItem);
    }

    [Test]
    [Ignore]
    public async void TestCreateItemByIdAndSetHtmlFieldValue()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Set HTML in field");
      const string FieldName = "Text";
      const string FieldValue = "<div>Welcome to Sitecore!</div><div><br /><a href=\"~/link.aspx?_id=A2EE64D5BD7A4567A27E708440CAA9CD&amp;_z=z\">Accelerometer</a></div>";

      var request = CreateByPathRequestBuilder()
         .AddFields(FieldName)
         .ItemName(expectedItem.DisplayName)
         .AddFieldsRawValuesByName(FieldName, FieldValue)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(FieldValue, resultItem.FieldWithName(FieldName).RawValue);

      this.GetAndCheckItem(expectedItem, resultItem);
    }



    [Test]
    [Ignore]
    public async void TestCreateItemByPathFromBranch()
    {
      await this.RemoveAll();
      TestEnvironment.Item expectedItem = this.CreateTestItem("Multiple item brunch");

      var request = ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplateId("{14416817-CDED-45AF-99BF-2DE9883B7AC3}")
//        .ItemName("Name should Not be in the branch")
        .Database("master")
        .Build();

      var createResponse = await session.CreateItemAsync(request);
      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);

      this.GetAndCheckItem(expectedItem, resultItem);
    }

    [Test]
    public void TestCreateItemByIdAndGetDuplicateFieldsReturnsException()
    {
      const string FieldName = "Text";

      var exception = Assert.Throws<InvalidOperationException>(() =>
        ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
         .ItemTemplatePath(testData.Items.Home.Template)
         .AddFields(FieldName, "Title", FieldName)
         .ItemName("Get duplicate fields")
         .Build());
      Assert.AreEqual("CreateItemByIdRequestBuilder.Fields : duplicate fields are not allowed", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathAndSetDuplicateFieldsReturnsException()
    {
      const string FieldName = "Text";
      const string FieldValue = "Duplicate value";

      var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName("Set duplicate fields")
        .AddFieldsRawValuesByName(FieldName, FieldValue)
        .AddFieldsRawValuesByName(FieldName, FieldValue));
      Assert.AreEqual("CreateItemByPathRequestBuilder.FieldsRawValuesByName : duplicate fields are not allowed", exception.Message);
    }

    [Test]
    public async void TestCreateItemByPathAndGetInvalidEmptyAndNullFields()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Create and get invalid field");
      const string FieldName = "@*<<invalid!`fieldname=)";
      var request = CreateByPathRequestBuilder()
         .AddFields(FieldName, null, "")
         .ItemName(expectedItem.DisplayName)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(0, resultItem.Fields.Count);

      this.GetAndCheckItem(expectedItem, resultItem);
    }

    [Test]
    public void TestCreateItemEmtyOrNullFieldsNotAvailable()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => 
      {
        ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
          .ItemTemplatePath("/Sample/Sample Item")
          .AddFieldsRawValuesByName(null, "somevalue");
      });
      Assert.IsTrue(exception.Message.Contains("fieldKey"));

      var exception1 = Assert.Throws<ArgumentException>(() => 
      {
        ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
          .ItemTemplatePath("/Sample/Sample Item")
          .AddFieldsRawValuesByName("", "somevalue");
      });
      Assert.AreEqual("CreateItemByIdRequestBuilder.fieldKey : The input cannot be empty.", exception1.Message);

      var exception2 = Assert.Throws<ArgumentNullException>(() => 
      {
        ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
          .ItemTemplatePath("/Sample/Sample Item")
          .AddFieldsRawValuesByName("somekey", null);
      });
      Assert.IsTrue (exception2.Message.Contains ("fieldValue"));

      var exception3 = Assert.Throws<ArgumentException>(() => 
      {
        ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
          .ItemTemplatePath("/Sample/Sample Item")
          .AddFieldsRawValuesByName("somekey", "");
      });
      Assert.AreEqual("CreateItemByIdRequestBuilder.fieldValue : The input cannot be empty.", exception3.Message);
    }


    [Test]
    public async void TestCreateItemByIdAndSetInvalidEmptyAndNullFields()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Create and set invalid field");
      const string FieldName = "@*<<%#==_&@";
      var request = CreateByPathRequestBuilder()
         .AddFields(FieldName)
         .AddFieldsRawValuesByName(FieldName, FieldName)
         .ItemName(expectedItem.DisplayName)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var resultItem = this.CheckCreatedItem(createResponse, expectedItem);
      Assert.AreEqual(0, resultItem.Fields.Count);

      this.GetAndCheckItem(expectedItem, resultItem);
    }

    [Test]
    public void TestCreateItemByIdWithEmptyNameReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
        .ItemTemplatePath(testData.Items.Home.Template) 
        .ItemName("")
        .Build());
      Assert.AreEqual("CreateItemByIdRequestBuilder.ItemName : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithSpacesOnlyInItemName()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
         .ItemTemplatePath(testData.Items.Home.Template)
         .ItemName("  ")
         .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemName : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithNullItemNameReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
         .ItemTemplatePath(testData.Items.Home.Template)
         .ItemName(null)
         .Build());
      Assert.IsTrue(exception.Message.Contains("CreateItemByPathRequestBuilder.ItemName"));
    }

    [Test]
    public void TestCreateItemByPathWithoutItemNameReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
         .Build());
      Assert.IsTrue(exception.Message.Contains("CreateItemByPathRequestBuilder.ItemName"));
    }

    [Test]
    public void TestCreateItemByIdWithInvalidItemNameReturnsException()
    {
      const string ItemName = "@*<<%#==_&@";
      var request = CreateByPathRequestBuilder()
         .ItemName(ItemName)
         .Build();
      TestDelegate testCode = async () =>
      {
        var task = session.CreateItemAsync(request);
        await task;
      };
      var exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Unable to download data from the internet", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("An item name cannot contain any of the following characters: \\/:?\"<>|[] (controlled by the setting InvalidItemNameChars)", exception.InnerException.Message);
    }

    [Test]
    public void TestCreateItemByPathWithInvalidItemTemplateReturnsException()
    {
      const string Template = "@*<<%#==_&@";
      var request = ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(Template)
        .Database("master")
        .ItemName("item with invalid template")
        .Build();
      TestDelegate testCode = async () =>
      {
        var task = session.CreateItemAsync(request);
        await task;
      };
      var exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Unable to download data from the internet", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("Template item not found.", exception.InnerException.Message);
    }

    [Test]
    public void TestCreateItemByPathWithAnonymousUserReturnsException()
    {
      var anonymousSession = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(testData.InstanceUrl)
        .Site(testData.ShellSite)
        .BuildSession();
      var request = this.CreateByPathRequestBuilder()
        .ItemName("item created with anonymous user")
        .Build();
      TestDelegate testCode = async () =>
      {
        var task = anonymousSession.CreateItemAsync(request);
        await task;
      };
      var exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Unable to download data from the internet", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("Access to site is not granted.", exception.InnerException.Message);
    }

    [Test]
    public void TestCreateItemByIdWithUserWithoutCreateAccessReturnsException()
    {
      var anonymousSession = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.NoCreateAccess)
        .Site(testData.ShellSite)
        .BuildSession();
      var request = this.CreateByPathRequestBuilder()
        .ItemName("item created with nocreate user")
        .Build();
      TestDelegate testCode = async () =>
      {
        var task = anonymousSession.CreateItemAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Unable to download data from the internet", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("AddFromTemplate - Add access required"));
    }

    [Test]
    public void TestCreateItemByPathWithEmptyTemplateReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
         .ItemTemplatePath("")
         .ItemName("Item with empty template")
         .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemTemplate : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithEmptyTemplateIdReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplateId("")
        .ItemName("Item with empty template")
        .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemTemplate : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithSpacesOnlyInTemplateReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath("   ")
        .ItemName("Item with empty template")
         .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemTemplate : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithSpacesOnlyInTemplateIdReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplateId("   ")
        .ItemName("Item with empty template")
        .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemTemplate : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByIdWithTwoNamesReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
         .ItemTemplatePath("template")
         .ItemName("Item name 1")
         .ItemName("Item name 2")
         .Build());
      Assert.AreEqual("CreateItemByIdRequestBuilder.ItemName : Property cannot be assigned twice.", exception.Message);
    }

    [Test]
    public void TestCreateItemByIdhWithNullTemplateReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
         .ItemTemplatePath(null)
         .ItemName("Item with empty template")
         .Build());
      Assert.IsTrue(exception.Message.Contains("CreateItemByIdRequestBuilder.ItemTemplate"));
    }

    [Test]
    public void TestCreateItemByEmptyIdReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId("")
         .ItemTemplatePath("Some template")
         .ItemName("Item with empty parent id")
         .Build());
      Assert.AreEqual("CreateItemByIdRequestBuilder.ItemId : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByNullPathReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(null)
         .ItemTemplatePath("Some template")
         .ItemName("Item with null parent path")
         .Build());
      Assert.IsTrue(exception.Message.Contains("CreateItemByPathRequestBuilder.ItemPath"));
    }

    [Test]
    public void TestCreateItemByInvalidIdReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId(testData.Items.Home.Path)
         .ItemTemplatePath("Some template")
         .ItemName("Item with invalid parent id")
         .Build());
      Assert.AreEqual("CreateItemByIdRequestBuilder.ItemId : Item id must have curly braces '{}'", exception.Message);
    }

    [Test]
    public void TestCreateItemWithSpacesOnlyInPathReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath("  ")
        .ItemTemplatePath("Some template")
        .ItemName("Item with empty parent path")
        .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemPath : The input cannot be empty.", exception.Message);
    }

    [Test]
    public async void TestCreateItemWithNotExistentItemId()
    {
      const string Id = "{000D009F-D000-000A-0C0C-0A0DF0E00EF0}";
      var request = ItemWebApiRequestBuilder.CreateItemRequestWithId(Id)
        .ItemTemplatePath(testData.Items.Home.Template)
        .Database("master")
        .ItemName("item with not existent id")
        .Build();
      var createResponse = await session.CreateItemAsync(request);
      Assert.AreEqual(0, createResponse.Items.Count);
    }

    [Test]
    public void TestCreateItemByIdWithNullDatabaseReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId(testData.Items.Home.Id)
         .ItemTemplatePath("Some template")
         .ItemName("Item with null db")
         .Database(null)
         .Build());
      Assert.IsTrue(exception.Message.Contains("CreateItemByIdRequestBuilder.Database"));
    }

    [Test]
    public void TestCreateItemByIdWithEmptyDatabaseReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId(testData.Items.Home.Id)
         .ItemTemplatePath("Some template")
         .ItemName("Item with empty db")
         .Database("")
         .Build());
      Assert.AreEqual("CreateItemByIdRequestBuilder.Database : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithNullLanguageReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(testData.Items.Home.Path)
         .ItemTemplatePath("Some template")
         .ItemName("Item with null language")
         .Language(null)
         .Build());
      Assert.IsTrue(exception.Message.Contains("CreateItemByPathRequestBuilder.Language"));
    }

    [Test]
    public void TestCreateItemByIdWithSpacesOnlyInLanguageReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithId(testData.Items.Home.Id)
         .ItemTemplatePath("Some template") 
         .ItemName("Item with empty language")
         .Language("  ")
         .Build());
      Assert.AreEqual("CreateItemByIdRequestBuilder.Language : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithSpacesOnlyInDatabaseReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.CreateItemRequestWithPath(testData.Items.Home.Path)
         .ItemTemplatePath("Some template")
         .ItemName("Item with empty db")
         .Database("   ")
         .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.Database : The input cannot be empty.", exception.Message);
    }

    private async void GetAndCheckItem(TestEnvironment.Item expectedItem, ISitecoreItem resultItem)
    {
      var readResponse = await this.GetItemById(resultItem.Id);

      this.testData.AssertItemsCount(1, readResponse);
      this.testData.AssertItemsAreEqual(expectedItem, readResponse.Items[0]);
    }

    private async Task<ScItemsResponse> GetItemById(string id)
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(id).Database("master").Build();
      var response = await this.session.ReadItemAsync(request);
      return response;
    }

    private ICreateItemRequestParametersBuilder<ICreateItemByIdRequest> CreateByIdRequestBuilder()
    {
      return ItemWebApiRequestBuilder.CreateItemRequestWithId(this.testData.Items.CreateItemsHere.Id)
       .ItemTemplatePath(testData.Items.Home.Template)
        .Database("master");
    }

    private ICreateItemRequestParametersBuilder<ICreateItemByPathRequest> CreateByPathRequestBuilder()
    {
      return ItemWebApiRequestBuilder.CreateItemRequestWithPath(this.testData.Items.CreateItemsHere.Path)
          .ItemTemplatePath(testData.Items.Home.Template)
          .Database("master");
    }

    private TestEnvironment.Item CreateTestItem(string name)
    {
      return new TestEnvironment.Item
      {
        DisplayName = name,
        Path = testData.Items.CreateItemsHere.Path + "/" + name,
        Template = this.testData.Items.Home.Template
      };
    }

    private ISitecoreItem CheckCreatedItem(ScItemsResponse createResponse, TestEnvironment.Item expectedItem)
    {
      this.testData.AssertItemsCount(1, createResponse);
      ISitecoreItem resultItem = createResponse.Items[0];
      this.testData.AssertItemsAreEqual(expectedItem, resultItem);

      return resultItem;
    }

    private async Task<ScDeleteItemsResponse> DeleteAllItems(string database)
    {
      var deleteFromMaster = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(this.testData.Items.CreateItemsHere.Path)
          .AddScope(ScopeType.Children)
          .Database(database)
          .Build();
      return await this.noThrowCleanupSession.DeleteItemAsync(deleteFromMaster);
    }
  }
}