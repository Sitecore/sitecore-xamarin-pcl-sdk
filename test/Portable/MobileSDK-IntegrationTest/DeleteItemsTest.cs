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

  [TestFixture]
  public class DeleteItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiSession session;

    [TestFixtureSetUp]
    public async void TextFictureSetup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
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
      testData = TestEnvironment.DefaultTestEnvironment();
      session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
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

    [Test]
    public async void TestDeleteItemByPathWithDb()
    {
      const string Db = "web";
      var itemSession = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultDatabase(Db)
        .BuildSession();
      ISitecoreItem item = await this.CreateItem("Item in web", null, itemSession);

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(item.Path)
        .Database(Db)
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.AreEqual(1, result.Count);
      Assert.AreEqual(item.Id, result.ItemsIds[0]);
    }

    [Test]
    public async void TestDeleteItemByIdWithParentScope()
    {

      ISitecoreItem parentItem = await this.CreateItem("Parent item");
      ISitecoreItem childItem = await this.CreateItem("Child item", parentItem);

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(childItem.Path)
        .AddScope(ScopeType.Parent)
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.AreEqual(1, result.Count);
      Assert.AreEqual(parentItem.Id, result.ItemsIds[0]);
    }

    [Test]
    public async void TestDeleteInternationalItemWithSpacesInNameByQuery()
    {

      ISitecoreItem item1 = await this.CreateItem("International בינלאומי");
      ISitecoreItem item2 = await this.CreateItem("インターナショナル عالمي");

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(testData.Items.CreateItemsHere.Path + "/*")
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.AreEqual(2, result.Count);
      Assert.AreEqual(item1.Id, result.ItemsIds[0]);
      Assert.AreEqual(item2.Id, result.ItemsIds[1]);
    }

    [Test]
    public async void TestDeleteItemByIdbWithParentAndChildrenScope()
    {

      ISitecoreItem parentItem = await this.CreateItem("Parent item");
      ISitecoreItem selfItem = await this.CreateItem("Self item", parentItem);
      ISitecoreItem childItem = await this.CreateItem("Child item", selfItem);

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(selfItem.Path)
        .AddScope(ScopeType.Parent)
        .AddScope(ScopeType.Children)
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.AreEqual(2, result.Count);
      Assert.AreEqual(parentItem.Id, result.ItemsIds[0]);
      Assert.AreEqual(childItem.Id, result.ItemsIds[1]);
    }

    [Test]
    public async void TestDeleteInternationalItemByPathbWithChildrenScope()
    {

      ISitecoreItem selfItem = await this.CreateItem("インターナショナル عالمي JJ ж");
      ISitecoreItem childItem = await this.CreateItem("インターナショナル", selfItem);

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(selfItem.Path)
        .AddScope(ScopeType.Children)
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.AreEqual(1, result.Count);
      Assert.AreEqual(childItem.Id, result.ItemsIds[0]);
    }

    [Test]
    public async void TestDeleteItemByQueryWithChildrenAndSelfScope()
    {

      ISitecoreItem parentItem = await this.CreateItem("Parent item");
      ISitecoreItem selfItem = await this.CreateItem("Self item", parentItem);
      await this.CreateItem("Child item", selfItem);

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(testData.Items.CreateItemsHere.Path + "/descendant::*[@@templatename='Sample Item']")
        .AddScope(ScopeType.Children)
        .AddScope(ScopeType.Self)
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.AreEqual(5, result.Count);  //but 3 items was deleted in fact (Item Web API issue)

    }

    [Test]
    public async void TestDeleteItemByIdAsAnonymousFromShellSiteReturnsException()
    {
      var anonymousSession = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(testData.InstanceUrl)
        .DefaultDatabase("master")
        .Site(testData.ShellSite)
        .BuildSession();

      ISitecoreItem item = await this.CreateItem("Item to delete as anonymous");

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(item.Path)
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = anonymousSession.DeleteItemAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Unable to download data from the internet", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("Access to site is not granted.", exception.InnerException.Message);

      await session.DeleteItemAsync(request);
    }

    [Test]
    public async void TestDeleteItemByPathWithoutDeleteAccessReturnsException()
    {
      var noAccessSession = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.NoCreateAccess)
        .DefaultDatabase("master")
        .Site(testData.ShellSite)
        .BuildSession();

      ISitecoreItem item = await this.CreateItem("Item to delete without delete access");

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(item.Path)
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = noAccessSession.DeleteItemAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Unable to download data from the internet", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("DeleteItem - Delete right required"));

      await session.DeleteItemAsync(request);
    }

    [Test]
    public  void TestDeleteItemByIdWithDuplicateScopeReturnsException()
    {
      var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithId("{SAMPLEID-7808-4798-A461-1FB3EB0A43E5}")
        .AddScope(ScopeType.Self)
        .AddScope(ScopeType.Self)
        .Build());
      Assert.AreEqual("Adding scope parameter duplicates is forbidden", exception.Message);

    }

    [Test]
    public void TestDeleteItemWithInvalidPathReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithPath("invalid path )").Build());
      Assert.AreEqual("Item path should begin with '/'", exception.Message);

    }

    [Test]
    public void TestDeleteItemByQueryWithNullScopeReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery("sample query")
        .AddScope(null)
        .Build());
      Assert.AreEqual("DeleteItemItemByQueryRequestBuilder.Scope: The input cannot be null or empty", exception.Message);

    }

    [Test]
    public void TestDeleteItemByQueryWithTwoDatabasesReturnsException()
    {
      var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery("sample query")
        .Database("1")
        .Database("2")
        .Build());
      Assert.AreEqual("DeleteItemItemByQueryRequestBuilder.Database : The input cannot be set twice", exception.Message);

    }

    [Test]
    public void TestDeleteItemByPathWithNullDatabaseReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithPath("/sample path")
        .Database(null)
        .Build());
      Assert.AreEqual("DeleteItemItemByPathRequestBuilder.Database: The input cannot be null or empty", exception.Message);

    }

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

    private async Task DeleteAllItems(string database)
    {
      var deleteFromMaster = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(this.testData.Items.CreateItemsHere.Path)
        .AddScope(ScopeType.Children)
        .Database(database)
        .Build();
      await this.session.DeleteItemAsync(deleteFromMaster);
    }
  }
}