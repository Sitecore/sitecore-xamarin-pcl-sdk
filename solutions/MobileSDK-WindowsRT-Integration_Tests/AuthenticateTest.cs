namespace MobileSDKIntegrationTest
{
    using System;
    using Sitecore.MobileSDK.API;
    using Sitecore.MobileSDK.API.Exceptions;
    using Sitecore.MobileSDK.MockObjects;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    [TestClass]
  public class AuthenticateTest
  {
    private TestEnvironment testData;

    [TestInitialize]
        public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
    }

    [TestCleanup]
        public void TearDown()
    {
      this.testData = null;
    }

        [TestMethod]
    public async void TestCheckValidCredentials()
    {
      using
      (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Admin)
          .BuildReadonlySession()
      )
      {
        bool response = await session.AuthenticateAsync();
        Assert.IsTrue(response);
      }
    }
        [TestMethod]
    public async void TestGetAuthenticationWithNotExistentUsername()
    {
      using
      (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.NotExistent)
          .BuildReadonlySession()
      )
      {
        bool response = await session.AuthenticateAsync();
        Assert.IsFalse(response);
      }
    }
        [TestMethod]
        //TODO: This testcase will fail after Item Web Api bugfix.
    public async void TestGetAuthenticationAsUserInExtranetDomainToShellSite()
    {
      using
      (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Creatorex)
          .Site(testData.ShellSite)
          .BuildReadonlySession()
      )
      {
        bool response = await session.AuthenticateAsync();
        Assert.IsTrue(response);
      }
    }
        [TestMethod]
    public async void TestGetAuthenticationAstUserInExtraneDomainToWebsite()
    {
      using
      (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.Creatorex)
          .Site("/")
          .BuildReadonlySession()
      )
      {
        bool response = await session.AuthenticateAsync();
        Assert.IsTrue(response);
      }
    }
        [TestMethod]
    public async void TestGetAuthenticationAsUserInSitecoreDomainToWebsite()
    {
      using
      (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.SitecoreCreator)
          .Site("/")
          .BuildReadonlySession()
      )
      {
        bool response = await session.AuthenticateAsync();
        Assert.IsTrue(response);
      }
    }
        [TestMethod]
    public async void TestGetAuthenticationAsUserInSitecoreDomainToShellSite()
    {
      using
      (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(testData.Users.SitecoreCreator)
          .Site(testData.ShellSite)
          .BuildReadonlySession()
      )
      {
        bool response = await session.AuthenticateAsync();
        Assert.IsTrue(response);
      }
    }
        [TestMethod]
    public async void TestGetAuthenticationWithNotExistentPassword()
    {
      using
      (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(new WebApiCredentialsPOD(testData.Users.Admin.Username, "wrongpassword"))
        .BuildReadonlySession()
      )
      {
        bool response = await session.AuthenticateAsync();
        Assert.IsFalse(response);
      }
    }
        [TestMethod]
    public async void TestGetAuthenticationWithInvalidPassword()
    {
      using
      (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(new WebApiCredentialsPOD(testData.Users.Admin.Username, "Password $#%^&^*"))
        .BuildReadonlySession()
      )
      {
        bool response = await session.AuthenticateAsync();
        Assert.IsFalse(response);
      }
    }
        [TestMethod]
    public async void TestGetAuthenticationWithInvalidUsername()
    {
      using
      (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(new WebApiCredentialsPOD("Username $#%^&^*", testData.Users.Admin.Password))
          .BuildReadonlySession()
      )
      {
        bool response = await session.AuthenticateAsync();
        Assert.IsFalse(response);
      }
    }
    //    [testmethod]
    //public void testgetpublickeywithnotexistenturl()
    //{
    //  using
    //  (
    //    var session = sitecorewebapisessionbuilder.authenticatedsessionwithhost("http://mobilesdk-notexistent.com")
    //      .credentials(testdata.users.admin)
    //      .buildreadonlysession()
    //  )
    //  {
    //    testdelegate testcode = async () =>
    //    {
    //      await session.authenticateasync();
    //    };
    //    exception exception = assert.throws<rsahandshakeexception>(testcode);
    //    assert.istrue(exception.message.contains("public key not received properly"));


    //    // todo : create platform specific files for this test case
    //    // windows : system.net.http.httprequestexception
    //    // ios : system.net.webexception

    //    //assert.areequal("system.net.http.httprequestexception", exception.innerexception.gettype().tostring());
    //    bool testcorrect = exception.innerexception.gettype().tostring().equals("system.net.http.httprequestexception");
    //    testcorrect = testcorrect || exception.innerexception.gettype().tostring().equals("system.net.webexception");
    //    assert.istrue(testcorrect, "exception.innerexception is wrong");

    //    bool messagecorrect = exception.innerexception.message.contains("an error occurred while sending the request");
    //    messagecorrect = messagecorrect || exception.innerexception.message.contains("nameresolutionfailure");
    //    assert.istrue(messagecorrect, "exception message is not correct");
    //  }
    //}
    //    [TestMethod]
    //public void TestGetAuthenticationWithInvalidUrl()
    //{
    //  using
    //  (
    //    var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("\\m.dk%&^&*(.net")
    //    .Credentials(testData.Users.Admin)
    //    .BuildReadonlySession()
    //  )
    //  {
    //    TestDelegate testCode = async () =>
    //    {
    //      await session.AuthenticateAsync();
    //    };
    //    Exception exception = Assert.Throws<RsaHandshakeException>(testCode);
    //    Assert.True(exception.Message.Contains("Public key not received properly"));

    //    Assert.AreEqual("System.UriFormatException", exception.InnerException.GetType().ToString());
    //    Assert.True(exception.InnerException.Message.Contains("Invalid URI: The hostname could not be parsed"));
    //  }
    //}
        [TestMethod]
    public async void TestGetAuthenticationForUrlWithoutHttp()
    {
      var urlWithoutHttp = testData.InstanceUrl.Remove(0, 7);
      using
      (
        var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(urlWithoutHttp)
          .Credentials(testData.Users.Admin)
          .BuildReadonlySession()
      )
      {
        bool response = await session.AuthenticateAsync();
        Assert.IsTrue(response);
      }
    }
  }
}