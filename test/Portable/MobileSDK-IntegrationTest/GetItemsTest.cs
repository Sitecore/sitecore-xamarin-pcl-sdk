namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.Items;

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
      this.sessionAuthenticatedUser = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
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
      const string ItemInterationalPath = "/sitecore/content/Home/Android/Static/Japanese/宇都宮";
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
      const string ItemInterationalPath = "/sitecore/content/Home/Android/Static/Japanese/宇都宮/ではまた明日";
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
        await this.GetItemById(null);
      }
      catch (ArgumentNullException exception)
      {
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
        await this.GetItemByPath(null);
      }
      catch (ArgumentNullException exception)
      {
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
        await this.GetItemByQuery(null);
      }
      catch (ArgumentNullException exception)
      {
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
        await this.GetItemByPath("");
      }
      catch (ArgumentNullException exception)
      {
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
        await this.GetItemByQuery("");
      }
      catch (ArgumentNullException exception)
      {
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
      var sessionWithoutAccess = testData.GetSession(
        testData.InstanceUrl,
        testData.Users.NoReadAccess.Username,
        testData.Users.NoReadAccess.Password);

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path).Build();
      var response = await sessionWithoutAccess.ReadItemAsync(request);

      testData.AssertItemsCount(0, response);
    }

    [Test] //this case should be changed for another instance
    public async void TestGetItemByQueryWithturnedOffItemWebApi()
    {
      var sessionWithoutAccess = testData.GetSession("http://ws-alr1.dk.sitecore.net:75", testData.Users.Admin.Username, testData.Users.Admin.Password);
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path).Build();

      try
      {
        await sessionWithoutAccess.ReadItemAsync(request);
      }
      catch (RsaHandshakeException exception)
      {
        Assert.True(exception.Message.Contains("Public key not received properly"));
        return;
      }
      Assert.Fail("Exception not thrown");
    }

    private async Task<ScItemsResponse> GetItemById(string id)
    {

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(id).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);
      return response;
    }

    private async Task<ScItemsResponse> GetItemByPath(string path)
    {

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(path).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);
      return response;
    }

    private async Task<ScItemsResponse> GetItemByQuery(string query)
    {

      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(query).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);
      return response;
    }
  }
}