

namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;

  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.Items;


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

      var config = SessionConfig.NewAuthenticatedSessionConfig(testData.InstanceUrl, testData.Users.Anonymous.Username, testData.Users.Anonymous.Password);
      this.anonymousSession = new ScTestApiSession(config, config, this.mediaSettings,ItemSource.DefaultSource());

      config = SessionConfig.NewAuthenticatedSessionConfig(testData.InstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      this.authenticatedSession = new ScTestApiSession(config, config, this.mediaSettings,ItemSource.DefaultSource());
    }

    [TearDown]
    public void TearDown()
    {
      this.anonymousSession = null;
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

