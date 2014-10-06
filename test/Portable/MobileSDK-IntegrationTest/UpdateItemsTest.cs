namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API.Exceptions;

  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class UpdateItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiSession session;
    private ISitecoreWebApiSession noThrowCleanupSession;
    private const string SampleId = "{SAMPLEID-7808-4798-A461-1FB3EB0A43E5}";

    [SetUp]
    public void SetupSession()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = this.CreateSession();

      // Same as this.session
      var cleanupSession = this.CreateSession();
      this.noThrowCleanupSession = new NoThrowWebApiSession(cleanupSession);
    }

    private ISitecoreWebApiSession CreateSession()
    {
      return SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultDatabase("master")
        .BuildSession();
    }


    private async Task<ScDeleteItemsResponse> RemoveAll()
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
    public async void TestUpdateDanishItemByPath()
    {
      await this.RemoveAll();
      const string Language = "da";
      var titleValue = RandomText();
      var textValue = RandomText();
      var itemSession = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultLanguage(Language)
        .DefaultDatabase("master")
        .BuildSession();
      ISitecoreItem item = await this.CreateItem("Danish item to update", null, itemSession);

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithPath(item.Path)
        .AddFieldsRawValuesByNameToSet("Title", titleValue)
        .AddFieldsRawValuesByNameToSet("Text", textValue)
        .Language(Language)
        .Build();

      var result = await this.session.UpdateItemAsync(request);

      Assert.AreEqual(1, result.ResultCount);
      var resultItem = result[0];
      Assert.AreEqual(item.Id, resultItem.Id);
      Assert.AreEqual(titleValue, resultItem["Title"].RawValue);
      Assert.AreEqual(textValue, resultItem["Text"].RawValue);
      Assert.AreEqual(Language, resultItem.Source.Language);
    }

    [Test]
    public async void TestUpdateItemByNotExistentId()
    {
      var textValue = RandomText();

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithId(SampleId)
        .AddFieldsRawValuesByNameToSet("Text", textValue)
        .Build();

      var result = await this.session.UpdateItemAsync(request);

      Assert.AreEqual(0, result.ResultCount);
    }

    [Test]
    public void TestUpdateItemByInvalidIdReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithId(testData.Items.Home.Path)
        .Build());
      Assert.AreEqual("UpdateItemByIdRequestBuilder.ItemId : Item id must have curly braces '{}'", exception.Message);
    }

    [Test]
    public void TestUpdateItemByEmptyIdReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithId("")
        .Build());
      Assert.AreEqual("UpdateItemByIdRequestBuilder.ItemId : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestUpdateItemByNullPathReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithPath(null)
        .Build());
      Assert.IsTrue(exception.Message.Contains("UpdateItemByPathRequestBuilder.ItemPath"));
    }

    [Test]
    public void TestUpdateItemByInvalidPathReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithPath("invalid path)")
        .Build());
      Assert.AreEqual("UpdateItemByPathRequestBuilder.ItemPath : should begin with '/'", exception.Message);
    }

    [Test]
    public void TestUpdateItemByIdWithNullDatabaseDoNotReturnsException()
    {
      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithId(SampleId)
        .Database(null)
        .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestUpdateItemByPathWithTwoDatabasesReturnsException()
    {
      var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithPath("/path")
        .Database("db1")
        .Database("db2")
        .Build());
      Assert.AreEqual("UpdateItemByPathRequestBuilder.Database : Property cannot be assigned twice.", exception.Message);
    }

    [Test]
    public void TestUpdateItemByIdWithSpacesOnlyInLanguageReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithId(SampleId)
        .Language("  ")
        .Build());
      Assert.AreEqual("UpdateItemByIdRequestBuilder.Language : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestUpdateItemByIdWithNullReadFieldsReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithId(SampleId)
        .AddFieldsToRead(null)
        .Build());
      Assert.IsTrue(exception.Message.Contains("UpdateItemByIdRequestBuilder.Fields"));
    }

    [Test]
    public void TestUpdateItemByPathWithNullFieldsToUpdateReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithPath("/path")
        .AddFieldsRawValuesByNameToSet(null)
        .Build());
      Assert.IsTrue(exception.Message.Contains("UpdateItemByPathRequestBuilder.FieldsRawValuesByName"));
    }

    [Test]
    public void TestUpdateItemByIdWithDuplicateFieldsToUpdateReturnsException()
    {
      var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithId(SampleId)
        .AddFieldsRawValuesByNameToSet("Title", "Value1")
        .AddFieldsRawValuesByNameToSet("Title", "Value2")
        .Build());
      Assert.AreEqual("UpdateItemByIdRequestBuilder.FieldsRawValuesByName : duplicate fields are not allowed", exception.Message);
    }

    [Test]
    public void TestUpdateItemByPathWithDuplicateFieldsToReadReturnsException()
    {
      var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithPath("/path")
        .AddFieldsToRead("Title")
        .AddFieldsToRead("Title"));
      Assert.AreEqual("UpdateItemByPathRequestBuilder.Fields : duplicate fields are not allowed", exception.Message);
    }

    [Test]
    public void TestUpdateItemByIdWithTwoVersionsReturnsException()
    {
      var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithId(SampleId)
        .Version(1)
        .Version(2)
        .Build());
      Assert.AreEqual("UpdateItemByIdRequestBuilder.Version : Property cannot be assigned twice.", exception.Message);
    }

    [Test]
    public void TestUpdateItemByPathWithSpacesOnlyReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.UpdateItemRequestWithPath("  ")
        .Build());
      Assert.AreEqual("UpdateItemByPathRequestBuilder.ItemPath : The input cannot be empty.", exception.Message);
    }

    [Test]
    public async void TestUpdateItemVersion1ById()
    {
      await this.RemoveAll();
      const int Version = 1;
      var textValue = RandomText();

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithId(testData.Items.ItemWithVersions.Id)
        .AddFieldsRawValuesByNameToSet("Text", textValue)
        .Payload(PayloadType.Full)
        .Version(Version)
        .Build();

      var result = await this.session.UpdateItemAsync(request);

      Assert.AreEqual(1, result.ResultCount);
      var resultItem = result[0];
      Assert.AreEqual(testData.Items.ItemWithVersions.Id, resultItem.Id);
      Assert.AreEqual(textValue, resultItem["Text"].RawValue);
      Assert.True(50 < resultItem.FieldsCount);
      Assert.AreEqual(Version, resultItem.Source.VersionNumber);
    }

    [Test]
    public async void TestUpdateInternationalItemByPath()
    {
      await this.RemoveAll();
      const string TextValue = "ఉక్రెయిన్ కు గ్లోరీ Ruhm für die Ukraine";
      const string ItemName = "גלורי לאוקראינה";
      ISitecoreItem item = await this.CreateItem(ItemName);

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithPath(testData.Items.CreateItemsHere.Path + "/" + ItemName)
        .AddFieldsRawValuesByNameToSet("Text", TextValue)
        .AddFieldsToRead("Text")
        .Payload(PayloadType.Content)
        .Build();

      var result = await this.session.UpdateItemAsync(request);

      Assert.AreEqual(1, result.ResultCount);
      var resultItem = result[0];
      Assert.AreEqual(item.Id, resultItem.Id);
      Assert.AreEqual(item.DisplayName, resultItem.DisplayName);
      Assert.AreEqual(TextValue, resultItem["Text"].RawValue);
    }

    [Test]
    public async void TestUpdateItemByIdWithNotExistentField()
    {
      await this.RemoveAll();
      const string FieldName = "Texttt";
      var fieldValue = RandomText();

      ISitecoreItem item = await this.CreateItem("item to updata not existen field");

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithPath(item.Path)
        .AddFieldsRawValuesByNameToSet(FieldName, fieldValue)
        .AddFieldsToRead(FieldName)
        .Build();

      var result = await this.session.UpdateItemAsync(request);

      Assert.AreEqual(1, result.ResultCount);
      var resultItem = result[0];
      Assert.AreEqual(item.Id, resultItem.Id);
      Assert.AreEqual(0, resultItem.FieldsCount);
    }

    [Test]
    [Ignore]
    public async void TestUpdateItemByIdSetHtmlField()
    {
      await this.RemoveAll();
      const string FieldName = "Text";
      const string FieldValue = "<div>Welcome to Sitecore!</div>";

      ISitecoreItem item = await this.CreateItem("item to updata not existen field");

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithPath(item.Path)
        .AddFieldsRawValuesByNameToSet(FieldName, FieldValue)
        .AddFieldsToRead(FieldName)
        .Build();

      var result = await this.session.UpdateItemAsync(request);

      Assert.AreEqual(1, result.ResultCount);
      var resultItem = result[0];
      Assert.AreEqual(item.Id, resultItem.Id);
      Assert.AreEqual(1, resultItem.FieldsCount);
      Assert.AreEqual(FieldValue, resultItem[FieldName].RawValue);
    }

    [Test]
    public void TestUpdateItemAsAnonymousFromShell()
    {
      const string FieldName = "Text";
      var fieldValue = RandomText();

      var itemSession = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(testData.InstanceUrl)
        .Site(testData.ShellSite)
        .DefaultDatabase("master")
        .BuildSession();

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithPath(testData.Items.ItemWithVersions.Path)
        .AddFieldsRawValuesByNameToSet(FieldName, fieldValue)
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = itemSession.UpdateItemAsync(request);
        await task;
      };
      var exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Data from the internet has unexpected format", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("Access to site is not granted.", exception.InnerException.Message);
    }

    [Test]
    public void TestUpdateItemAsUserWithoutWriteAccess()
    {
      const string FieldName = "Text";
      var fieldValue = RandomText();

      var itemSession = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.NoCreateAccess)
        .Site(testData.ShellSite)
        .DefaultDatabase("master")
        .BuildSession();

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithPath(testData.Items.ItemWithVersions.Path)
        .AddFieldsRawValuesByNameToSet(FieldName, fieldValue)
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = itemSession.UpdateItemAsync(request);
        await task;
      };
      var exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Data from the internet has unexpected format", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("The current user does not have write access to this item"));
    }

    private async Task<ISitecoreItem> CreateItem(string itemName, ISitecoreItem parentItem = null, ISitecoreWebApiSession itemSession = null)
    {
      if (itemSession == null)
      {
        itemSession = session;
      }
      string parentPath = parentItem == null ? this.testData.Items.CreateItemsHere.Path : parentItem.Path;
      var request = ItemWebApiRequestBuilder.CreateItemRequestWithParentPath(parentPath)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName(itemName)
        .Build();
      var createResponse = await itemSession.CreateItemAsync(request);

      Assert.AreEqual(1, createResponse.ResultCount);
      return createResponse[0];
    }

    private static string RandomText()
    {
      return "UpdatedText" + new Random(10000);
    }

    private async Task<ScDeleteItemsResponse> DeleteAllItems(string database)
    {
      var deleteFromMaster = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(this.testData.Items.CreateItemsHere.Path)
        .AddScope(ScopeType.Children)
        .Database(database)
        .Build();
      var response = await this.noThrowCleanupSession.DeleteItemAsync(deleteFromMaster);
      return response;
    }
  }
}