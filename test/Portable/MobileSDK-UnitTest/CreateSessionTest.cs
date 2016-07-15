namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.MediaItem;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.MockObjects;
  using Sitecore.MobileSDK.PasswordProvider.Interface;
  using Sitecore.MobileSDK.SessionSettings;

  [TestFixture]
  public class CreateSessionTest
  {
    private ISSCCredentials adminCredentials = new SSCCredentialsPOD("admin", "b");

    #region Explicit Construction
    [Test]
    public void TestSessionConfigForAuthenticatedSession()
    {
      var sessionSettings = new SessionConfig("localhost", "sitecore");
      var credentials = new SSCCredentialsPOD("root", "pass");

      Assert.IsNotNull(sessionSettings);
      Assert.IsNotNull(credentials);

      Assert.AreEqual("localhost", sessionSettings.InstanceUrl);
      Assert.AreEqual("root", credentials.Username);
      Assert.AreEqual("pass", credentials.Password);
      Assert.AreEqual("sitecore", sessionSettings.Site);
    }

    [Test]
    public void TestSessionConfigAllowsBothNullForAuthenticatedSession()
    {
      var sessionSettings = new SessionConfig("localhost", "sitecore");
      var credentials = new SSCCredentialsPOD(null, null);

      Assert.IsNotNull(sessionSettings);
      Assert.IsNotNull(credentials);

      Assert.AreEqual("localhost", sessionSettings.InstanceUrl);
      Assert.IsNull(credentials.Username);
      Assert.IsNull(credentials.Password);
      Assert.AreEqual("sitecore", sessionSettings.Site);
    }


    [Test]
    public void TestSessionConfigAllowsNullUsernameForAuthenticatedSession()
    {
      var sessionSettings = new SessionConfig("localhost", "sitecore");
      var credentials = new SSCCredentialsPOD(null, "pass");

      Assert.IsNotNull(sessionSettings);
      Assert.IsNotNull(credentials);

      Assert.AreEqual("localhost", sessionSettings.InstanceUrl);
      Assert.IsNull(credentials.Username);
      Assert.AreEqual("pass", credentials.Password);
      Assert.AreEqual("sitecore", sessionSettings.Site);
    }
    #endregion Explicit Construction

    #region Builder Interface
    [Test]
    public void TestAnonymousSessionShouldBeCreatedByTheBuilder()
    {
      var builder =
        SitecoreSSCSessionBuilder.AnonymousSessionWithHost("sitecore.net")
          .DefaultDatabase("web")
          .DefaultLanguage("en")
          .Site(" sitecore")
          .MediaLibraryRoot("/sitecore/media library")
          .DefaultMediaResourceExtension("ashx")
          .MediaPrefix("~/media");

      var session = builder.BuildSession();
      Assert.IsNotNull(session);

      var roSession = builder.BuildReadonlySession();
      Assert.IsNotNull(roSession);
    }

    [Test]
    public void TestAuthenticatedSessionShouldBeCreatedByTheBuilder()
    {
      ISSCCredentials credentials = this.adminCredentials;

      var builder = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(credentials)
        .DefaultDatabase("web")
        .DefaultLanguage("en")
        .Site("sitecore")
        .MediaLibraryRoot("/sitecore/media library")
        .DefaultMediaResourceExtension("ashx");


      ISitecoreSSCSession session = builder.BuildSession();
      Assert.IsNotNull(session);

      var roSession = builder.BuildReadonlySession();
      Assert.IsNotNull(roSession);
    }
    #endregion Builder Interface

    #region Write Once


    [Test]
    public void TestDatabaseIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>(() =>
        SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .DefaultDatabase("web")
        .DefaultDatabase("web")
      );

      Assert.Throws<InvalidOperationException>(() =>
        SitecoreSSCSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .DefaultDatabase("master")
        .DefaultDatabase("core")
      );
    }

    [Test]
    public void TestLanguageIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>(() =>
        SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .DefaultLanguage("en")
        .DefaultLanguage("es")
      );

      Assert.Throws<InvalidOperationException>(() =>
        SitecoreSSCSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .DefaultLanguage("en")
        .DefaultLanguage("en")
      );
    }

    [Test]
    public void TestSiteIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>(() =>
        SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .Site("sitecore")
        .Site("/baz/baz")
      );

      Assert.Throws<InvalidOperationException>(() =>
        SitecoreSSCSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .Site("/ololo/trololo")
        .Site("/foo/bar")
      );
    }


    [Test]
    public void TestMediaRootIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>(() =>
        SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .MediaLibraryRoot("/sitecore/media library")
        .MediaLibraryRoot("/sitecore/other media library")
      );

      Assert.Throws<InvalidOperationException>(() =>
        SitecoreSSCSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .MediaLibraryRoot("/dev/null")
        .MediaLibraryRoot("/sitecore/media library")
      );
    }


    [Test]
    public void TestMediaExtIsWriteOnce()
    {
      Assert.Throws<InvalidOperationException>(() =>
        SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .DefaultMediaResourceExtension("ashx")
        .DefaultMediaResourceExtension("pdf")
      );

      Assert.Throws<InvalidOperationException>(() =>
        SitecoreSSCSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .DefaultMediaResourceExtension("jpeg")
        .DefaultMediaResourceExtension("jpg")
      );
    }
    #endregion Write Once

    #region Validate Null
    [Test]
    public void TestSSCVersionThrowsExceptionForNullInput()
    {
      Assert.Throws<ArgumentNullException>(() =>
        SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .SSCVersion(null)
      );

      Assert.Throws<ArgumentNullException>(() =>
        SitecoreSSCSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .SSCVersion(null)
      );
    }

    [Test]
    public void TestDatabaseDoNotThrowsExceptionForNullInput()
    {
      using
        (
        var session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .DefaultDatabase(null)
        .BuildSession()
        )
      {
        Assert.IsNotNull(session);
      }
    }

    [Test]
    public void TestLanguageDoNotThrowsExceptionForNullInput()
    {
      using
        (
          var session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
          .Credentials(this.adminCredentials)
          .DefaultLanguage(null)
          .BuildSession()
        )
      {
        Assert.IsNotNull(session);
      }
    }

    [Test]
    public void TestSiteDoNotThrowsExceptionForNullInput()
    {
      using
        (
          var session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
          .Credentials(this.adminCredentials)
          .Site(null)
          .BuildSession()
        )
      {
        Assert.IsNotNull(session);
      }
    }


    [Test]
    public void TestMediaDoNotThrowsExceptionForNullInput()
    {
      using
        (
          var session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
          .Credentials(this.adminCredentials)
          .MediaLibraryRoot(null)
          .BuildSession()
        )
      {
        Assert.IsNotNull(session);
      }
    }


    [Test]
    public void TestMediaExtDonotThrowsExceptionForNullInput()
    {
      using
        (
          var session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
          .Credentials(this.adminCredentials)
          .DefaultMediaResourceExtension(null)
          .BuildSession()
        )
      {
        Assert.IsNotNull(session);
      }
    }
    #endregion Validate Null

    [Test]
    public void TestHashingFlagCanBeSet()
    {
      using (var session = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .MediaResizingStrategy(DownloadStrategy.Plain)
        .BuildSession())
      {
        Assert.IsNotNull(session);
        Assert.IsNotNull(session.MediaLibrarySettings);
        Assert.AreEqual(DownloadStrategy.Plain, session.MediaLibrarySettings.MediaDownloadStrategy);
      }


      using (var otherSession = SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .MediaResizingStrategy(DownloadStrategy.Hashed)
        .BuildSession())
      {
        Assert.IsNotNull(otherSession);
        Assert.IsNotNull(otherSession.MediaLibrarySettings);
        Assert.AreEqual(DownloadStrategy.Hashed, otherSession.MediaLibrarySettings.MediaDownloadStrategy);
      }
    }
  }
}

