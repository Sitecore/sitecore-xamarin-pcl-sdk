using SitecoreMobileSDKMockObjects;
using Sitecore.MobileSDK;

namespace MobileSDKIntegrationTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK.Session;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.Exceptions;

  using MobileSDKIntegrationTest;


  [TestFixture]
  public class AuthenticateTest
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
    public async void TestCheckValidCredentials()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Admin)
          .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNotExistentUsername()
    {
      var badLogin = testData.Users.Admin.UserShallowCopy();
      badLogin.Username = testData.Users.Admin.Username + "wrong";

      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(badLogin)
          .BuildReadonlySession();


      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    //TODO: This testcase will fail after Item Web Api bugfix.
    [Test]
    public async void TestGetAuthenticationAsUserInExtranetDomainToShellSite()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Creatorex)
          .Site(testData.ShellSite)
          .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationAstUserInExtraneDomainToWebsite()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Creatorex)
          .Site("/")
          .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationAsUserInSitecoreDomainToWebsite()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.SitecoreCreator)
          .Site("/")
          .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationAsUserInSitecoreDomainToShellSite()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.SitecoreCreator)
          .Site(testData.ShellSite)
          .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNotExistentPassword()
    {
      var badPassword = testData.Users.Admin.UserShallowCopy();
      badPassword.Password = "NotExistentPassword";


      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(badPassword)
          .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithInvalidPassword()
    {
      var badPassword = testData.Users.Admin.UserShallowCopy();
      badPassword.Password = "Password $#%^&^*";


      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(badPassword)
          .BuildReadonlySession();


      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithInvalidUsername()
    {
      var badLogin = testData.Users.Admin.UserShallowCopy();
      badLogin.Username = "#%^&*^()";

      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(badLogin)
          .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithEmptyUsername()
    {
      var sessionConfig = new MutableSessionConfig("mock instance", "mock login", "mock password");
      sessionConfig.SetInstanceUrl(testData.InstanceUrl);
      sessionConfig.SetLogin("");
      sessionConfig.SetPassword(testData.Users.Admin.Password);

      MutableItemSource defaultItemSource = null;
      var session = new ScApiSession(sessionConfig, defaultItemSource);


      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithEmptyPassword()
    {
      var sessionConfig = new MutableSessionConfig("mock instance", "mock login", "mock password");
      sessionConfig.SetInstanceUrl(testData.InstanceUrl);
      sessionConfig.SetLogin(testData.Users.Admin.Username);
      sessionConfig.SetPassword("");

      MutableItemSource defaultItemSource = null;
      var session = new ScApiSession(sessionConfig, defaultItemSource);

      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNullUsername()
    {
      var sessionConfig = new MutableSessionConfig("mock instance", "mock login", "mock password");
      sessionConfig.SetInstanceUrl(testData.InstanceUrl);
      sessionConfig.SetLogin(null);
      sessionConfig.SetPassword(testData.Users.Admin.Password);

      MutableItemSource defaultItemSource = null;
      var session = new ScApiSession(sessionConfig, defaultItemSource);


      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNullPassword()
    {
      var sessionConfig = new MutableSessionConfig("mock instance", "mock login", "mock password");
      sessionConfig.SetInstanceUrl(testData.InstanceUrl);
      sessionConfig.SetLogin(testData.Users.Admin.Username);
      sessionConfig.SetPassword(null);

      MutableItemSource defaultItemSource = null;
      var session = new ScApiSession(sessionConfig, defaultItemSource);

      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public void TestGetAuthenticationWithNullUrl()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => 
        SessionConfig.NewAuthenticatedSessionConfig(null, testData.Users.Admin.Username, testData.Users.Admin.Password)
      );

      Assert.IsTrue(
          exception.GetBaseException().ToString().Contains("SessionConfig.InstanceUrl is required")
      );
    }

    [Test]
    public async void TestGetAuthenticationWithEmptyUrl()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => 
        SessionConfig.NewAuthenticatedSessionConfig("", testData.Users.Admin.Username, testData.Users.Admin.Password)
      );

      Assert.IsTrue(
          exception.GetBaseException().ToString().Contains("SessionConfig.InstanceUrl is required")
      );
    }

    [Test]
    public void TestGetPublicKeyWithNotExistentUrl()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Admin)
          .BuildReadonlySession();

      TestDelegate testCode = async () =>
      {
        await session.AuthenticateAsync();
      };
      Exception exception = Assert.Throws<RsaHandshakeException>(testCode);
      Assert.True(exception.Message.Contains("Public key not received properly"));


      // TODO : create platform specific files for this test case
      // Windows : System.Net.Http.HttpRequestException
      // iOS : System.Net.WebException
      Assert.AreEqual("System.Net.Http.HttpRequestException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("An error occurred while sending the request"));
    }

    [Test]
    public async void TestGetAuthenticationWithInvalidUrl()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("\\m.dk%&^&*(.net")
          .Credentials(testData.Users.Admin)
          .BuildReadonlySession();

      TestDelegate testCode = async () =>
      {
        await session.AuthenticateAsync();
      };
      Exception exception = Assert.Throws<RsaHandshakeException>(testCode);
      Assert.True(exception.Message.Contains("Public key not received properly"));

      Assert.AreEqual("System.UriFormatException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Invalid URI: The hostname could not be parsed"));
    }

    [Test]
    public async void TestGetAuthenticationForUrlWithoutHttp()
    {
      var urlWithoutHttp = testData.InstanceUrl.Remove(0, 7);

      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(urlWithoutHttp)
          .Credentials(testData.Users.Admin)
          .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }
  }
}