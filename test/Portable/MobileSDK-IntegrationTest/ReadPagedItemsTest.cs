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
    private TestEnvironment env;
    private ISitecoreWebApiReadonlySession session;

    [SetUp]
    public void SetUp()
    {
      this.env = TestEnvironment.DefaultTestEnvironment();

      this.session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.env.InstanceUrl)
        .Credentials(this.env.Users.Admin)
        .DefaultDatabase("master")
        .DefaultLanguage("en")
        .BuildReadonlySession();
    }

    [TearDown]
    public void TearDown()
    {
      this.env = null;

      this.session.Dispose();
      this.session = null;
    }

    [Test]
    public async void TestPagingStartsWithZero()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/sitecore/media library/images/*")
        .AddScope(ScopeType.Self)
        .ItemsPerPage(2)
        .PageNumber(0)
        .Build();

      ScItemsResponse response = await this.session.ReadItemAsync(request);

      Assert.IsNotNull(response);
      Assert.AreEqual(2, response.ResultCount);
      Assert.AreEqual(8, response.TotalCount);
    }


    [Test]
    public async void TestValidPagingOptionsWithId()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.env.Items.MediaImagesItem.Id)
        .AddScope(ScopeType.Children)
        .ItemsPerPage(2)
        .PageNumber(3)
        .Build();

      ScItemsResponse response = await this.session.ReadItemAsync(request);

      Assert.IsNotNull(response);
      Assert.AreEqual(2, response.ResultCount);
      Assert.AreEqual(8, response.TotalCount);
    }

    [Test]
    public async void TestOutOfRangeRequestReturnsEmptyDataset()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.env.Items.Home.Path)
        .AddScope(ScopeType.Children)
        .ItemsPerPage(5)
        .PageNumber(10)
        .Build();

      ScItemsResponse response = await this.session.ReadItemAsync(request);

      Assert.IsNotNull(response);
      Assert.AreEqual(0, response.ResultCount);
      Assert.AreEqual(4, response.TotalCount);
    }

    [Test]
    public void TestBuilderThrowsArgumentExceptionForNegativePageNumberInput()
    {
      Assert.Throws<ArgumentException>( ()=>
      {
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.env.Items.Home.Path).PageNumber(-1);
      });
    }

    [Test]
    public void TestBuilderThrowsArgumentExceptionForZeroItemsPerPageCountInput()
    {
      Assert.Throws<ArgumentException>( ()=>
      {
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.env.Items.Home.Path).ItemsPerPage(0);
      });
    }

    [Test]
    public void TestBuilderThrowsArgumentExceptionForNegativeItemsPerPageCountInput()
    {
      Assert.Throws<ArgumentException>( ()=>
      {
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.env.Items.Home.Path).ItemsPerPage(-1);
      });
    }
  }
}

