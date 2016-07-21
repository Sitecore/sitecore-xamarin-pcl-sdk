namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using System.Diagnostics;
  using NUnit.Framework;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.MediaItem;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.MockObjects;
  using Sitecore.MobileSDK.PasswordProvider.Interface;
  using Sitecore.MobileSDK.SessionSettings;

  [TestFixture]
  public class ItemSourceTest
  {
    IMediaLibrarySettings mediaSettings;
    IWebApiCredentials credentials;
    ISessionConfig localhostConnection;


    [SetUp]
    public void SetUp()
    {
      this.credentials = new SSCCredentialsPOD(
        "alex.fergusson", 
        "man u is a champion");

      this.mediaSettings = new MediaLibrarySettings(
        "/sitecore/media library",
        "ashx",
        "~/media/");

      this.localhostConnection = new SessionConfig("localhost");
    }

    [TearDown]
    public void TearDown()
    {
      this.mediaSettings = null;
      this.credentials = null;
      this.localhostConnection = null;
    }


    [Test]
    public void TestApiSessionConstructorDoesNotRequiresDefaultSource()
    {
      ScApiSession result = new ScApiSession(this.localhostConnection, this.credentials, this.mediaSettings, null);
      Assert.IsNotNull(result);
    }


    [Test]
    public void TestApiSessionConstructorRequiresConfig()
    {
      ItemSource defaultSource = LegacyConstants.DefaultSource();

      TestDelegate initSessionAction = () =>
      {
        ScApiSession result = new ScApiSession(null, this.credentials, this.mediaSettings, defaultSource);
        Debug.WriteLine( result );
      };

      Assert.Throws<ArgumentNullException>(initSessionAction);
    }

    [Test]
    public void TestItemSourceDatabaseIsOptional()
    {
      var result = new ItemSource(null, "en", 1);

      Assert.IsNotNull(result);
      Assert.IsNull(result.Database);
    }

    [Test]
    public void TestItemSourceLanguageIsOptional()
    {
      var result = new ItemSource("master", null, 1);

      Assert.IsNotNull(result);
      Assert.IsNull(result.Language);
    }

    [Test]
    public void TestItemVersionIsOptionalForItemSource()
    {
      var result = new ItemSource ("core", "da", null);

      Assert.IsNotNull(result);
      Assert.IsNull(result.VersionNumber);
    }

    [Test]
    public void TestDefaultSource()
    {
      ItemSource defaultSource = LegacyConstants.DefaultSource();

      Assert.AreEqual (defaultSource.Database, "web");
      Assert.AreEqual (defaultSource.Language, "en");

    }
  }
}

