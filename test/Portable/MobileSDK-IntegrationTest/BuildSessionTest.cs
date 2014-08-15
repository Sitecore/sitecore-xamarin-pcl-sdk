using Sitecore.MobileSDK;

namespace MobileSDKIntegrationTest
{
  using System;
  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.SessionSettings;

  [TestFixture]
  public class BuildSessionTest
  {
    private TestEnvironment testData;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
    }

    [Test]
    public void TestBuildSessionWithEmptyUsernameReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
       .Credentials(new WebApiCredentialsPOD("", testData.Users.Admin.Password))
      );
      Assert.AreEqual("SessionBuilder.Credentials.Username : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithEmptyPasswordReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
         .Credentials(new WebApiCredentialsPOD("username", ""))
         );
      Assert.AreEqual("SessionBuilder.Credentials.Password : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithNullUsernameReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
         .Credentials(new WebApiCredentialsPOD(null, "password"))
         );
      Assert.IsTrue(exception.Message.Contains("SessionBuilder.Credentials.Username"));
    }

    [Test]
    public void TestBuildSessionWithNullPasswordReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
         .Credentials(new WebApiCredentialsPOD("username", null))
         );
      Assert.IsTrue(exception.Message.Contains("SessionBuilder.Credentials.Password"));
    }

    [Test]
    public void TestBuildSessionWithNullUrlReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(null)
        .Credentials(new WebApiCredentialsPOD("Username", "Password"))
        );
      Assert.IsTrue(exception.Message.Contains("SessionBuilder.InstanceUrl"));
    }

    [Test]
    public void TestBuildSessionWithEmptyUrlReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("")
        .Credentials(new WebApiCredentialsPOD("Username", "Password"))
        );
      Assert.AreEqual("SessionBuilder.InstanceUrl : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithNullDatabaseDoNotReturnsException()
    {
      using
        (
          var session = this.NewSession()
          .DefaultDatabase(null)
          .BuildReadonlySession()
        )
      {
        Assert.IsNotNull(session);
      }
    }

    [Test]
    public void TestBuildSessionWithLanguageWithSpacesOnlyReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => this.NewSession()
        .DefaultLanguage(" ")
        );
      Assert.AreEqual("SessionBuilder.DefaultLanguage : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithNullMediaSourceExtensionDoNotReturnsException()
    {
      using
        (
          var session = this.NewSession()
          .DefaultMediaResourceExtension(null)
          .BuildSession()
        )
      {
        Assert.IsNotNull(session);
      }
    }

    [Test]
    public void TestBuildSessionWithEmptyMediaLibraryRootDoNotReturnsException()
    {
      using
      (
        var session = this.NewSession()
        .MediaLibraryRoot("")
        .BuildSession()
      )
      {
        Assert.IsNotNull(session);
      }

    }

    [Test]
    public void TestBuildSessionWithNullSiteDoNotReturnsException()
    {
      using
        (
          var session = this.NewSession()
          .Site(null)
          .BuildSession()
        )
      {
        Assert.IsNotNull(session);
      }
    }

    [Test]
    public void TestBuildSessionWithMediaPrefixWithSpacesOnlyReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => this.NewSession()
        .MediaPrefix(" ")
        );
      Assert.AreEqual("SessionBuilder.MediaPrefix : The input cannot be empty.", exception.Message);
    }

    private IBaseSessionBuilder NewSession()
    {
      return SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
        .Credentials(new WebApiCredentialsPOD("username", "password"));
    }
  }
}