namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.MockObjects;

  [TestFixture]
  public class CreateItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreSSCSession session;
    private ISitecoreSSCSession noThrowCleanupSession;


    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = this.CreateSession();


      // Same as this.session
      var cleanupSession = this.CreateSession();
      this.noThrowCleanupSession = new NoThrowSSCSession(cleanupSession);
    }

    private ISitecoreSSCSession CreateSession()
    {
      var result = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .BuildSession();

      return result;
    }

    public async Task RemoveAll()
    {
      await this.DeleteAllItems("master");
      await this.DeleteAllItems("web");
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
    public async void TestCreateItemByPathAndTemplatePathWithoutFieldsSet()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Create by parent path and template Path");

      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName(expectedItem.DisplayName)
        .Database("master")
        .Build();

      var createResponse = await session.CreateItemAsync(request);

      Assert.IsTrue(createResponse.Created);
    }

    [Test]
    public async void TestCreateItemByPathAndTemplateIdWithoutFieldsSet()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Create by parent path and template ID");

      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.TemplateId)
        .ItemName(expectedItem.DisplayName)
        .Database("master")
        .Build();

      var createResponse = await session.CreateItemAsync(request);

      var readRequest = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.CreateItemsHere.Path + "/" + expectedItem.DisplayName)
                                        .Database("master")
                                        .Build();

      var readResponse = await session.ReadItemAsync(readRequest);

      var resultItem = this.CheckCreatedItem(readResponse, expectedItem);

    }


    [Test]
    public async void TestCreateItemByPathWithSpecifiedFields()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Create with fields");

      const string CreatedTitle = "Created title";
      const string CreatedText = "Created text";
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
         .ItemTemplatePath(testData.Items.Home.Template)
         .ItemName(expectedItem.DisplayName)
         .Database("master")
         .AddFieldsRawValuesByNameToSet("Title", CreatedTitle)
         .AddFieldsRawValuesByNameToSet("Text", CreatedText)
         .Build();

      var createResponse = await session.CreateItemAsync(request);

      var readRequest = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.CreateItemsHere.Path + "/" + expectedItem.DisplayName)
                                        .Database("master")
                                        .Build();

      var readResponse = await session.ReadItemAsync(readRequest);

      var resultItem = this.CheckCreatedItem(readResponse, expectedItem);
      Assert.AreEqual(CreatedTitle, resultItem["Title"].RawValue);
      Assert.AreEqual(CreatedText, resultItem["Text"].RawValue);

    }

    [Test]
    public async void TestCreateItemByIdWithInternationalNameAndFields()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("International Слава Україні ウクライナへの栄光 عالمي");
      const string CreatedTitle = "ఉక్రెయిన్ కు గ్లోరీ Ruhm für die Ukraine";
      const string CreatedText = "युक्रेन गौरव גלורי לאוקראינה";
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName(expectedItem.DisplayName)
        .Database("master")
        .AddFieldsRawValuesByNameToSet("Title", CreatedTitle)
        .AddFieldsRawValuesByNameToSet("Text", CreatedText)
        .Build();

      var createResponse = await session.CreateItemAsync(request);

      var readRequest = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.CreateItemsHere.Path + "/" + expectedItem.DisplayName)
                                        .Database("master")
                                        .Build();

      var readResponse = await session.ReadItemAsync(readRequest);

      var resultItem = this.CheckCreatedItem(readResponse, expectedItem);
      Assert.AreEqual(CreatedTitle, resultItem["Title"].RawValue);
      Assert.AreEqual(CreatedText, resultItem["Text"].RawValue);

    }

    [Test]
    public async void TestCreateItemByPathWithNotExistentFields()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Set not existent field");
      const string CreatedTitle = "Existent title";
      const string CreatedTexttt = "Not existent texttt";
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName(expectedItem.DisplayName)
        .Database("master")
        .AddFieldsRawValuesByNameToSet("Title", CreatedTitle)
        .AddFieldsRawValuesByNameToSet("Texttt", CreatedTexttt)
        .Build();

      var createResponse = await session.CreateItemAsync(request);

      var readRequest = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.CreateItemsHere.Path + "/" + expectedItem.DisplayName)
                                        .Database("master")
                                        .Build();

      var readResponse = await session.ReadItemAsync(readRequest);

      var resultItem = this.CheckCreatedItem(readResponse, expectedItem);
      Assert.AreEqual(CreatedTitle, resultItem["Title"].RawValue);

    }

    [Test]
    public async void TestCreateItemByPathAndSetFieldWithSpacesInName()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Set standard field value");
      const string FieldName = "__Standard values";
      const string FieldValue = "Created standard value 000!! ))";
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName(expectedItem.DisplayName)
        .Database("master")
        .AddFieldsRawValuesByNameToSet(FieldName, FieldValue)
        .Build();

      var createResponse = await session.CreateItemAsync(request);

      var readRequest = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.CreateItemsHere.Path + "/" + expectedItem.DisplayName)
                                        .Database("master")
                                        .Build();

      var readResponse = await session.ReadItemAsync(readRequest);

      var resultItem = this.CheckCreatedItem(readResponse, expectedItem);
      Assert.AreEqual(FieldValue, resultItem[FieldName].RawValue);


    }

    //Item Web API issue #451738
    [Test]
    public async void TestCreateItemByIdAndSetHtmlFieldValue()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Set HTML in field");
      const string FieldName = "Text";
      const string FieldValue = "<div>Welcome to Sitecore!</div><div><br /><a href=\"~/link.aspx?_id=A2EE64D5BD7A4567A27E708440CAA9CD&amp;_z=z\">Accelerometer</a></div>";

      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.TemplateId)
        .ItemName(expectedItem.DisplayName)
        .Database("master")
        .AddFieldsRawValuesByNameToSet(FieldName, FieldValue)
        .Build();

      var createResponse = await session.CreateItemAsync(request);

      Assert.IsTrue(createResponse.Created);

      var readRequest = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.CreateItemsHere.Path + "/" + expectedItem.DisplayName)
                                        .Database("master")
                                        .AddFieldsToRead(FieldName)
                                        .Build();

      var readResponse = await session.ReadItemAsync(readRequest);

      var resultItem = readResponse[0];

      Assert.AreEqual(FieldValue, resultItem[FieldName].RawValue);

    }

    [Test]
    public void TestCreateItemByPathAndSetDuplicateFieldsReturnsException()
    {
      const string FieldName = "Text";
      const string FieldValue = "Duplicate value";

      var exception = Assert.Throws<InvalidOperationException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName("Set duplicate fields")
        .AddFieldsRawValuesByNameToSet(FieldName, FieldValue)
        .AddFieldsRawValuesByNameToSet(FieldName, FieldValue));
      Assert.AreEqual("CreateItemByPathRequestBuilder.FieldsRawValuesByName : duplicate fields are not allowed", exception.Message);
    }

    [Test]
    public async void TestCreateItemByPathAndGetInvalidEmptyAndNullFields()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Create and get invalid field");
      const string FieldName = "@*<<invalid!`fieldname=)";
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName(expectedItem.DisplayName)
        .Database("master")
        .Build();

      var createResponse = await session.CreateItemAsync(request);

      Assert.IsFalse(createResponse.Created);

    }

    [Test]
    public void TestCreateItemWithEmptyOrNullFieldsReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath("/Sample/Sample Item")
        .ItemName("SomeValidName")
        .AddFieldsRawValuesByNameToSet(null, "somevalue"));
      Assert.IsTrue(exception.Message.Contains("fieldName"));

      var exception1 = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath("/Sample/Sample Item")
        .ItemName("SomeValidName")
        .AddFieldsRawValuesByNameToSet("", "somevalue"));
      Assert.AreEqual("CreateItemByIdRequestBuilder.fieldName : The input cannot be empty.", exception1.Message);

      var exception2 = Assert.Throws<ArgumentNullException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath("/Sample/Sample Item")
        .ItemName("SomeValidName")
        .AddFieldsRawValuesByNameToSet("somekey", null));
      Assert.IsTrue(exception2.Message.Contains("fieldValue"));

      var exception3 = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath("/Sample/Sample Item")
        .ItemName("SomeValidName")
        .AddFieldsRawValuesByNameToSet("somekey", ""));
      Assert.AreEqual("CreateItemByIdRequestBuilder.fieldValue : The input cannot be empty.", exception3.Message);
    }


    [Test]
    public async void TestCreateItemByPathAndSetInvalidEmptyAndNullFields()
    {
      await this.RemoveAll();
      var expectedItem = this.CreateTestItem("Create and set invalid field");
      const string FieldName = "@*<<%#==_&@";
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName(expectedItem.DisplayName)
        .Database("master")
        .AddFieldsRawValuesByNameToSet(FieldName, FieldName)
        .Build();

      var createResponse = await session.CreateItemAsync(request);

      var readRequest = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.CreateItemsHere.Path + "/" + expectedItem.DisplayName)
                                        .Database("master")
                                        .Build();

      var readResponse = await session.ReadItemAsync(readRequest);

      var resultItem = readResponse[0];

      Assert.AreEqual(0, resultItem.FieldsCount);

    }

    [Test]
    public void TestCreateItemByPathWithEmptyNameReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName("")
        .Build());
      Assert.AreEqual("CreateItemByIdRequestBuilder.ItemName : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithSpacesOnlyInItemName()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
         .ItemTemplatePath(testData.Items.Home.Template)
         .ItemName("  ")
         .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemName : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithNullItemNameReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
         .ItemTemplatePath(testData.Items.Home.Template)
         .ItemName(null)
         .Build());
      Assert.IsTrue(exception.Message.Contains("CreateItemByPathRequestBuilder.ItemName"));
    }

    [Test]
    public void TestCreateItemByIdWithInvalidItemNameReturnsException()
    {
      const string ItemName = "@*<<%#==_&@";
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName(ItemName)
        .Database("master")
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = session.CreateItemAsync(request);
        await task;
      };
      var exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Data from the internet has unexpected format", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.SSCJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("An item name cannot contain any of the following characters: \\/:?\"<>|[] (controlled by the setting InvalidItemNameChars)", exception.InnerException.Message);
    }

    [Test]
    public void TestCreateItemByPathWithInvalidItemTemplateReturnsException()
    {
      const string Template = "@*<<%#==_&@";
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(Template)
        .ItemName("item with invalid template")
        .Database("master")
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = session.CreateItemAsync(request);
        await task;
      };
      var exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Data from the internet has unexpected format", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.SSCJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("Template item not found.", exception.InnerException.Message);
    }

    [Test]
    public void TestCreateItemByPathWithAnonymousUserReturnsException()
    {
      var anonymousSession = SitecoreSSCSessionBuilder.AnonymousSessionWithHost(testData.InstanceUrl)
        .Site(testData.ShellSite)
        .BuildSession();
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName("item created with anonymous user")
        .Database("master")
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = anonymousSession.CreateItemAsync(request);
        await task;
      };
      var exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Data from the internet has unexpected format", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.SSCJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("Access to site is not granted.", exception.InnerException.Message);
    }

    [Test]
    public void TestCreateItemByIdWithUserWithoutCreateAccessReturnsException()
    {
      var anonymousSession = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.NoCreateAccess)
        .Site(testData.ShellSite)
        .BuildSession();
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName("item created with nocreate user")
        .Database("master")
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = anonymousSession.CreateItemAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Data from the internet has unexpected format", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.SSCJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("AddFromTemplate - Add access required"));
    }

    [Test]
    public void TestCreateItemByPathWithEmptyTemplateReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
         .ItemTemplatePath("")
         .ItemName("Item with empty template")
         .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemTemplate : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithEmptyTemplateIdReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplateId("")
        .ItemName("Item with empty template")
        .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemTemplate : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithSpacesOnlyInTemplateReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplatePath("   ")
        .ItemName("Item with empty template")
         .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemTemplate : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithSpacesOnlyInTemplateIdReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
        .ItemTemplateId("   ")
        .ItemName("Item with empty template")
        .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemTemplate : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathhWithNullTemplateReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentNullException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(this.testData.Items.CreateItemsHere.Path)
         .ItemTemplatePath(null)
         .ItemName("Item with empty template")
         .Build());
      Assert.IsTrue(exception.Message.Contains("CreateItemByIdRequestBuilder.ItemTemplate"));
    }

    [Test]
    public void TestCreateItemByNullPathReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentNullException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(null)
         .ItemTemplatePath("Some template")
         .ItemName("Item with null parent path")
         .Build());
      Assert.IsTrue(exception.Message.Contains("CreateItemByPathRequestBuilder.ItemPath"));
    }

    [Test]
    public void TestCreateItemWithSpacesOnlyInPathReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath("  ")
        .ItemTemplatePath("Some template")
        .ItemName("Item with empty parent path")
        .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.ItemPath : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestCreateItemByPathWithNullDatabaseDoNotReturnsException()
    {
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(testData.Items.Home.Path)
         .ItemTemplatePath("Some template")
         .ItemName("Item with null db")
         .Database(null)
         .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestCreateItemByPathWithEmptyDatabaseDoNotReturnsException()
    {
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(testData.Items.Home.Path)
         .ItemTemplatePath("Some template")
         .ItemName("Item with empty db")
         .Database("")
         .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestCreateItemByPathWithNullLanguageDoNotReturnsException()
    {
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(testData.Items.Home.Path)
         .ItemTemplatePath("Some template")
         .ItemName("Item with null language")
         .Language(null)
         .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestCreateItemByPathWithSpacesOnlyInDatabaseReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.CreateItemRequestWithParentPath(testData.Items.Home.Path)
         .ItemTemplatePath("Some template")
         .ItemName("Item with empty db")
         .Database("   ")
         .Build());
      Assert.AreEqual("CreateItemByPathRequestBuilder.Database : The input cannot be empty.", exception.Message);
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
      ISitecoreItem resultItem = createResponse[0];
      this.testData.AssertItemsAreEqual(expectedItem, resultItem);

      return resultItem;
    }

    private async Task DeleteAllItems(string database)
    {
      var getItemsToDelet = ItemSSCRequestBuilder.ReadChildrenRequestWithId(this.testData.Items.CreateItemsHere.Id)
          .Database(database)
          .Build();

      ScItemsResponse items = await this.noThrowCleanupSession.ReadChildrenAsync(getItemsToDelet);

      foreach (var item in items) { 

        var deleteFromMaster = ItemSSCRequestBuilder.DeleteItemRequestWithId(item.Id)
          .Database(database)
          .Build();
        await this.noThrowCleanupSession.DeleteItemAsync(deleteFromMaster);
      
      }

    }
  }
}