namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Session;

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
  }
}