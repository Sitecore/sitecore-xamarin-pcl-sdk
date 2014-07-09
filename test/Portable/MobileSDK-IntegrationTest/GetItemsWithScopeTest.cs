namespace MobileSDKIntegrationTest
{
  using System;
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
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/Home/Allowed_Parent")
        .Payload(PayloadType.Full)
        .AddScope(ScopeType.Parent)
        .Build();
      var response = await this.session.ReadItemAsync(request);
      testData.AssertItemsCount(1, response);
      var resultItem = response.Items[0];

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
      var resultItem = response.Items[0];

      testData.AssertItemsAreEqual(testData.Items.Home, resultItem);
      Assert.AreEqual(2, resultItem.Fields.Count);
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
      Assert.AreEqual("Allowed_Child", response.Items[0].DisplayName);
      Assert.AreEqual("Not_Allowed_Child", response.Items[1].DisplayName);
    }

    [Test]
    public async void TestGetItemWithChildrenAndParentScopeById()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.AllowedItem.Id)
        .AddScope(ScopeType.Children)
        .Database("master")
        .AddFields("title")
        .AddScope(ScopeType.Parent)
        .Payload(PayloadType.Full)
        .Build();
      var response = await this.session.ReadItemAsync(request);

      testData.AssertItemsCount(3, response);
      Assert.AreEqual("Allowed_Child", response.Items[0].DisplayName);
      testData.AssertItemsAreEqual(this.testData.Items.AllowedItem, response.Items[2]);
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
        .AddScope(ScopeType.Children)
        .AddScope(ScopeType.Self)
        .Language("en")
        .AddScope(ScopeType.Parent)
        .Payload(PayloadType.Content)
        .Build();
      var response = await this.session.ReadItemAsync(request);
      //TODO: check with Item Web API request directly
      testData.AssertItemsCount(28, response);
      Assert.AreEqual("Allowed_Child", response.Items[0].DisplayName);
      Assert.AreEqual("Not_Allowed_Child", response.Items[1].DisplayName);
      Assert.AreEqual("Allowed_Item", response.Items[2].DisplayName);
    }

    [Test]
    public async void TestGetItemWithSelfScopeSeveralTimesById()
    {
      try
      {
        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.ItemWithVersions.Id)
          // .AddScope(ScopeType.Self)
          .Language("en")
          // .AddScope(ScopeType.Childern)
          // .AddScope(ScopeType.Self)
          .Build();
      }
      //TODO: catch argument exception
      catch (Exception e)
      {

      }
    }

    [Test]
    public async void TestGetItemWithChildrenScopeByQueryWithSpecifiedFields()        //children in name
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home//*[@title='Allowed_Parent']")
       .AddScope(ScopeType.Children)
       .Language("en")
       .AddScope(ScopeType.Self)
       .Build();
      var response = await this.session.ReadItemAsync(request);
      //TODO: check items count and order
      testData.AssertItemsCount(3, response);
      Assert.AreEqual("Allowed_Item", response.Items[0].DisplayName);
      Assert.AreEqual("Not_Allowed_Item", response.Items[1].DisplayName);
      Assert.AreEqual("Allowed_Parent", response.Items[2].DisplayName); 
    }

    [Test]
    public async void TestGetNotAllowedItemWithChildrenScopeById()
    {
      var sessionWithNoReadAccessUser = testData.GetSession(testData.InstanceUrl, testData.Users.NoReadAccess.Username, testData.Users.NoReadAccess.Password);
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.Home.Id)
        .AddScope(ScopeType.Children)
        .Build();
      var response = await sessionWithNoReadAccessUser.ReadItemAsync(request);

      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemWitParentAndSelfScopeWhenParentItemIsNotAllowedByPath()
    {
      var sessionWithNoReadAccessUser = testData.GetSession(testData.InstanceUrl, testData.Users.Anonymous.Username, testData.Users.Anonymous.Password);
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/Home/Not_Allowed_Parent/Allowed_Item")
       .AddScope(ScopeType.Parent)
       .AddScope(ScopeType.Self)
       .Build();
      var response = await sessionWithNoReadAccessUser.ReadItemAsync(request);

     testData.AssertItemsCount(1, response);
      Assert.AreEqual("Allowed_Item", response.Items[0].DisplayName);
    }
  }
}
