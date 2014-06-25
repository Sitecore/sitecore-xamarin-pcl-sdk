using SitecoreMobileSDKMockObjects;

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;



  [TestFixture]
  public class ThreadSafetyTest
  {
//    [Test]
//    public void TestSessionConfigCannotBeMutated()
//    {
//      var anonymous = new SessionConfig("localhost", null, null);
//      var defaultSource = ItemSource.DefaultSource();
//
//      var session = new ScApiSession(anonymous, defaultSource);
//
//      Assert.AreEqual(defaultSource, session.DefaultSource);
//      Assert.AreNotSame(defaultSource, session.DefaultSource);
//
//      anonymous.InstanceUrl = "sitecore.net";
//      Assert.AreNotEqual(defaultSource, session.DefaultSource);
//      Assert.AreNotSame(defaultSource, session.DefaultSource);
//    }

    [Test]
    void TestSessionDefaultSourceCannotBeMutated()
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

