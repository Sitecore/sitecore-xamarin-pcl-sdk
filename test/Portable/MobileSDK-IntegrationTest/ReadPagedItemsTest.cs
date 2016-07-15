namespace MobileSDKIntegrationTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class ReadPagedItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreSSCReadonlySession session;

    [SetUp]
    public void SetUp()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();

      this.session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
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
    public async void TestGetPage2WithSize3ByItemIdWithChildrenScope()
    {
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithId(this.testData.Items.MediaImagesItem.Id)
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
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path)
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
      var exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path).PageNumber(-1));
      Assert.AreEqual("ReadItemByPathRequestBuilder.PageNumber : The input cannot be negative.", exception.Message);
    }

    [Test]
    public void TestGetItemByIdWithZeroItemsPerPageCountThrowsArgumentException()
    {
      var exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithId(this.testData.Items.Home.Id).PageNumber(10).ItemsPerPage(0));
      Assert.AreEqual("ReadItemByIdRequestBuilder.ItemsPerPage : The input should be > 0.", exception.Message);
    }

  }
}

