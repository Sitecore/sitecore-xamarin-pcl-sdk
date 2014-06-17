namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;

  using System;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.SessionSettings;


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
      var session = testData.GetSession(this.testData.InstanceUrl, this.testData.Users.Admin.Username, this.testData.Users.Admin.Password);

      var response = await session.ReadItemAsync(requestWithItemId);
      testData.AssertItemsCount(1, response);
      Assert.AreEqual(testData.Items.Home.DisplayName, response.Items[0].DisplayName);
    }

    [Test]
    public async void TestAuthenticateToInstanceWithoutHttp()
    {
      var session = testData.GetSession("mobiledev1ua1.dk.sitecore.net:7119", testData.Users.Admin.Username, testData.Users.Admin.Password);

      try
      {
        await session.ReadItemAsync(this.requestWithItemId);
      }
      catch (RsaHandshakeException exception)
      {
        Assert.True(exception.Message.Contains("Public key not received properly"));

        Assert.AreEqual("System.ArgumentException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("Only 'http' and 'https' schemes are allowed."));
        return;
      }
    }

    [Test]
    public async void TestAuthenticateWithSlashInTheEnd()
    {
      var session = testData.GetSession("http://mobiledev1ua1.dk.sitecore.net:7119/", testData.Users.Admin.Username, testData.Users.Admin.Password);

      var response = await session.ReadItemAsync(requestWithItemId);
      testData.AssertItemsCount(1, response);
      Assert.AreEqual(testData.Items.Home.DisplayName, response.Items[0].DisplayName);
    }

    [Test]
    public async void TestGetItemsWithNotExistentInstanceUrl()
    {
      var session = testData.GetSession("http://mobiledev1ua1.dddk.sitecore.net", testData.Users.Admin.Username, testData.Users.Admin.Password);

      try
      {
        await session.ReadItemAsync(this.requestWithItemId);
      }
      catch (RsaHandshakeException exception)
      {
        Assert.True(exception.Message.Contains("Public key not received properly"));

        Assert.AreEqual("System.Net.Http.HttpRequestException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("An error occurred while sending the request."));
        return;
      }

      Assert.Fail("Excption not thrown");
    }

    [Test]
    public void TestGetItemWithNullInstanceUrl()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => new SessionConfig(null, testData.Users.Admin.Username, testData.Users.Admin.Password));
      Assert.IsTrue(
          exception.GetBaseException().ToString().Contains("SessionConfig.InstanceUrl is required")
      );
    }

    [Test]
    public void TestGetItemWithNullItemsSource()
    {
      var config = new SessionConfig(testData.InstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);

      TestDelegate action = () => new ScApiSession(config, null);
      var exception = Assert.Throws<ArgumentNullException>(action, "we should get exception here");

      Assert.IsTrue(
          exception.GetBaseException().ToString().Contains("ScApiSession.defaultSource cannot be null")
      );
    }

    [Test]
    public async void TestGetItemWithEmptyPassword()
    {
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, "");

      try
      {
        await session.ReadItemAsync(this.requestWithItemId);
      }
      catch (ParserException exception)
      {
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));

        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("Access to site is not granted."));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemWithNotExistentUser()
    {
      var session = testData.GetSession(testData.InstanceUrl, "sitecore\\notexistent", "notexistent");

      try
      {
        await session.ReadItemAsync(this.requestWithItemId);
      }
      catch (ParserException exception)
      {
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));

        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("Access to site is not granted."));

        return;
      }
      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemWithInvalidUsernameAndPassword()
    {
      var session = testData.GetSession(testData.InstanceUrl, "inval|d u$er№ame", null);

      try
      {
        await session.ReadItemAsync(this.requestWithItemId);
      }
      catch (ParserException exception)
      {
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));

        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("Access to site is not granted."));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemAsAnonymousWithoutReadAccess()
    {
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Anonymous.Username, testData.Users.Anonymous.Password);

      try
      {
        await session.ReadItemAsync(this.requestWithItemId);
      }
      catch (ParserException exception)
      {
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));

        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("Access to site is not granted."));
        return;
      }

      Assert.Fail("Exception not thrown");
    }
  }
}
