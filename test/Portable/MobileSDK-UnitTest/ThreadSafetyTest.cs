

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
    public void TestSessionDefaultSourceCannotBeMutated()
    {
      var anonymous = new SessionConfig("localhost", null, null);
      var defaultSource = ItemSource.DefaultSource();

      var session = new ScApiSession(anonymous, defaultSource);

      Assert.AreEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);

      anonymous.InstanceUrl = "sitecore.net";
      Assert.AreNotEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);
    }
  }
}

