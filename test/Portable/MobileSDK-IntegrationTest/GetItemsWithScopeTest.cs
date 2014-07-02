namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


  [TestFixture]
  public class GetItemsWithScopeTest
  {
    private TestEnvironment testData;
    private ScApiSession session;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.session = null;
    }

    [Test]
    public async void TestGetItemWithParentScopeByPath()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path)
        .Payload(PayloadType.Full)
        // .AddScope(ScopeType.Parent)
        .Build();
      var response = await this.session.ReadItemAsync(request);
      testData.AssertItemsCount(1, response);
      var resultItem = response.Items[0];

      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      Assert.AreEqual("This section contains all web site content.", resultItem.FieldWithName("__Long description").RawValue);
    }

    [Test]
    public async void TestGetItemWithSelfScopeByQuery()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(this.testData.Items.Home.Path)
        // .AddScope(ScopeType.Self)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response.Items[0];

      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      Assert.AreEqual(2, resultItem.Fields.Count);
      Assert.AreEqual("Sitecore", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithChildrenScopeByQuery()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(this.testData.Items.Home.Path)
        // .AddScope(ScopeType.Children)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(4, response);
      Assert.AreEqual("Android", response.Items[1].DisplayName);
      Assert.AreEqual("Not_Allowed_Parent", response.Items[2].DisplayName);
      Assert.AreEqual(this.testData.Items.Home.Template, response.Items[2].Template);
      Assert.AreEqual(19, response.Items[3].Fields.Count);
    }

    [Test]
    public async void TestGetItemWithChildrenAndParentScopeById()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.Home.Id)
        // .AddScope(ScopeType.Children)
        .Database("master")
        .AddFields("title")
        // .AddScope(ScopeType.Parent)
        .Payload(PayloadType.Full)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(5, response);
      Assert.AreEqual("Test Fields", response.Items[3].DisplayName);
      Assert.AreEqual("Content", response.Items[4].DisplayName);
    }

    [Test]
    public async void TestGetItemWitchHasNotChildrenWithChildrenScopeByPath()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.TestFieldsItem.Path)
        // .AddScope(ScopeType.Children)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemWWithSelfScopeByNotExistentPath()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/not existent/path")
        // .AddScope(ScopeType.Children)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemWithChildrenAndSelfAndParentScopeByQuery()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home/Allowed_Parent")
        // .AddScope(ScopeType.Children)
        // .AddScope(ScopeType.Self)
        .Language("en")
        // .AddScope(ScopeType.Parent)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(4, response);
      Assert.AreEqual("Allowed_Item", response.Items[0].DisplayName);
      Assert.AreEqual("Not_Allowed_Item", response.Items[1].DisplayName);
      Assert.AreEqual("Allowed_Parent", response.Items[2].DisplayName);
      Assert.AreEqual("Home", response.Items[3].DisplayName);
    }

    [Test]
    public async void TestGetItemWithSelfScopeSeveralTimesById()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.ItemWithVersions.Id)
        // .AddScope(ScopeType.Self)
        .Language("en")
        // .AddScope(ScopeType.Self)
        // .AddScope(ScopeType.Self)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      Assert.AreEqual("TestFieldsVersionsAndDB", response.Items[0].DisplayName);
    }

    [Test]
    public async void TestGetItemWithChildrenScopeByQueryWithSpecifiedFields()        //children in name
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home//*[@text='']")
        // .AddScope(ScopeType.Children)
           .Language("en")
        // .AddScope(ScopeType.Self)
           .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(21, response); //self
    }

    [Test]
    public async void TestGetNotAllowedItemWithChildrenScopeById()
    {
      var sessionWithNoReadAccessUser = testData.GetSession(testData.InstanceUrl, testData.Users.NoReadAccess.Username, testData.Users.NoReadAccess.Password,null,"/");
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.Home.Id)
        // .AddScope(ScopeType.Children)
           .Build();
      var response = await sessionWithNoReadAccessUser.ReadItemAsync(request);

      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemWitParentScopeWhenParentItemIsNotAllowedByPath()
    {
      var sessionWithNoReadAccessUser = testData.GetSession(testData.InstanceUrl, testData.Users.NoReadAccess.Username, testData.Users.NoReadAccess.Password,null,"/");
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/Home/Android")
        // .AddScope(ScopeType.Parent)
           .Build();
      var response = await sessionWithNoReadAccessUser.ReadItemAsync(request);

      testData.AssertItemsCount(0, response);
    }
  }
}
