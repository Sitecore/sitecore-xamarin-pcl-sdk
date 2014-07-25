﻿namespace MobileSDKIntegrationTest
{
  using System;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Exceptions;
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
        .Credentials(new WebApiCredentialsPOD(testData.Users.Admin.Username, "wrongpassword"))
        .BuildReadonlySession();

      bool response = await session.AuthenticateAsync();
      Assert.False(response);
    }

    [Test]
    public async void TestGetAuthenticationWithInvalidPassword()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(new WebApiCredentialsPOD(testData.Users.Admin.Username, "Password $#%^&^*"))
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