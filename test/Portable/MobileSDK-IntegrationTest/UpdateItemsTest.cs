namespace MobileSDKIntegrationTest
{
  using System;
  using System.Diagnostics;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK.Items;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class UpdateItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiSession session;
    private const string SampleId = "{SAMPLEID-7808-4798-A461-1FB3EB0A43E5}";

    [TestFixtureSetUp]
    public async void TestFixtureSetup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultDatabase("master")
        .BuildSession();

      await this.DeleteAllItems("master");
      await this.DeleteAllItems("web");
    }

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultDatabase("master")
        .BuildSession();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.session = null;
    }
    /*
    [Test]
    public async void TestUpdateItemByIdFromWebDbWithChildrenScope()
    {
      const string Db = "web";
      const string TitleValue = "New title after update";
      const string TextValue = "New text after update";
      var itemSession = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultDatabase(Db)
        .BuildSession();
      ISitecoreItem parentItem = await this.CreateItem("Parent item to update in web", null, itemSession);
      ISitecoreItem childItem = await this.CreateItem("Child item to update in web", parentItem, itemSession);

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithId(parentItem.Id)
        .AddFieldsRawValuesByName("Title", TitleValue)
        .AddFieldsRawValuesByName("Text", TextValue)
        .AddScope(ScopeType.Children)
        .Database(Db)
        .Build();

      var result = await itemSession.UpdateItemAsync(request);

      Assert.AreEqual(1, result.Items.Count);
      var resultItem = result.Items[0];
      Assert.AreEqual(childItem.Id, resultItem.Id);
      Assert.AreEqual(TitleValue, resultItem.FieldWithName("Title").RawValue);
      Assert.AreEqual(TextValue, resultItem.FieldWithName("Text").RawValue);
      Assert.AreEqual(Db, resultItem.Source.Database);

      this.RemoveItem(parentItem);
    }

    [Test]
    public async void TestUpdateDanishItemByPath()
    {
      const string Language = "da";
      const string TitleValue = "New title after update";
      const string TextValue = "New text after update";
      var itemSession = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultLanguage(Language)
        .BuildSession();
      ISitecoreItem item = await this.CreateItem("Danish item to update", null, itemSession);

      var request = ItemWebApiRequestBuilder.UpdateItemsRequestWithPath(item.Path)
        .AddFieldsRawValuesByName("Title", TitleValue)
        .AddFieldsRawValuesByName("Text", TextValue)
        .Language(Language)
        .Build();

      var result = await this.session.UpdateItemsAsync(request);

      Assert.AreEqual(1, result.Items.Count);
      var resultItem = result.Items[0];
      Assert.AreEqual(item.Id, resultItem.Id);
      Assert.AreEqual(TitleValue, resultItem.FieldWithName("Title").RawValue);
      Assert.AreEqual(TextValue, resultItem.FieldWithName("Text").RawValue);
      Assert.AreEqual(Language, resultItem.Source.Language);

      this.RemoveItem(item);
    }
    
    [Test]
    public async void TestUpdateItemByQueryWithParentScope()
    {
      const string TextValue = "Parent text after update";

      ISitecoreItem parentItem = await this.CreateItem("Parent item to update by query");
      ISitecoreItem childItem = await this.CreateItem("Child item to update by query", parentItem);

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithSitecoreQuery(childItem.Path)
        .AddFieldsRawValuesByName("Text", TextValue)
        .AddScope(ScopeType.Parent)
        .Database(Db)
        .Build();

      var result = await this.session.UpdateItemAsync(request);

      Assert.AreEqual(1, result.Items.Count);
      var resultItem = result.Items[0];
      Assert.AreEqual(parentItem.Id, resultItem.Id);
      Assert.AreEqual(TextValue, resultItem.FieldWithName("Text").RawValue);

      this.RemoveItem(parentItem);
    }
    
    [Test]
    public async void TestUpdateItemByPathWithParentChildrenAndSelfScope()
    {
      const string TextValue = "Text after update";

      ISitecoreItem parentItem = await this.CreateItem("Parent item to update by query");
      ISitecoreItem selfItem = await this.CreateItem("Self item to update by query", parentItem);
      ISitecoreItem childItem = await this.CreateItem("Child item to update by query", selfItem);

      var request = ItemWebApiRequestBuilder.UpdateItemRequestWithPath(childItem.Path)
        .AddFieldsRawValuesByName("Text", TextValue)
        .AddScope(ScopeType.Parent)
        .AddScope(ScopeType.Children)
        .AddScope(ScopeType.Self)
        .Build();

      var result = await this.session.UpdateItemAsync(request);

      Assert.AreEqual(3, result.Items.Count);
      var resultItem = result.Items[0];
      Assert.AreEqual(parentItem.Id, resultItem.Id);
      foreach (var item in result.Items)
      {
        Assert.AreEqual(TextValue, item.FieldWithName("Text").RawValue);
      }

      this.RemoveItem(parentItem);
    }
    

    [Test]
    public async void TestUpdateInternationalItemByQuery()
    {
      const string TextValue = "ఉక్రెయిన్ కు గ్లోరీ Ruhm für die Ukraine";
      const string ItemName = "युक्रेन गौरव גלורי לאוקראינה";

      ISitecoreItem item = await this.CreateItem(ItemName);
      
      var request = ItemWebApiRequestBuilder.UpdateItemsRequestWithSitecoreQuery(testData.Items.CreateItemsHere.Path + "/" + ItemName)
        .AddFieldsRawValuesByName("Text", TextValue)
        .Payload(PayloadType.Content)
        .Build();

      var result = await this.session.UpdateItemAsync(request);

      Assert.AreEqual(1, result.Items.Count);
      var resultItem = result.Items[0];
      Assert.AreEqual(item.Id, resultItem.Id);
      Assert.AreEqual(item.DisplayName, resultItem.DisplayName);
      Assert.AreEqual(TextValue, item.FieldWithName("Text").RawValue);

      this.RemoveItem(parentItem);
    }
    */
    private async Task<ISitecoreItem> CreateItem(string itemName, ISitecoreItem parentItem = null, ISitecoreWebApiSession itemSession = null)
    {
      if (itemSession == null)
      {
        itemSession = session;
      }
      string parentPath = parentItem == null ? this.testData.Items.CreateItemsHere.Path : parentItem.Path;
      var request = ItemWebApiRequestBuilder.CreateItemRequestWithPath(parentPath)
        .ItemName(itemName)
        .ItemTemplate(testData.Items.Home.Template)
        .Build();
      var createResponse = await itemSession.CreateItemAsync(request);

      Assert.AreEqual(1, createResponse.Items.Count);
      return createResponse.Items[0];
    }

    private async void RemoveItem(ISitecoreItem item, ISitecoreWebApiSession itemSession = null)
    {
      if (itemSession == null)
      {
        itemSession = this.session;
      }

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithId(item.Id)
        .Database(item.Source.Database)
        .Build();

      var response = await itemSession.DeleteItemAsync(request);
      Assert.AreEqual(1, response.Count);
    }

    private async Task<ScDeleteItemsResponse> DeleteAllItems(string database)
    {
      try
      {
        var deleteFromMaster = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(this.testData.Items.CreateItemsHere.Path)
          .AddScope(ScopeType.Children)
          .Database(database)
          .Build();
        return await this.session.DeleteItemAsync(deleteFromMaster);
      }
      catch (Exception ex)
      {
        string message = "Error removing items : " + ex;
        Debug.WriteLine(message);

        return null;
      }
    }
  }
}