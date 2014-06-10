namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;
  using NUnit.Framework.Constraints;

  using System;
  using System.Security;
  using System.Net.Http;
  using System.Xml;

  using MobileSDKUnitTest.Mock;

  using Sitecore.MobileSDK;
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
      this.requestWithItemId = requestBuilder.RequestWithId (this.testData.Items.Home.Id).Build();
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

      var response = await session.ReadItemByIdAsync(requestWithItemId);
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
        await session.ReadItemByIdAsync(this.requestWithItemId);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.LoadDataFromNetworkException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));

        Assert.AreEqual("System.Net.Http.HttpRequestException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("An error occurred while sending the request."));
        return;
      }

      Assert.Fail("Excption not thrown");
    }

    [Test]
    public async void TestGetItemWithNullInstanceUrl()
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
      SessionConfig config = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Admin.Username, "");
      ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

      try
      {
        await session.ReadItemByIdAsync(this.requestWithItemId);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.ParserException", exception.GetType().ToString());
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
      SessionConfig config = new SessionConfig(testData.AuthenticatedInstanceUrl, "sitecore\\notexistent", "notexistent");
      ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

      try
      {
        await session.ReadItemByIdAsync(this.requestWithItemId);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.ParserException", exception.GetType().ToString());
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
      SessionConfig config = new SessionConfig(testData.AuthenticatedInstanceUrl, "inval|d u$er№ame", null);
      ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

      try
      {
        await session.ReadItemByIdAsync(this.requestWithItemId);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.ParserException", exception.GetType().ToString());
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
      SessionConfig config = new SessionConfig(testData.AuthenticatedInstanceUrl, null, null);
      ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

      try
      {
        await session.ReadItemByIdAsync(this.requestWithItemId);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.ParserException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));

        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
        Assert.True(exception.InnerException.Message.Contains("Access to site is not granted."));
        return;
      }

      Assert.Fail("Exception not thrown");
    }
  }
}
