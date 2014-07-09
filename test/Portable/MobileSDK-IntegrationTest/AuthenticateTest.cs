namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.SessionSettings;

  [TestFixture]
  public class AuthnticateTest
  {
    private ScApiSession sessionWithValidConfig;
    private ScApiSession sessionWithWrongUsernameConfig;
    private ScApiSession sessionWithWebsiteUserToShellSiteConfig;

    [SetUp]
    public void Setup()
    {
      var testData = TestEnvironment.DefaultTestEnvironment();

      var instanceUrl = testData.InstanceUrl;
      var username = testData.Users.Admin.Username;
      var password = testData.Users.Admin.Password;

      var validConfig = new SessionConfig(instanceUrl, username, password);
      this.sessionWithValidConfig = new ScApiSession(validConfig, null);

      var wrongUsernameConfig = new SessionConfig(instanceUrl, username + "wrong", password);
      this.sessionWithWrongUsernameConfig = new ScApiSession(wrongUsernameConfig, null);


      username = testData.Users.Creatorex.Username;
      password = testData.Users.Creatorex.Password;
      var site = testData.ShellSite;

      var wrongSiteConfig = new SessionConfig(instanceUrl, username, password, site);
      this.sessionWithWebsiteUserToShellSiteConfig = new ScApiSession(wrongSiteConfig, null);
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionWithValidConfig = null;
      this.sessionWithWrongUsernameConfig = null;
      this.sessionWithWebsiteUserToShellSiteConfig = null;
    }

    [Test]
    public async void TestCheckValidCredentials()
    {
      bool response = await this.sessionWithValidConfig.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestCheckWrongCredentials()
    {
      bool response = await this.sessionWithWrongUsernameConfig.AuthenticateAsync();
      Assert.False(response);
    }

    //TODO: This testcase should fail due to WebApi bug.
    [Test]
    public async void TestWrongSiteAuthenticate()
    {
      bool response = await this.sessionWithWebsiteUserToShellSiteConfig.AuthenticateAsync();
      Assert.False(response);
    }

  }
}