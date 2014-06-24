
namespace Sitecore.MobileSdkUnitTest
{
  using NUnit.Framework;

  using System;
  using System.Diagnostics;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;



  [TestFixture]
  public class ItemSourceTest
  {
    [Test]
    public void TestApiSessionConstructorDoesNotRequiresDefaultSource()
    {
      SessionConfig config = new SessionConfig ("localhost", "alex.fergusson", "man u is a champion");

      ScApiSession result = new ScApiSession (config, null);
      Assert.IsNotNull(result);
    }


    [Test]
    public void TestApiSessionConstructorRequiresConfig()
    {
      ItemSource defaultSource = ItemSource.DefaultSource ();

      TestDelegate initSessionAction = () =>
      {
        ScApiSession result = new ScApiSession (null, defaultSource);
        Debug.WriteLine( result );
      };

      Assert.Throws<ArgumentNullException>(initSessionAction);
    }

    [Test]
    public void TestItemSourceDatabaseIsOptional()
    {
      var result = new ItemSource (null, "en", "1");

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

