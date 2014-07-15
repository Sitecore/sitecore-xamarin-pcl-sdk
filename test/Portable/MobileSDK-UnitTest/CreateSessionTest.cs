

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK.Session;
  using Sitecore.MobileSDK.SessionSettings;


  [TestFixture]
  public class CreateSessionTest
  {
    [Test]
    public void TestSessionConfigForAuthenticatedSession()
    {
      var conf = SessionConfig.NewAuthenticatedSessionConfig("localhost", "root", "pass", "/sitecore/shell", "v1");
      Assert.IsNotNull(conf);

      Assert.AreEqual("localhost", conf.InstanceUrl);
      Assert.AreEqual("root", conf.Login);
      Assert.AreEqual("pass", conf.Password);
      Assert.AreEqual("/sitecore/shell", conf.Site);
      Assert.AreEqual("v1", conf.ItemWebApiVersion);
    }

    [Test]
    public void TestSessionConfigAllowsBothNullForAuthenticatedSession()
    {
      var conf = SessionConfig.NewAuthenticatedSessionConfig("localhost", null, null, "/sitecore/shell", "v1");
      Assert.IsNotNull(conf);

      Assert.AreEqual("localhost", conf.InstanceUrl);
      Assert.IsNull(conf.Login);
      Assert.IsNull(conf.Password);
      Assert.AreEqual("/sitecore/shell", conf.Site);
      Assert.AreEqual("v1", conf.ItemWebApiVersion);
    }


    [Test]
    public void TestSessionConfigAllowsNullUserNameForAuthenticatedSession()
    {
      var conf = SessionConfig.NewAuthenticatedSessionConfig("localhost", null, "pass", "/sitecore/shell", "v1");
      Assert.IsNotNull(conf);

      Assert.AreEqual("localhost", conf.InstanceUrl);
      Assert.IsNull(conf.Login);
      Assert.AreEqual("pass", conf.Password);
      Assert.AreEqual("/sitecore/shell", conf.Site);
      Assert.AreEqual("v1", conf.ItemWebApiVersion);
    }

    [Test]
    public void TestSessionConfigDoesNotAllowNullPasswordForAuthenticatedSession()
    {
      Assert.Throws<ArgumentNullException>(() =>
        SessionConfig.NewAuthenticatedSessionConfig("localhost", "userrrr", null, "/sitecore/shell", "v1")
      );
    }

    [Test]
    public void TestSessionConfigForAnonymousSession()
    {
      var conf = SessionConfig.NewAnonymousSessionConfig("localhost", "/sitecore/shell", "v1");
      Assert.IsNotNull(conf);

      Assert.AreEqual("localhost", conf.InstanceUrl);
      Assert.IsNull(conf.Login);
      Assert.IsNull(conf.Password);
      Assert.AreEqual("/sitecore/shell", conf.Site);
      Assert.AreEqual("v1", conf.ItemWebApiVersion);
    }

    [Test]
    public void TestAnonymousSessionShouldBeCreatedByTheBuilder()
    {
      ISitecoreWebApiSession session = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .WebApiVersion("v1")
        .DefaultDatabase("web")
        .DefauldLanguage("en")
        .MediaLibraryRoot("/sitecore/media library")
        .DefaultMediaResourceExtension("ashx")
        .Build();

      Assert.IsNotNull(session);
    }

    [Test]
    public void TestAuthenticatedSessionShouldBeCreatedByTheBuilder()
    {
      IWebApiCredentials credentials = new WebApiCredentialsPOD("admin", "b");

      ISitecoreWebApiSession session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(credentials)
        .WebApiVersion("v1")
        .DefaultDatabase("web")
        .DefauldLanguage("en")
        .MediaLibraryRoot("/sitecore/media library")
        .DefaultMediaResourceExtension("ashx")
        .Build();

      Assert.IsNotNull(session);
    }
  }
}

