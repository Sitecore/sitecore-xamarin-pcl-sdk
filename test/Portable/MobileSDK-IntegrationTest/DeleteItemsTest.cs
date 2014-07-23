namespace MobileSDKIntegrationTest
{
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
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