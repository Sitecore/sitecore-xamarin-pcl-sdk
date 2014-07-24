namespace MobileSDKIntegrationTest
{
  using System;
  using NUnit.Framework;
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
    public void TestGetAuthenticationWithEmptyUsernameReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
       .Credentials(new WebApiCredentialsPOD("", testData.Users.Admin.Password))
       .BuildReadonlySession());

      Assert.AreEqual("SessionConfig.Credentials.Username : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithEmptyPasswordReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
         .Credentials(new WebApiCredentialsPOD("username", ""))
         .BuildReadonlySession());
      Assert.AreEqual("SessionConfig.Credentials.Password : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithNullUsernameReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
         .Credentials(new WebApiCredentialsPOD(null, "password"))
         .BuildReadonlySession());
      Assert.AreEqual("SessionConfig.Credentials.Username : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithNullPasswordReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
         .Credentials(new WebApiCredentialsPOD("username", null))
         .BuildReadonlySession());
      Assert.AreEqual("SessionConfig.Credentials.Password : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithNullUrlReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(null)
        .Credentials(new WebApiCredentialsPOD("Username", "Password"))
        .BuildReadonlySession());
      Assert.AreEqual("SessionConfig.InstanceUrl : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestGetAuthenticationWithEmptyUrlReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("")
        .Credentials(new WebApiCredentialsPOD("Username", "Password"))
        .BuildReadonlySession());
      Assert.AreEqual("SessionConfig.InstanceUrl : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithNullDatabaseReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => this.NewSession()
        .DefaultDatabase(null)
        .BuildReadonlySession());
      Assert.AreEqual("SessionConfig.DefaultDatabase : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithLanguageWithSpacesOnlyReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => this.NewSession()
        .DefaultLanguage(" ")
        .BuildReadonlySession());
      Assert.AreEqual("SessionConfig.DefaultLanguage : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithNullMediaSourceExtensionReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => this.NewSession()
        .DefaultMediaResourceExtension(null)
        .BuildSession());
      Assert.AreEqual("SessionConfig.DefaultMediaResourceExtension : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithEmptyMediaLibraryRootReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => this.NewSession()
        .MediaLibraryRoot("")
        .BuildSession());
      Assert.AreEqual("SessionConfig.MediaLibraryRoot : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithNullSiteReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => this.NewSession()
        .Site(null)
        .BuildSession());
      Assert.AreEqual("SessionConfig.Site : The input cannot be null or empty", exception.Message);
    }

    [Test]
    public void TestBuildSessionWithMediaPrefixWithSpacesOnlyReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() => this.NewSession()
        .MediaPrefix(" ")
        .BuildSession());
      Assert.AreEqual("SessionConfig.MediaPrefix : The input cannot be null or empty", exception.Message);
    }

    private IBaseSessionBuilder NewSession()
    {
      return SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
        .Credentials(new WebApiCredentialsPOD("username", "password"));
    }
  }
}