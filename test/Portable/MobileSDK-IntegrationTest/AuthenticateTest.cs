using SitecoreMobileSDKMockObjects;
using Sitecore.MobileSDK;

namespace MobileSDKIntegrationTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
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
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNotExistentUsername()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.NotExistent)
        .BuildReadonlySession();


      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    //TODO: This testcase will fail after Item Web Api bugfix.
    [Test]
    public async void TestGetAuthenticationAsUserInExtranetDomainToShellSite()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Creatorex)
        .Site(testData.ShellSite)
        .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationAstUserInExtraneDomainToWebsite()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Creatorex)
        .Site("/")
        .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationAsUserInSitecoreDomainToWebsite()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.SitecoreCreator)
        .Site("/")
        .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationAsUserInSitecoreDomainToShellSite()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.SitecoreCreator)
        .Site(testData.ShellSite)
        .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNotExistentPassword()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(new WebApiCredentialsPOD(testData.Users.Admin.UserName, "wrongpassword"))
        .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithInvalidPassword()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(new WebApiCredentialsPOD(testData.Users.Admin.UserName, "Password $#%^&^*"))
        .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithInvalidUsername()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(new WebApiCredentialsPOD("Username $#%^&^*", testData.Users.Admin.Password))
        .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithEmptyUsername()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
       .Credentials(new WebApiCredentialsPOD("", testData.Users.Admin.Password))
       .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithEmptyPassword()
    {
      var sessionConfig = new MutableSessionConfig("mock instance", "mock login", "mock password");
      sessionConfig.SetInstanceUrl(testData.InstanceUrl);
      sessionConfig.SetLogin(testData.Users.Admin.UserName);
      sessionConfig.SetPassword("");

      var session = new ScApiSession(sessionConfig);

      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNullUsername()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(new WebApiCredentialsPOD(null, testData.Users.Admin.Password))
        .BuildReadonlySession();


      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public void TestGetAuthenticationWithNullPassword()
    {
      var exception = Assert.Throws<ArgumentNullException>(() =>  SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
         .Credentials(new WebApiCredentialsPOD("Username $#%^&^*", null))
         .BuildReadonlySession());
      Assert.AreEqual("Value cannot be null.\r\nParameter name: SessionConfig.Credentials : password is required for authenticated session", exception.Message);
    }

    [Test]
    public void TestGetAuthenticationWithNullUrl()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => 
        SessionConfig.NewAuthenticatedSessionConfig(null, testData.Users.Admin.UserName, testData.Users.Admin.Password)
      );

      Assert.IsTrue(exception.GetBaseException().ToString().Contains("SessionConfig.InstanceUrl is required"));
    }

    [Test]
    public void TestGetAuthenticationWithEmptyUrl()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => 
        SessionConfig.NewAuthenticatedSessionConfig("", testData.Users.Admin.UserName, testData.Users.Admin.Password)
      );

      Assert.IsTrue(exception.GetBaseException().ToString().Contains("SessionConfig.InstanceUrl is required"));
    }

    [Test]
    public void TestGetPublicKeyWithNotExistentUrl()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("http://mobilesdk-notexistent.com")
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
    public void TestGetAuthenticationWithInvalidUrl()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("\\m.dk%&^&*(.net")
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

      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(urlWithoutHttp)
        .Credentials(testData.Users.Admin)
        .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }
  }
}