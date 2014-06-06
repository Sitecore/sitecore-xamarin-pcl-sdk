

namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using MobileSDKUnitTest.Mock;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  [TestFixture]
  public class GetItemsTest
  {
    private TestEnvironment testData;
    private ScApiSession sessionAuthenticatedUser;

    private const string ItemWithSpacesPath = "/sitecore/content/T E S T/i t e m";
    private const string ItemWithSpacesName = "i t e m";

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      this.sessionAuthenticatedUser = new ScApiSession(config, ItemSource.DefaultSource());
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionAuthenticatedUser = null;
      this.testData = null;
    }

    [Test]
    public async void TestGetItemById()
    {
      var response = await GetItemById(this.testData.Items.Home.Id);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
    }

    [Test]
    public async void TestGetItemByInvalidId()
    {
      const string ItemInvalidId = "{4%75_B3E2 D050FA|cF4E1}";
      var response = await GetItemById(ItemInvalidId);
      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByNotExistentId()
    {
      const string NotExistentId = "{3D6658D8-QQQQ-QQQQ-B3E2-D050FABCF4E1}";
      var response = await GetItemById(NotExistentId);
      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByPath()
    {
      var response = await GetItemByPath(testData.Items.Home.Path);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
    }

    [Test]
    public async void TestGetItemByPathWithSpaces()
    {
      var response = await GetItemByPath(ItemWithSpacesPath);

      testData.AssertItemsCount(1, response);
      var expectedItem = new TestEnvironment.Item
      {
        DisplayName = ItemWithSpacesName,
        Path = ItemWithSpacesPath,
        Template = testData.Items.Home.Template
      };
      testData.AssertItemsAreEqual(expectedItem, response.Items[0]);
    }

    [Test]
    public async void TestGetItemByPathForTwoItemsWithTheSamePathExist()
    {
      var response = await GetItemByPath(ItemWithSpacesPath);

      testData.AssertItemsCount(1, response);
      var expectedItem = new TestEnvironment.Item
      {
        DisplayName = ItemWithSpacesName,
        Path = ItemWithSpacesPath,
        Template = testData.Items.Home.Template
      };
      testData.AssertItemsAreEqual(expectedItem, response.Items[0]);

    }

    [Test]
    public async void TestGetItemByNotExistentPath()
    {
      const string PathNotExistent = "/not/existent/path";
      var response = await GetItemByPath(PathNotExistent);
      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByPathWithInternationalName()
    {
      const string ItemInterationalPath = "/sitecore/content/Home/Android/Folder for create items/Japanese/宇都宮";
      var response = await GetItemByPath(ItemInterationalPath);
      testData.AssertItemsCount(1, response);
      var expectedItem = new TestEnvironment.Item
      {
        DisplayName = "宇都宮",
        Path = ItemInterationalPath,
        Template = testData.Items.Home.Template
      };
      testData.AssertItemsAreEqual(expectedItem, response.Items[0]);
    }

    [Test]
    public async void TestGetItemByInternationalPath()
    {
      const string ItemInterationalPath = "/sitecore/content/Home/Android/Folder for create items/Japanese/宇都宮/ではまた明日";
      var response = await GetItemByPath(ItemInterationalPath);
      var expectedItem = new TestEnvironment.Item
      {
        DisplayName = "ではまた明日",
        Path = ItemInterationalPath,
        Template = testData.Items.Home.Template
      };
      testData.AssertItemsAreEqual(expectedItem, response.Items[0]);
    }

    [Test]
    public async void TestGetItemsByQueryCaseInsensetive()
    {
      const string Query = "/sitecore/content/HOME/AllowED_PARent/*";

      var response = await this.GetItemByQuery(Query);

      testData.AssertItemsCount(2, response);
      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByInternationalQuery()
    {
      const string QueryInternational = "/sitecore/content/HOME//*[@title='宇都宮']";
      var response = await this.GetItemByQuery(QueryInternational);

      testData.AssertItemsCount(1, response);
      Assert.AreEqual("宇都宮", response.Items[0].DisplayName);
    }

    [Test]
    public async void TestGetItemByInvalidQuery()
    {
      const string QueryInvalid = "/sitecore/content/HOME/AllowED_PARent//*[@@templatekey123='sample item']";
      var response = await this.GetItemByQuery(QueryInvalid);

      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByNullId()
    {
      try
      {
        var response = await this.GetItemById(null);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("System.ArgumentNullException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Item id cannot be null"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemByNullPath()
    {
      try
      {
        var response = await this.GetItemByPath(null);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("System.ArgumentNullException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Item path cannot be null"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemByNullQuery()
    {
      try
      {
        var response = await this.GetItemByQuery(null);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("System.ArgumentNullException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("SitecoreQuery cannot be null"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemByEmptyPath()
    {
      try
      {
        var response = await this.GetItemByPath("");
      }
      catch (Exception exception)
      {
        Assert.AreEqual("System.ArgumentNullException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Item path cannot be null or empty"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemByEmptyQuery()
    {
      try
      {
        var response = await this.GetItemByQuery("");
      }
      catch (Exception exception)
      {
        Assert.AreEqual("System.ArgumentNullException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("SitecoreQuery cannot be null"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetOneHundredItemsByQuery()
    {
      var response = await this.GetItemByQuery("/sitecore/content/Home/Android/Static/100Items/*");

      testData.AssertItemsCount(100, response);

      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByPathWithUserWithoutReadAccessToHomeItem()
    {
      var config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "extranet\\noreadaccess", "noreadaccess");
      var sessionWithoutAccess = new ScApiSession(config, ItemSource.DefaultSource());

      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithPath(this.testData.Items.Home.Path).Build();
      var response = await sessionWithoutAccess.ReadItemByPathAsync(request);

      testData.AssertItemsCount(0, response);
    }

    [Test] //this case should be changed for another instance
    public async void TestGetItemByQueryWithturnedOffItemWebApi()
    {

      var config = new SessionConfig("http://ws-alr1.dk.sitecore.net:75", testData.Users.Admin.Username, testData.Users.Admin.Password);
      var sessionWithoutAccess = new ScApiSession(config, ItemSource.DefaultSource()); // = sessionAuthenticatedUser;

      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithPath(this.testData.Items.Home.Path).Build();

      try
      {
        await sessionWithoutAccess.ReadItemByPathAsync(request); 
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.Exceptions.ParserException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Unable to download data from the internet"));
        return;
      }

      Assert.Fail("Exception not thrown");
    }

    private async Task<ScItemsResponse> GetItemById(string id)
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithId(id).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);
      return response;
    }

    private async Task<ScItemsResponse> GetItemByPath(string path)
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithPath(path).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);
      return response;
    }

    private async Task<ScItemsResponse> GetItemByQuery(string query)
    {
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithSitecoreQuery(query).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);
      return response;
    }
  }
}