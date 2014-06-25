

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;
  using SitecoreMobileSDKMockObjects;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;



  [TestFixture]
  public class ThreadSafetyTest
  {
    [Test]
    public void TestSessionConfigCannotBeMutated()
    {
      var anonymous = new MutableSessionConfig("localhost", null, null);
      var defaultSource = ItemSource.DefaultSource();

      var session = new ScApiSession(anonymous, defaultSource);

      Assert.AreEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);

      anonymous.SetInstanceUrl("sitecore.net");
      anonymous.SetLogin("admin");
      anonymous.SetPassword("b");

      Assert.AreNotEqual(anonymous, session.Config);
      Assert.AreNotSame(anonymous, session.Config);
    }

    [Test]
    public void TestSessionDefaultSourceCannotBeMutated()
    {
      var anonymous = new SessionConfig("localhost", null, null);
      var defaultSource = new MutableItemSource("master", "en");
      var session = new ScApiSession(anonymous, defaultSource);

      Assert.AreEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);



      defaultSource.SetDatabase("web");
      defaultSource.SetLanguage("da");
      defaultSource.SetVersion("100500");
      Assert.AreNotEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);
    }
  }
}

