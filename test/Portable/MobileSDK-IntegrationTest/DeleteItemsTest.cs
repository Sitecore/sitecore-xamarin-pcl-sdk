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
  public class DeleteItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreSSCSession session;
    private ISitecoreSSCSession noThrowCleanupSession;
    private const string SampleId = "{SAMPLEID-7808-4798-A461-1FB3EB0A43E5}";
    /*
    [TestFixtureSetUp]
    public async void TestFixtureSetup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultDatabase("master")
        .BuildSession();

      // @adk : must not throw
      await this.DeleteAllItems("master");
      await this.DeleteAllItems("web");
    }
     */


    private ISitecoreSSCSession CreateSession()
    {
      return SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultDatabase("master")
        .BuildSession();
    }


    private async Task RemoveAll()
    {
      await this.DeleteAllItems("master");
      await this.DeleteAllItems("web");
    }

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = this.CreateSession();

      // Same as this.session
      var cleanupSession = this.CreateSession();
      this.noThrowCleanupSession = new NoThrowSSCSession(cleanupSession);
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
    public async void TestDeleteItemByIdWithDb()
    {
      await this.RemoveAll();

      const string Db = "web";
      var itemSession = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .Site(testData.ShellSite)
        .DefaultDatabase(Db)
        .BuildSession();
      ISitecoreItem item = await this.CreateItem("Item in web", null, itemSession);

      var request = ItemSSCRequestBuilder.DeleteItemRequestWithId(item.Id)
        .Database(Db)
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.IsTrue(result.Deleted);
    }

    [Test]
    public async void TestDeleteItemByIdWithParentScope()
    {
      await this.RemoveAll();

      ISitecoreItem parentItem = await this.CreateItem("Parent item");
      ISitecoreItem childItem = await this.CreateItem("Child item", parentItem);

      var request = ItemSSCRequestBuilder.DeleteItemRequestWithId(childItem.Id)
        .Build();

      var result = await this.session.DeleteItemAsync(request);
      Assert.IsTrue(result.Deleted);
    }

    [Test]
    public async void TestDeleteItemByIdAsAnonymousFromShellSiteReturnsException()
    {
      await this.RemoveAll();

      var anonymousSession = SitecoreSSCSessionBuilder.AnonymousSessionWithHost(testData.InstanceUrl)
        .DefaultDatabase("master")
        .Site(testData.ShellSite)
        .BuildSession();

      ISitecoreItem item = await this.CreateItem("Item to delete as anonymous");

      var request = ItemSSCRequestBuilder.DeleteItemRequestWithId(item.Id)
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = anonymousSession.DeleteItemAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Data from the internet has unexpected format", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.SSCJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.AreEqual("Access to site is not granted.", exception.InnerException.Message);

      await session.DeleteItemAsync(request);
    }

    [Test]
    public async void TestDeleteItemByIdWithoutDeleteAccessReturnsException()
    {
      await this.RemoveAll();

      var noAccessSession = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.NoCreateAccess)
        .DefaultDatabase("master")
        .Site(testData.ShellSite)
        .BuildSession();

      ISitecoreItem item = await this.CreateItem("Item to delete without delete access");

      var request = ItemSSCRequestBuilder.DeleteItemRequestWithId(item.Id)
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = noAccessSession.DeleteItemAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);
      Assert.AreEqual("[Sitecore Mobile SDK] Data from the internet has unexpected format", exception.Message);
      Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.SSCJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("DeleteItem - Delete right required"));

      await session.DeleteItemAsync(request);
    }

    [Test]
    public async void TestDeleteItemByNotExistentId()
    {
      await this.RemoveAll();

      var request = ItemSSCRequestBuilder.DeleteItemRequestWithId(SampleId).Build();


      var response = await session.DeleteItemAsync(request);
      Assert.IsFalse(response.Deleted);
    }

    [Test]
    public void TestDeleteItemByInvalidIdReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.DeleteItemRequestWithId("invalid id")
        .Build());
      Assert.AreEqual("DeleteItemByIdRequestBuilder.ItemId : Item id must have curly braces '{}'", exception.Message);
    }

  

    [Test]
    public void TestDeleteItemByPathWithNullDatabaseDoNotReturnsException()
    {
      var request = ItemSSCRequestBuilder.DeleteItemRequestWithId(SampleId)
        .Database(null)
        .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestDeleteItemByIdWithEmptyDatabaseDoNotReturnsException()
    {
      var request = ItemSSCRequestBuilder.DeleteItemRequestWithId(SampleId)
        .Database("")
        .Build();
      Assert.IsNotNull(request);
    }

    [Test]
    public void TestDeleteItemByEmptyIdReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.DeleteItemRequestWithId(""));
      Assert.AreEqual("DeleteItemByIdRequestBuilder.ItemId : The input cannot be empty.", exception.Message);
    }

    private async Task<ISitecoreItem> CreateItem(string itemName, ISitecoreItem parentItem = null, ISitecoreSSCSession itemSession = null)
    {
      if (itemSession == null)
      {
        itemSession = this.session;
      }
      string parentPath = (parentItem == null) ? this.testData.Items.CreateItemsHere.Path : parentItem.Path;
      var request = ItemSSCRequestBuilder.CreateItemRequestWithParentPath(parentPath)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName(itemName)
        .Build();
      var createResponse = await itemSession.CreateItemAsync(request);

     Assert.IsTrue(createResponse.Created);

      var readRequest = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.CreateItemsHere.Path + "/" + itemName)
                                         .Build();

      var readResponse = await itemSession.ReadItemAsync(readRequest);

      return readResponse[0];
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