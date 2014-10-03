namespace MobileSDKIntegrationTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API.Items;

  [TestFixture]
  public class ReadPagedItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiReadonlySession session;

    [SetUp]
    public void SetUp()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();

      this.session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
        .Credentials(this.testData.Users.Admin)
        .DefaultDatabase("master")
        .DefaultLanguage("en")
        .BuildReadonlySession();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;

      this.session.Dispose();
      this.session = null;
    }

    [Test]
    public async void TestGetPage0WithSize3ByQuery()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/content/Home/*")
        .AddScope(ScopeType.Self)
        .PageNumber(0)
        .ItemsPerPage(3)
        .Build();

      ScItemsResponse response = await this.session.ReadItemAsync(request);

      Assert.IsNotNull(response);
      Assert.AreEqual(3, response.ResultCount);
      Assert.AreEqual(4, response.TotalCount);
    }


    [Test]
    public async void TestGetPage2WithSize3ByItemIdWithChildrenScope()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.MediaImagesItem.Id)
        .AddScope(ScopeType.Children)
        .PageNumber(2)
        .ItemsPerPage(3)
        .Build();

      ScItemsResponse response = await this.session.ReadItemAsync(request);

      Assert.IsNotNull(response);
      Assert.AreEqual(2, response.ResultCount);
      Assert.AreEqual(8, response.TotalCount);
    }

    [Test]
    public async void TestOutOfRangeRequestReturnsEmptyDataset()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path)
        .AddScope(ScopeType.Children)
        .PageNumber(10)
        .ItemsPerPage(5)
        .Build();

      ScItemsResponse response = await this.session.ReadItemAsync(request);

      Assert.IsNotNull(response);
      Assert.AreEqual(0, response.ResultCount);
      Assert.AreEqual(4, response.TotalCount);
    }

    [Test]
    public void TestGetItemByPathWithNegativePageNumberThrowsArgumentException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path).PageNumber(-1));
      Assert.AreEqual("ReadItemByPathRequestBuilder.PageNumber : The input cannot be negative.", exception.Message);
    }

    [Test]
    public void TestGetItemByQueryWithPageNumberAssignedTwiceThrowsInvalidOperationException()
    {
      var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(this.testData.Items.Home.Path).PageNumber(1).ItemsPerPage(2).PageNumber(2));
      Assert.AreEqual("ReadItemByQueryRequestBuilder.PageNumber : Property cannot be assigned twice.", exception.Message);
    }


    [Test]
    public void TestGetItemByIdWithZeroItemsPerPageCountThrowsArgumentException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.Home.Id).PageNumber(10).ItemsPerPage(0));
      Assert.AreEqual("ReadItemByIdRequestBuilder.ItemsPerPage : The input should be > 0.", exception.Message);
    }

    [Test]
    public void TestGetItemByQueryWithNegativeCountOfItemsPerPageThrowsArgumentException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(this.testData.Items.Home.Path).PageNumber(10).ItemsPerPage(-1));
      Assert.AreEqual("ReadItemByQueryRequestBuilder.ItemsPerPage : The input should be > 0.", exception.Message);
    }
  }
}

