using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;

namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;

  [TestFixture]
  public class CryptorConstructionTest
  {
    private ScTestApiSession anonymousSession;
    private ScTestApiSession authenticatedSession;
    private TestEnvironment testData;
    [SetUp]
    public void SetUp()
    {
      testData = TestEnvironment.DefaultTestEnvironment();

      var config = SessionConfig.NewAuthenticatedSessionConfig(testData.InstanceUrl, testData.Users.Anonymous.UserName, testData.Users.Anonymous.Password);
      this.anonymousSession = new ScTestApiSession(config, ItemSource.DefaultSource());

      config = SessionConfig.NewAuthenticatedSessionConfig(testData.InstanceUrl, testData.Users.Admin.UserName, testData.Users.Admin.Password);
      this.authenticatedSession = new ScTestApiSession(config, ItemSource.DefaultSource());
    }

    [TearDown]
    public void TearDown()
    {
      this.anonymousSession = null;
      this.authenticatedSession = null;
    }

    [Test]
    public async void TestAnonymousSessionDoesNotFetchPublicKey()
    {
      var cryptor = await this.anonymousSession.GetCredentialsCryptorAsyncPublic();
      Assert.NotNull(cryptor);

      Assert.AreEqual(0, this.anonymousSession.GetPublicKeyInvocationsCount);
    }


    [Test]
    public async void TestAuthenticatedSessionDownloadsPublicKey()
    {
      var cryptor = await this.authenticatedSession.GetCredentialsCryptorAsyncPublic();
      Assert.NotNull(cryptor);

      Assert.AreEqual(1, this.authenticatedSession.GetPublicKeyInvocationsCount);
    }
  }
}

