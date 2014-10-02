using Sitecore.MobileSDK.API.MediaItem;

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.SessionSettings;
  using SitecoreMobileSdkPasswordProvider.API;



  [TestFixture]
  public class CreateSessionTest
  {
    private IWebApiCredentials adminCredentials = new WebApiCredentialsPOD("admin", "b");

    #region Explicit Construction
    [Test]
    public void TestSessionConfigForAuthenticatedSession()
    {
      var sessionSettings = new SessionConfig("localhost", "/sitecore/shell", "v1");
      var credentials = new WebApiCredentialsPOD("root", "pass");

      Assert.IsNotNull(sessionSettings);
      Assert.IsNotNull(credentials);

      Assert.AreEqual("localhost", sessionSettings.InstanceUrl);
      Assert.AreEqual("root", credentials.Username);
      Assert.AreEqual("pass", credentials.Password);
      Assert.AreEqual("/sitecore/shell", sessionSettings.Site);
      Assert.AreEqual("v1", sessionSettings.ItemWebApiVersion);
    }

    [Test]
    public void TestSessionConfigAllowsBothNullForAuthenticatedSession()
    {
      var sessionSettings = new SessionConfig("localhost", "/sitecore/shell", "v1");
      var credentials = new WebApiCredentialsPOD(null, null);

      Assert.IsNotNull(sessionSettings);
      Assert.IsNotNull(credentials);

      Assert.AreEqual("localhost", sessionSettings.InstanceUrl);
      Assert.IsNull(credentials.Username);
      Assert.IsNull(credentials.Password);
      Assert.AreEqual("/sitecore/shell", sessionSettings.Site);
      Assert.AreEqual("v1", sessionSettings.ItemWebApiVersion);
    }


    [Test]
    public void TestSessionConfigAllowsNullUsernameForAuthenticatedSession()
    {
      var sessionSettings = new SessionConfig("localhost", "/sitecore/shell", "v1");
      var credentials = new WebApiCredentialsPOD(null, "pass");

      Assert.IsNotNull(sessionSettings);
      Assert.IsNotNull(credentials);

      Assert.AreEqual("localhost", sessionSettings.InstanceUrl);
      Assert.IsNull(credentials.Username);
      Assert.AreEqual("pass", credentials.Password);
      Assert.AreEqual("/sitecore/shell", sessionSettings.Site);
      Assert.AreEqual("v1", sessionSettings.ItemWebApiVersion);
    }
    #endregion Explicit Construction

    #region Builder Interface
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
    #endregion Builder Interface

    #region Write Once
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
    #endregion Write Once

    #region Validate Null
    [Test]
    public void TestWebApiVersionThrowsExceptionForNullInput()
    {
      Assert.Throws<ArgumentNullException>(() =>
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .WebApiVersion(null)
      );

      Assert.Throws<ArgumentNullException>(() =>
        SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("sitecore.net")
        .WebApiVersion(null)
      );
    }

    [Test]
    public void TestDatabaseDoNotThrowsExceptionForNullInput()
    {
      using 
        (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
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
          var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
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
          var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
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
          var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
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
          var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
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
      using ( var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .MediaResizingStrategy(DownloadStrategy.Plain)
        .BuildSession() )
      {
        Assert.IsNotNull(session);
      }


      using ( var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("sitecore.net")
        .Credentials(this.adminCredentials)
        .MediaResizingStrategy(DownloadStrategy.Hashed)
        .BuildSession() )
      {
        Assert.IsNotNull(session);
      }
    }
  }
}

