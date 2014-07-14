namespace MobileSDKIntegrationTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.SessionSettings;


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
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNotExistentUsername()
    {
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username + "wrong", testData.Users.Admin.Password);
      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    //TODO: This testcase should fail due to WebApi bug.
    [Test]
    public async void TestGetAuthenticationAsUserInExtranetDomainToShellSite()
    {
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Creatorex.Username, testData.Users.Creatorex.Password, null, testData.ShellSite);
      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationAstUserInExtraneDomainToWebsite()
    {
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Creatorex.Username, testData.Users.Creatorex.Password, null, "/");
      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationAsUserInSitecoreDomainToWebsite()
    {
      var session = testData.GetSession(testData.InstanceUrl, "sitecore\\creator", "creator", null, "/");
      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationAsUserInSitecoreDomainToShellSite()
    {
      var session = testData.GetSession(testData.InstanceUrl, "sitecore\\creator", "creator", null, testData.ShellSite);
      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNotExistentPassword()
    {
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, "NotExistentPassword");
      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithInvalidPassword()
    {
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, "Password $#%^&^*");
      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithInvalidUsername()
    {
      var session = testData.GetSession(testData.InstanceUrl, "#%^&*^()", testData.Users.Admin.Password);
      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithEmptyUsername()
    {
      var session = testData.GetSession(testData.InstanceUrl, "", testData.Users.Admin.Password);
      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithEmptyPassword()
    {
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, "");
      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNullUsername()
    {
      var session = testData.GetSession(testData.InstanceUrl, null, testData.Users.Admin.Password);
      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithNullPassword()
    {
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, null);
      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public void TestGetAuthenticationWithNullUrl()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => SessionConfig.NewAuthenticatedSessionConfig(null, testData.Users.Admin.Username, testData.Users.Admin.Password));
      Assert.IsTrue(
          exception.GetBaseException().ToString().Contains("SessionConfig.InstanceUrl is required")
      );
    }

    [Test]
    public async void TestGetAuthenticationWithEmptyUrl()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => SessionConfig.NewAuthenticatedSessionConfig("", testData.Users.Admin.Username, testData.Users.Admin.Password));
      Assert.IsTrue(
          exception.GetBaseException().ToString().Contains("SessionConfig.InstanceUrl is required")
      );
    }

    [Test]
    public void TestGetAuthenticationWithNotExistentUrl()
    {
      var session = testData.GetSession("http://mobiled.sitecore.net:7220", testData.Users.Admin.Username, testData.Users.Admin.Password);
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
      var session = testData.GetSession("\\m.dk%&^&*(.net", testData.Users.Admin.Username, testData.Users.Admin.Password);
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
      var session = testData.GetSession(urlWithoutHttp, testData.Users.Admin.Username, testData.Users.Admin.Password);
      bool response = await session.AuthenticateAsync();
      Assert.True(response);
    }
  }
}