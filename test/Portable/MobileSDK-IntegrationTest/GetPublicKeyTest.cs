namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;

  using System;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.Items;
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

      var requestBuilder = new ItemWebApiRequestBuilder ();
      this.requestWithItemId = requestBuilder.ReadItemsRequestWithId (this.testData.Items.Home.Id).Build();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
    }

    [Test]
    public async void TestGetItemAsAuthenticatedUser()
    {
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      var session = new ScApiSession(config, ItemSource.DefaultSource());

      var response = await session.ReadItemAsync(requestWithItemId);
      testData.AssertItemsCount(1, response);
      Assert.AreEqual(testData.Items.Home.DisplayName, response.Items[0].DisplayName);
    }

    [Test]
    public async void TestGetItemsWithNotExistentInstanceUrl()
    {
      var config = new SessionConfig("http://mobiledev1ua1.dddk.sitecore.net", testData.Users.Admin.Username, testData.Users.Admin.Password);
      var session = new ScApiSession(config, ItemSource.DefaultSource());

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
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);

      TestDelegate action = () => new ScApiSession(config, null);
      var exception = Assert.Throws<ArgumentNullException>(action, "we should get exception here");

      Assert.IsTrue(
          exception.GetBaseException().ToString().Contains("ScApiSession.defaultSource cannot be null")
      );
    }

    [Test]
    public async void TestGetItemWithEmptyPassword()
    {
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Admin.Username, "");
      var session = new ScApiSession(config, ItemSource.DefaultSource());

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
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, "sitecore\\notexistent", "notexistent");
      var session = new ScApiSession(config, ItemSource.DefaultSource());

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
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, "inval|d u$er№ame", null);
      var session = new ScApiSession(config, ItemSource.DefaultSource());

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
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, null, null);
      var session = new ScApiSession(config, ItemSource.DefaultSource());

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
