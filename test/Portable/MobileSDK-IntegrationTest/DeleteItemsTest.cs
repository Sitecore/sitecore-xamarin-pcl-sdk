namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class DeleteItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiSession session;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      session =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("http://mobiledev1ua1.dk.sitecore.net:722")
          .Credentials(testData.Users.Admin)
          .Site(testData.ShellSite)
          .BuildSession();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.session = null;
    }

    [Test]
    public async void TestCorrectDeleteItemWithId()
    {
      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithId("{92EDF93B-75DB-4BF3-99A8-754AE4070122}")
        .Database("master")
        .Build();

      var result = await this.session.DeleteItemAsync(request);

      Assert.AreEqual(1, result.Count);
      Assert.AreEqual("{92EDF93B-75DB-4BF3-99A8-754AE4070122}", result.ItemsIds[0]);
    }

    [Test]
    public async void TestCorrectDeleteItemWithPath()
    {
      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath("/sitecore/content/Home/Android/Folder for deleting/2")
        .Database("master")
        .Build();

      var result = await this.session.DeleteItemAsync(request);

      Assert.AreEqual(1, result.Count);
      Assert.AreEqual("{F0F2D78B-1D58-4B69-8A28-E8C8E1FCFB29}", result.ItemsIds[0]);
    }

    [Test]
    public async void TestCorrectDeleteItemWithSitecoreQuery()
    {
      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithSitecoreQuery("/sitecore/content/Home/Android/Folder for deleting/*")
        .Database("master")
        .Build();

      var result = await this.session.DeleteItemAsync(request);

      Assert.AreEqual(3, result.Count);
      Assert.IsTrue(result.ItemsIds.Contains("{8C982575-8BF5-4894-BB8E-D153E9BA7F0C}"));
      Assert.IsTrue(result.ItemsIds.Contains("{84558B18-2C02-4F0C-BE69-754C467F64E9}"));
      Assert.IsTrue(result.ItemsIds.Contains("{C07000E2-7538-4F30-9D6C-976E09EA7971}"));
    }
  }
}