namespace MobileSDKIntegrationTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class GetItemsWithScopeTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiReadonlySession session;

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
        .Credentials(this.testData.Users.Admin)
        .BuildReadonlySession();
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
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/Home/Allowed_Parent")
        .Payload(PayloadType.Full)
        .AddScope(ScopeType.Parent)
        .Build();
      var response = await this.session.ReadItemAsync(request);
      testData.AssertItemsCount(1, response);
      var resultItem = response[0];

      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      Assert.AreEqual("The Home item is the default starting point for a website.", resultItem.FieldWithName("__Long description").RawValue);
    }

    [Test]
    public async void TestGetItemWithSelfScopeByQuery()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(this.testData.Items.Home.Path)
        .AddScope(ScopeType.Self)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      var resultItem = response[0];

      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      Assert.AreEqual(2, resultItem.FieldsCount);
      Assert.AreEqual("Sitecore", resultItem.FieldWithName("Title").RawValue);
    }

    [Test]
    public async void TestGetItemWithChildrenScopeByQuery()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(this.testData.Items.AllowedItem.Path)
         .AddScope(ScopeType.Children)
         .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(2, response);
      Assert.AreEqual("Allowed_Child", response[0].DisplayName);
      Assert.AreEqual("Not_Allowed_Child", response[1].DisplayName);
    }

    [Test]
    public async void TestGetItemWithChildrenAndParentScopeById()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.AllowedItem.Id)
        .AddScope(ScopeType.Children, ScopeType.Parent)
        .Database("master")
        .AddFields("title")
        .Payload(PayloadType.Full)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(3, response);
      Assert.AreEqual("Allowed_Child", response[0].DisplayName);
      testData.AssertItemsAreEqual(this.testData.Items.AllowedParent, response[2]);
    }

    [Test]
    public async void TestGetItemWitchHasNotChildrenWithChildrenScopeByPath()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.TestFieldsItem.Path)
        .AddScope(ScopeType.Children)
        .Build();
      var response = await this.session.ReadItemAsync(request);
      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemWWithSelfScopeByNotExistentPath()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/not existent/path")
        .AddScope(ScopeType.Children)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemWithChildrenAndSelfAndParentScopeByQuery()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home/Allowed_Parent/Allowed_Item/ancestor-or-self::*")
        .AddScope(ScopeType.Children, ScopeType.Self, ScopeType.Parent)
        .Language("en")
        .Payload(PayloadType.Content)
        .Build();
      var response = await this.session.ReadItemAsync(request);
      testData.AssertItemsCount(28, response);
      Assert.AreEqual("Allowed_Child", response[0].DisplayName);
      Assert.AreEqual("Not_Allowed_Child", response[1].DisplayName);
      Assert.AreEqual("Allowed_Item", response[2].DisplayName);
      Assert.AreEqual("Allowed_Parent", response[3].DisplayName);
    }

    [Test]
    public async void TestGetItemWithChildrenScopeByQueryWithSpecifiedFields()        //children in name
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home/descendant::*[@title='Allowed_Item']")
        .AddScope(ScopeType.Children)
        .Language("en")
        .AddScope(ScopeType.Self)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(6, response);
      Assert.AreEqual("Allowed_Child", response[0].DisplayName);
      Assert.AreEqual("Not_Allowed_Child", response[1].DisplayName);
      Assert.AreEqual("Allowed_Item", response[2].DisplayName);
    }

    [Test]
    public async void TestGetNotAllowedItemWithChildrenScopeById()
    {
      var sessionWithNoReadAccessUser = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
        .Credentials(this.testData.Users.NoReadAccess)
        .BuildReadonlySession();

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.Home.Id)
        .AddScope(ScopeType.Children)
        .Build();
      var response = await sessionWithNoReadAccessUser.ReadItemAsync(request);

      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemWitParentAndSelfScopeWhenParentItemIsNotAllowedByPath()
    {
      var sessionWithNoReadAccessUser = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
        .Credentials(this.testData.Users.FakeAnonymous)
        .BuildReadonlySession();


      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/Home/Not_Allowed_Parent/Allowed_Item")
        .AddScope(ScopeType.Parent)
        .AddScope(ScopeType.Self)
        .Build();
      var response = await sessionWithNoReadAccessUser.ReadItemAsync(request);

      testData.AssertItemsCount(1, response);
      Assert.AreEqual("Allowed_Item", response[0].DisplayName);
    }

    [Test]
    public void TestGetItemByPathDuplicateScopeParamsReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path)
        .AddScope(ScopeType.Parent)
        .AddScope(ScopeType.Children)
        .AddScope(ScopeType.Parent)
        .Build());
      Assert.AreEqual("ReadItemByPathRequestBuilder.Scope : Adding scope parameter duplicates is forbidden", exception.Message);
    }

    [Test]
    public void TestGetItemByIdDuplicateScopeParamsReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id)
        .AddScope(ScopeType.Self, ScopeType.Self)
        .Build());
      Assert.AreEqual("ReadItemByIdRequestBuilder.Scope : Adding scope parameter duplicates is forbidden", exception.Message);
    }

    [Test]
    public void TestGetItemByQueryDuplicateScopeParamsReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Path)
        .AddScope(ScopeType.Children, ScopeType.Self, ScopeType.Children)
        .Build());
      Assert.AreEqual("ReadItemByQueryRequestBuilder.Scope : Adding scope parameter duplicates is forbidden", exception.Message);
    }
  }
}
