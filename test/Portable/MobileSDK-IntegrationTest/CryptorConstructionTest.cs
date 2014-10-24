namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;
  using Sitecore.MobileSDK.MockObjects;
  using Sitecore.MobileSDK.SessionSettings;

  [TestFixture]
  public class CryptorConstructionTest
  {
    private ScTestApiSession anonymousSession;
    private ScTestApiSession authenticatedSession;
    private TestEnvironment testData;
    private MediaLibrarySettings mediaSettings;



    [SetUp]
    public void SetUp()
    {
      this.mediaSettings = new MediaLibrarySettings(
        "/sitecore/media library",
        "ashx",
        "~/media/");

      this.testData = TestEnvironment.DefaultTestEnvironment();

      var config = new SessionConfig(testData.InstanceUrl);
      this.anonymousSession = new ScTestApiSession(config, null, this.mediaSettings, LegacyConstants.DefaultSource());

      config = new SessionConfig(testData.InstanceUrl);
      this.authenticatedSession = new ScTestApiSession(config, testData.Users.Admin, this.mediaSettings, LegacyConstants.DefaultSource());
    }

    [TearDown]
    public void TearDown()
    {
      this.anonymousSession.Dispose();
      this.anonymousSession = null;
      this.authenticatedSession.Dispose();
      this.authenticatedSession = null;
      this.mediaSettings = null;
      this.testData = null;
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

