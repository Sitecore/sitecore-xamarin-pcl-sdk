namespace MobileSDKIntegrationTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.MockObjects;

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
      var exception = Assert.Throws<ArgumentException>(() => SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
       .Credentials(new SSCCredentialsPOD("", testData.Users.Admin.Password))
      );
      Assert.AreEqual("SessionBuilder.Credentials.Username : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithEmptyPasswordReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
         .Credentials(new SSCCredentialsPOD("username", ""))
         );
      Assert.AreEqual("SessionBuilder.Credentials.Password : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithNullUsernameReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
         .Credentials(new SSCCredentialsPOD(null, "password"))
         );
      Assert.IsTrue(exception.Message.Contains("SessionBuilder.Credentials.Username"));
    }

    [Test]
    public void TestBuildSessionWithNullPasswordReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
         .Credentials(new SSCCredentialsPOD("username", null))
         );
      Assert.IsTrue(exception.Message.Contains("SessionBuilder.Credentials.Password"));
    }

    [Test]
    public void TestBuildSessionWithNullUrlReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(null)
        .Credentials(new SSCCredentialsPOD("Username", "Password"))
        );
      Assert.IsTrue(exception.Message.Contains("SessionBuilder.InstanceUrl"));
    }

    [Test]
    public void TestBuildSessionWithEmptyUrlReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost("")
        .Credentials(new SSCCredentialsPOD("Username", "Password"))
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
      return SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
        .Credentials(new SSCCredentialsPOD("username", "password"));
    }
  }
}