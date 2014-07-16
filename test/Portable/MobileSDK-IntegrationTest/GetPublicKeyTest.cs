﻿

namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;

  using System;
  using System.Net.Http;


  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.Session;

  using SitecoreMobileSDKMockObjects;



  [TestFixture]
  public class GetPublicKeyTest
  {
    private TestEnvironment testData;

    IReadItemsByIdRequest requestWithItemId;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();

      this.requestWithItemId = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.Home.Id).Build();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
    }

    [Test]
    public async void TestGetItemAsAuthenticatedUser()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          .BuildReadonlySession();

      var response = await session.ReadItemAsync(requestWithItemId);
      testData.AssertItemsCount(1, response);
      Assert.AreEqual(testData.Items.Home.DisplayName, response.Items[0].DisplayName);
    }

    [Test]
    public async void TestMissingHttpIsAutocompletedDuringAuthentication()
    {
      var urlWithoutHttp = testData.InstanceUrl.Remove(0, 7);

      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(urlWithoutHttp)
          .Credentials(this.testData.Users.Admin)
          .BuildReadonlySession();

      var certrificate = await session.ReadItemAsync(this.requestWithItemId);
      Assert.IsNotNull(certrificate);
    }

    [Test]
    public async void TestAuthenticateWithSlashInTheEnd()
    {
      string urlWithSlahInTheEnd = testData.InstanceUrl+'/';
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(urlWithSlahInTheEnd)
          .Credentials(this.testData.Users.Admin)
          .BuildReadonlySession();

      var response = await session.ReadItemAsync(requestWithItemId);
      testData.AssertItemsCount(1, response);
      Assert.AreEqual(testData.Items.Home.DisplayName, response.Items[0].DisplayName);
    }

    [Test]
    public void TestGetItemsWithNotExistentInstanceUrl()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("http://mobiledev1ua1.dddk.sitecore.net")
          .Credentials(this.testData.Users.Admin)
          .BuildReadonlySession();

      TestDelegate testCode = async() =>
      {
        var task = session.ReadItemAsync(this.requestWithItemId);
        await task;
      };
      Exception exception = Assert.Throws<RsaHandshakeException>(testCode);

      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.RsaHandshakeException", exception.GetType().ToString());
      Assert.AreEqual("[Sitecore Mobile SDK] Public key not received properly", exception.Message);
    }

    [Test]
    public void TestGetItemWithNullInstanceUrl()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => 
        SessionConfig.NewAuthenticatedSessionConfig(null, testData.Users.Admin.Username, testData.Users.Admin.Password)
      );

      Assert.IsTrue(
          exception.GetBaseException().ToString().Contains("SessionConfig.InstanceUrl is required")
      );
    }

    [Test]
    public async void TestGetItemWithNullItemsSource()
    {
      var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
        .Credentials(testData.Users.Admin)
        .BuildReadonlySession();

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
        .Build();

      var itemRequest = await session.ReadItemAsync(request);
      Assert.IsNotNull(itemRequest);
      Assert.AreEqual(1, itemRequest.ResultCount);
    }

    [Test]
    public void TestGetItemWithEmptyPassword()
    {
      var sessionConfig = new MutableSessionConfig("mock instance", "mock login", "mock password");
      sessionConfig.SetInstanceUrl(testData.InstanceUrl);
      sessionConfig.SetLogin(testData.Users.Admin.Username);
      sessionConfig.SetPassword("");
      sessionConfig.SetSite(testData.ShellSite);

      var defaultItemSource = ItemSource.DefaultSource();
      var session = new ScApiSession(sessionConfig, defaultItemSource);

      TestDelegate testCode = async() =>
      {
        var task = session.ReadItemAsync(this.requestWithItemId);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);

      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Access to site is not granted."));
    }

    [Test]
    public void TestGetItemWithNotExistentUser()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(this.testData.Users.NotExistent)
          .DefaultDatabase("web")
          .DefaultLanguage("en")
          .Site(testData.ShellSite)
          .BuildReadonlySession();


      TestDelegate testCode = async() =>
      {
        var task = session.ReadItemAsync(this.requestWithItemId);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);


      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Access to site is not granted."));
    }

    [Test]
    public void TestGetItemWithInvalidUsernameAndPassword()
    {
      var sessionConfig = new MutableSessionConfig("mock instance", "mock login", "mock password");
      sessionConfig.SetInstanceUrl(testData.InstanceUrl);
      sessionConfig.SetLogin("inval|d u$er№ame");
      sessionConfig.SetPassword(null);
      sessionConfig.SetSite(testData.ShellSite);

      var defaultItemSource = ItemSource.DefaultSource();
      var session = new ScApiSession(sessionConfig, defaultItemSource);

      TestDelegate testCode = async() =>
      {
        var task = session.ReadItemAsync(this.requestWithItemId);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);


      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Access to site is not granted."));
    }

    [Test]
    public void TestGetItemAsAnonymousWithoutReadAccess()
    {
      var session = 
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(testData.InstanceUrl)
          .Credentials(this.testData.Users.Anonymous)
          .DefaultDatabase("web")
          .DefaultLanguage("en")
          .Site(testData.ShellSite)
          .BuildReadonlySession();


      TestDelegate testCode = async() =>
      {
        var task = session.ReadItemAsync(this.requestWithItemId);
        await task;
      };
      Exception exception = Assert.Throws<ParserException>(testCode);

      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Access to site is not granted."));
    }
  }
}
