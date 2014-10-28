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
      noInstanceUrl.ItemWebApiVersion = "v1";
      noInstanceUrl.Site = "/sitecore/shell";

      var noSite = new SessionConfigPOD();
      noSite.InstanceUrl = "http://localhost:80";
      noSite.ItemWebApiVersion = "v1";
      noSite.Site = null;


      var noWebApiVersion = new SessionConfigPOD();
      noWebApiVersion.InstanceUrl = "sitecore.net";
      noWebApiVersion.ItemWebApiVersion = null;
      noWebApiVersion.Site = "/sitecore/shell";



      Assert.Throws<ArgumentNullException>(() => new SessionConfigMerger(noInstanceUrl));
      Assert.Throws<ArgumentNullException>(() => new SessionConfigMerger(noWebApiVersion));
      Assert.DoesNotThrow(() => new SessionConfigMerger(noSite));
    }

    [Test]
    public void TestMergerReturnsDefaultSourceForNilInput()
    {
      var defaultConfig = new SessionConfigPOD();
      defaultConfig.InstanceUrl = "sitecore.net";
      defaultConfig.ItemWebApiVersion = "v1";
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
      defaultConfig.ItemWebApiVersion = "v1";
      defaultConfig.Site = "/sitecore/shell";

      var userConfig = new SessionConfigPOD();
      userConfig.InstanceUrl = "http://localhost:80";
      userConfig.ItemWebApiVersion = "v100500";
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
      defaultConfig.ItemWebApiVersion = "v1";
      defaultConfig.Site = "/sitecore/shell";

      var userConfig = new SessionConfigPOD();
      userConfig.InstanceUrl = null;
      userConfig.ItemWebApiVersion = null;
      userConfig.Site = null;

      var merger = new SessionConfigMerger(defaultConfig);
      ISessionConfig result = merger.FillSessionConfigGaps(userConfig);

      Assert.AreEqual(defaultConfig, result);
      Assert.AreNotSame(defaultConfig, result);
    }
  }
}

