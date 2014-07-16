﻿

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK.Session;
  using Sitecore.MobileSDK.SessionSettings;


  [TestFixture]
  public class CreateSessionTest
  {
    private IWebApiCredentials adminCredentials = new WebApiCredentialsPOD("admin", "b");

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
      var builder = 
        SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("sitecore.net")
          .WebApiVersion("v1")
          .DefaultDatabase("web")
          .DefaultLanguage("en")
          .Site("/sitecore/shell")
          .MediaLibraryRoot("/sitecore/media library")
          .DefaultMediaResourceExtension("ashx");

      var session = builder.BuildSession();
      Assert.IsNotNull(session);

      var roSession = builder.BuildReadonlySession();
      Assert.IsNotNull(roSession);
    }

    [Test]
    public void TestAuthenticatedSessionShouldBeCreatedByTheBuilder()
    {
      IWebApiCredentials credentials = this.adminCredentials;

      var builder = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(credentials)
        .WebApiVersion("v1")
        .DefaultDatabase("web")
        .DefaultLanguage("en")
        .Site("/sitecore/shell")
        .MediaLibraryRoot("/sitecore/media library")
        .DefaultMediaResourceExtension("ashx");


      ISitecoreWebApiSession session = builder.BuildSession();
      Assert.IsNotNull(session);

      var roSession = builder.BuildReadonlySession();
      Assert.IsNotNull(roSession);
    }

    [Test]
    public void TestWebApiVersionIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .WebApiVersion("v1")
        .WebApiVersion("v1")
      );

      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .WebApiVersion("v3")
        .WebApiVersion("v4")
      );
    }

    [Test]
    public void TestDatabaseIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .DefaultDatabase("web")
        .DefaultDatabase("web")
      );

      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .DefaultDatabase("master")
        .DefaultDatabase("core")
      );
    }

    [Test]
    public void TestLanguageIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .DefaultLanguage("en")
        .DefaultLanguage("es")
      );

      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .DefaultLanguage("en")
        .DefaultLanguage("en")
      );
    }

    [Test]
    public void TestSiteIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .Site("/sitecore/shell")
        .Site("/baz/baz")
      );

      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .Site("/ololo/trololo")
        .Site("/foo/bar")
      );
    }


    [Test]
    public void TestMediaRootIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .MediaLibraryRoot("/sitecore/media library")
        .MediaLibraryRoot("/sitecore/other media library")
      );

      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .MediaLibraryRoot("/dev/null")
        .MediaLibraryRoot("/sitecore/media library")
      );
    }


    [Test]
    public void TestMediaExtIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .DefaultMediaResourceExtension("ashx")
        .DefaultMediaResourceExtension("pdf")
      );

      Assert.Throws<InvalidOperationException>( ()=>
        SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .DefaultMediaResourceExtension("jpeg")
        .DefaultMediaResourceExtension("jpg")
      );
    }
  }
}

