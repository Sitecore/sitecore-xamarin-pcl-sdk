namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UserRequest;

  [TestFixture]
  public class SessionConfigMergerTest
  {
    [Test]
    public void TestMergerRequiresDefaultValues()
    {
      Assert.Throws<ArgumentNullException>(() => new SessionConfigMerger(null));
    }

    [Test]
    public void TestUrlAndVersionMustBeSetOnDefaultConfig()
    {
      var noInstanceUrl = new SessionConfigPOD();
      noInstanceUrl.InstanceUrl = null;
      noInstanceUrl.ItemSSCVersion = "v1";
      noInstanceUrl.Site = "/sitecore/shell";

      var noSite = new SessionConfigPOD();
      noSite.InstanceUrl = "http://localhost:80";
      noSite.ItemSSCVersion = "v1";
      noSite.Site = null;


      var noSSCVersion = new SessionConfigPOD();
      noSSCVersion.InstanceUrl = "sitecore.net";
      noSSCVersion.ItemSSCVersion = null;
      noSSCVersion.Site = "/sitecore/shell";



      Assert.Throws<ArgumentNullException>(() => new SessionConfigMerger(noInstanceUrl));
      Assert.Throws<ArgumentNullException>(() => new SessionConfigMerger(noSSCVersion));
      Assert.DoesNotThrow(() => new SessionConfigMerger(noSite));
    }

    [Test]
    public void TestMergerReturnsDefaultSourceForNilInput()
    {
      var defaultConfig = new SessionConfigPOD();
      defaultConfig.InstanceUrl = "sitecore.net";
      defaultConfig.ItemSSCVersion = "v1";
      defaultConfig.Site = "/sitecore/shell";


      var merger = new SessionConfigMerger(defaultConfig);
      ISessionConfig result = merger.FillSessionConfigGaps(null);

      Assert.AreSame(defaultConfig, result);
    }


    [Test]
    public void TestUserFieldsHaveHigherPriority()
    {
      var defaultConfig = new SessionConfigPOD();
      defaultConfig.InstanceUrl = "sitecore.net";
      defaultConfig.ItemSSCVersion = "v1";
      defaultConfig.Site = "/sitecore/shell";

      var userConfig = new SessionConfigPOD();
      userConfig.InstanceUrl = "http://localhost:80";
      userConfig.ItemSSCVersion = "v100500";
      userConfig.Site = "/abra/kadabra";

      var merger = new SessionConfigMerger(defaultConfig);
      ISessionConfig result = merger.FillSessionConfigGaps(userConfig);

      Assert.AreEqual(userConfig, result);
      Assert.AreNotSame(userConfig, result);
    }

    [Test]
    public void TestNullUserFieldsAreAutocompleted()
    {
      var defaultConfig = new SessionConfigPOD();
      defaultConfig.InstanceUrl = "sitecore.net";
      defaultConfig.ItemSSCVersion = "v1";
      defaultConfig.Site = "/sitecore/shell";

      var userConfig = new SessionConfigPOD();
      userConfig.InstanceUrl = null;
      userConfig.ItemSSCVersion = null;
      userConfig.Site = null;

      var merger = new SessionConfigMerger(defaultConfig);
      ISessionConfig result = merger.FillSessionConfigGaps(userConfig);

      Assert.AreEqual(defaultConfig, result);
      Assert.AreNotSame(defaultConfig, result);
    }
  }
}

