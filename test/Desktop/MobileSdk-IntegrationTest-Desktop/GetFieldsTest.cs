namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  [TestFixture]
  public class GetFieldsTest
  {
    private TestEnvironment testData;
    private ScApiSession sessionAuthenticatedUser;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      this.sessionAuthenticatedUser = new ScApiSession(config, ItemSource.DefaultSource());
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionAuthenticatedUser = null;
      this.testData = null;
    }

    [Test]
    public async void TestGetItemById()
    {
      var response = await GetItemById(this.testData.Items.Home.Id);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
    }
  }
}