

namespace MobileSDKIntegrationTest
{
  using NUnit.Framework;

  using System;
  using System.Threading.Tasks;

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
    public async void TestMissingHttpIsAutocompletedDuringAuthentication()
    {
      var session = testData.GetSession("mobiledev1ua1.dk.sitecore.net:7119", testData.Users.Admin.Username, testData.Users.Admin.Password);
      var certrificate = await session.ReadItemAsync(this.requestWithItemId);
      Assert.IsNotNull(certrificate);
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
    public void TestGetItemsWithNotExistentInstanceUrl()
    {
      var session = testData.GetSession("http://mobiledev1ua1.dddk.sitecore.net", testData.Users.Admin.Username, testData.Users.Admin.Password);

      TestDelegate testCode = async() =>
      {
        var task = session.ReadItemAsync(this.requestWithItemId);
        await task;
      };
      Exception exception = Assert.Throws<RsaHandshakeException>(testCode);

      Assert.True(exception.Message.Contains("Public key not received properly"));

      //@adk : changed exception type due to using different HTTP Client API
      Assert.AreEqual("System.Net.WebException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Error: NameResolutionFailure"));
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
    public async void TestGetItemWithNullItemsSource()
    {
      var config = new SessionConfig(testData.InstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      var session = new ScApiSession(config, null);

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
        .Build();

      var itemRequest = await session.ReadItemAsync(request);
      Assert.IsNotNull(itemRequest);
      Assert.AreEqual(1, itemRequest.ResultCount);
    }

    [Test]
    public void TestGetItemWithEmptyPassword()
    {
      var session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, "", ItemSource.DefaultSource(), testData.ShellSite);

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
      var session = testData.GetSession(testData.InstanceUrl, "sitecore\\notexistent", "notexistent", ItemSource.DefaultSource(), testData.ShellSite);

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
      var session = testData.GetSession(testData.InstanceUrl, "inval|d u$er№ame", null, ItemSource.DefaultSource(), testData.ShellSite);

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
      var session = testData.GetSession(testData.InstanceUrl, 
        testData.Users.Anonymous.Username, 
        testData.Users.Anonymous.Password, 
        ItemSource.DefaultSource(), 
        testData.ShellSite);


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
