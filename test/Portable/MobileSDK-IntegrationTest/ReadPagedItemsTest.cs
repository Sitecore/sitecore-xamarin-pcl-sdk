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
    public async void TestValidPagingOptions()
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
  }
}

