


namespace Sitecore.MobileSdkUnitTest
{
  using NUnit.Framework;

  using System;
  using System.Diagnostics;

  using Sitecore.MobileSDK.API;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;



  [TestFixture]
  public class ItemSourceTest
  {
    IMediaLibrarySettings mediaSettings;
//    IWebApiCredentials credentials;


    [SetUp]
    public void SetUp()
    {
      this.mediaSettings = new MediaLibrarySettings(
        "/sitecore/media library",
        "ashx",
        "~/media/");
    }

    [TearDown]
    public void TearDown()
    {
      this.mediaSettings = null;
    }


    [Test]
    public void TestApiSessionConstructorDoesNotRequiresDefaultSource()
    {
      SessionConfig config = SessionConfig.NewAuthenticatedSessionConfig("localhost", "alex.fergusson", "man u is a champion");

      ScApiSession result = new ScApiSession(config, config, this.mediaSettings, null);
      Assert.IsNotNull(result);
    }


    [Test]
    public void TestApiSessionConstructorRequiresConfig()
    {
      ItemSource defaultSource = ItemSource.DefaultSource();

      var config = SessionConfig.NewAuthenticatedSessionConfig("localhost", "alex.fergusson", "man u is a champion");
      var credentials = config;

      TestDelegate initSessionAction = () =>
      {
        ScApiSession result = new ScApiSession(null, credentials, this.mediaSettings, defaultSource);
        Debug.WriteLine( result );
      };

      Assert.Throws<ArgumentNullException>(initSessionAction);
    }

    [Test]
    public void TestItemSourceDatabaseIsOptional()
    {
      var result = new ItemSource(null, "en", "1");

      Assert.IsNotNull(result);
      Assert.IsNull(result.Database);
    }

    [Test]
    public void TestItemSourceLanguageIsOptional()
    {
      var result = new ItemSource("master", null, "1");

      Assert.IsNotNull(result);
      Assert.IsNull(result.Language);
    }

    [Test]
    public void TestItemVersionIsOptionalForItemSource()
    {
      var result = new ItemSource ("core", "da", null);

      Assert.IsNotNull(result);
      Assert.IsNull(result.Version);
    }

    [Test]
    public void TestDefaultSource()
    {
      ItemSource defaultSource = ItemSource.DefaultSource ();

      Assert.AreEqual (defaultSource.Database, "web");
      Assert.AreEqual (defaultSource.Language, "en");

    }
  }
}

