namespace MobileSDKIntegrationTest
{
  using System;
  using System.Diagnostics;
  using System.Linq;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;

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
    private ISitecoreWebApiSession noThrowCleanupSession;
    private const string SampleId = "{SAMPLEID-7808-4798-A461-1FB3EB0A43E5}";
    /*
    [TestFixtureSetUp]
    public async void TestFixtureSetup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultDatabase("master")
        .BuildSession();

      // @adk : must not throw
      await this.DeleteAllItems("master");
      await this.DeleteAllItems("web");
    }
     */


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

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = this.CreateSession();

      // Same as this.session
      var cleanupSession = this.CreateSession();
      this.noThrowCleanupSession = new NoThrowWebApiSession(cleanupSession);
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
    public async void TestDeleteItemByPathWithDb()
    {
      await this.RemoveAll();

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
      Assert.AreEqual(item.Id, result[0]);
    }

    [Test]
    public async void TestDeleteItemByIdWithParentScope()
    {
      await this.RemoveAll();

      ISitecoreItem parentItem = await this.CreateItem("Parent item");
      ISitecoreItem childItem = await this.CreateItem("Child item", parentItem);

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(childItem.Path)
        .AddScope(ScopeType.Parent)
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.AreEqual(1, result.Count);
      Assert.AreEqual(parentItem.Id, result[0]);
    }

    [Test]
    public async void TestDeleteInternationalItemWithSpacesInNameByQuery()
    {
      await this.RemoveAll();

      ISitecoreItem item1 = await this.CreateItem("International בינלאומי");
      ISitecoreItem item2 = await this.CreateItem("インターナショナル عالمي");

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(testData.Items.CreateItemsHere.Path + "/*")
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.AreEqual(2, result.Count);
      Assert.AreEqual(item1.Id, result[0]);
      Assert.AreEqual(item2.Id, result[1]);
    }

    [Test]
    public async void TestDeleteItemByIdbWithParentAndChildrenScope()
    {
      await this.RemoveAll();

      ISitecoreItem parentItem = await this.CreateItem("Parent item");
      ISitecoreItem selfItem = await this.CreateItem("Self item", parentItem);
      ISitecoreItem childItem = await this.CreateItem("Child item", selfItem);

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(selfItem.Path)
        .AddScope(ScopeType.Parent)
        .AddScope(ScopeType.Children)
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.AreEqual(2, result.Count);
      Assert.AreEqual(parentItem.Id, result[0]);
      Assert.AreEqual(childItem.Id, result[1]);
    }

    [Test]
    public async void TestDeleteInternationalItemByPathbWithChildrenScope()
    {
      await this.RemoveAll();

      ISitecoreItem selfItem = await this.CreateItem("インターナショナル عالمي JJ ж");
      ISitecoreItem childItem = await this.CreateItem("インターナショナル", selfItem);

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(selfItem.Path)
        .AddScope(ScopeType.Children)
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.AreEqual(1, result.Count);
      Assert.AreEqual(childItem.Id, result[0]);
    }

    [Test]
    public async void TestDeleteItemByQueryWithChildrenAndSelfScope()
    {
      await this.RemoveAll();

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
      await this.RemoveAll();

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
      await this.RemoveAll();

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
    public async void TestDeleteItemByNotExistentPath()
    {
      await this.RemoveAll();

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath(testData.Items.CreateItemsHere.Path + "/not existent item")
        .Build();

      var response = await session.DeleteItemAsync(request);
      Assert.AreEqual(0, response.Count);
    }

    [Test]
    public async void TestDeleteItemByNotExistentId()
    {
      await this.RemoveAll();

      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithId(SampleId).Build();


      var response = await session.DeleteItemAsync(request);
      Assert.AreEqual(0, response.Count);
    }

    [Test]
    public void TestDeleteItemByInvalidIdReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithId("invalid id")
        .Build());
      Assert.AreEqual("DeleteItemByIdRequestBuilder.ItemId : Item id must have curly braces '{}'", exception.Message);
    }

    [Test]
    public void TestDeleteItemByIdWithDuplicateScopeReturnsException()
    {
      var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithId(SampleId)
        .AddScope(ScopeType.Self)
        .AddScope(ScopeType.Self));
      Assert.AreEqual("DeleteItemByIdRequestBuilder.Scope : Adding scope parameter duplicates is forbidden", exception.Message);
    }

    [Test]
    public void TestDeleteItemWithInvalidPathReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithPath("invalid path )"));
      Assert.AreEqual("DeleteItemItemByPathRequestBuilder.ItemPath : should begin with '/'", exception.Message);
    }

    [Test]
    public void TestDeleteItemByQueryWithNullScopeReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery("sample query")
        .AddScope(null));
      Assert.IsTrue(exception.Message.Contains("DeleteItemItemByQueryRequestBuilder.Scope"));
    }

    [Test]
    public void TestDeleteItemByQueryWithTwoDatabasesReturnsException()
    {
      var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery("sample query")
        .Database("1")
        .Database("2"));
      Assert.AreEqual("DeleteItemItemByQueryRequestBuilder.Database : Property cannot be assigned twice.", exception.Message);
    }

    [Test]
    public void TestDeleteItemByPathWithNullDatabaseDoNotReturnsException()
    {
      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath("/sample path")
        .Database(null)
        .Build();
      Assert.IsNotNull (request);
    }

    [Test]
    public void TestDeleteItemByIdWithEmptyDatabaseDoNotReturnsException()
    {
      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithId(SampleId)
        .Database("")
        .Build();
      Assert.IsNotNull (request);
    }

    [Test]
    public void TestDeleteItemByQueryWithSpacesOnlyInDatabaseReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery("/sample query")
        .Database("  "));
      Assert.AreEqual("DeleteItemItemByQueryRequestBuilder.Database : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestDeleteItemByEmptyIdReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithId(""));
      Assert.AreEqual("DeleteItemByIdRequestBuilder.ItemId : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestDeleteItemByNullQueryReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(null));
      Assert.IsTrue(exception.Message.Contains("DeleteItemItemByQueryRequestBuilder.SitecoreQuery"));
    }

    [Test]
    public void TestDeleteItemByPathWithSpacesOnlyReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DeleteItemRequestWithPath(" "));
      Assert.AreEqual("DeleteItemItemByPathRequestBuilder.ItemPath : The input cannot be empty.", exception.Message);
    }

    [Test]
    public async void TestCreateGetAndDelete101ItemsByQuery()
    {
      await this.RemoveAll();

      for (int i = 0; i < 101; i++)
      {
        await this.CreateItem("Test item " + (i + 1));
      }

      var query = testData.Items.CreateItemsHere.Path + "/descendant::*[@@templatename='Sample Item']";

      var readRequest = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(query).PageNumber(0).ItemsPerPage(101).Build();
      var readResult = await this.session.ReadItemAsync(readRequest);
      testData.AssertItemsCount(100, readResult);

      var deleteRequest = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery(query).Build();
      var deleteResult = await this.session.DeleteItemAsync(deleteRequest);
      Assert.AreEqual(101, deleteResult.Count);
    }

    private async Task<ISitecoreItem> CreateItem(string itemName, ISitecoreItem parentItem = null, ISitecoreWebApiSession itemSession = null)
    {
      if (itemSession == null)
      {
        itemSession = this.session;
      }
      string parentPath = (parentItem == null) ? this.testData.Items.CreateItemsHere.Path : parentItem.Path;
      var request = ItemWebApiRequestBuilder.CreateItemRequestWithParentPath(parentPath)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName(itemName)
        .Build();
      var createResponse = await itemSession.CreateItemAsync(request);

      Assert.AreEqual(1, createResponse.ResultCount);
      return createResponse[0];
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