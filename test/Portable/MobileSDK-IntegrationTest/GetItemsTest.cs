using MobileSDKUnitTest.Mock;

namespace MobileSDKIntegrationTest
{
  using System;
  using System.Configuration;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  [TestFixture]
  public class GetItemsTest
  {
    private ScApiSession sessionWithAnonymousAccess;
    private string homeItemId;
    private string homeItemPath;
    private const string homeItemName = "Home";
    private const string sampleItemTemplate = "Sample/Sample Item";

    // for this scenario we should created two the same items with path /sitecore/content/T E S T/i t e m
    private string itemWithSpacesPath = "/sitecore/content/T E S T/i t e m";
    private string itemWithSpacesName = "i t e m";

    [SetUp]
    public void Setup()
    {
      var config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "sitecore\\admin", "b");
      this.sessionWithAnonymousAccess = new ScApiSession(config, ItemSource.DefaultSource());
      this.homeItemId = ConfigurationManager.AppSettings["HomeItemId"];
      this.homeItemPath = ConfigurationManager.AppSettings["HomeItemPath"];
    }

    [Test]
    public async void TestGetItemById()
    {
      var request = new MockGetItemsByIdParameters
      {
        ItemId = this.homeItemId
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByIdAsync(request);           
     
      AssertItemsCount(1,response);
      Assert.AreEqual(homeItemName, response.Items[0].DisplayName);
      Assert.AreEqual(homeItemId, response.Items[0].Id);
      Assert.AreEqual(sampleItemTemplate, response.Items[0].Template);
    }

    private static void AssertItemsCount(int itemCount,ScItemsResponse response)
    {
      Assert.AreEqual(itemCount, response.TotalCount);
      Assert.AreEqual(itemCount, response.ResultCount);
      Assert.AreEqual(itemCount, response.Items.Count);
    }

    [Test]
    public async void TestGetItemByInvalidId()
    {
      const string itemInvalidId = "{4%75_B3E2 D050FA|cF4E1}";
      var request = new MockGetItemsByIdParameters
      {
        ItemId = itemInvalidId
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByIdAsync(request);
      AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByNotExistentId()
    {
      const string notExistentId = "{3D6658D8-QQQQ-QQQQ-B3E2-D050FABCF4E1}";
      var request = new MockGetItemsByIdParameters
      {
        ItemId = notExistentId
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByIdAsync(request);
      AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByPath()
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = this.homeItemPath
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual(homeItemName, response.Items[0].DisplayName);
      Assert.AreEqual(homeItemPath, response.Items[0].Path);
      Assert.AreEqual(sampleItemTemplate, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByPathWithSpaces()
    // for this scenario we should created item with path /sitecore/content/T E S T/i t e m
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = this.itemWithSpacesPath
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual(itemWithSpacesName, response.Items[0].DisplayName);
      Assert.AreEqual(itemWithSpacesPath, response.Items[0].Path);
      Assert.AreEqual(sampleItemTemplate, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByPathForTwoItemsWithTheSamePathExist()
    // for this scenario we should created two the same items with path /sitecore/content/T E S T/i t e m
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = this.itemWithSpacesPath
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual(itemWithSpacesName, response.Items[0].DisplayName);
      Assert.AreEqual(itemWithSpacesPath, response.Items[0].Path);
      Assert.AreEqual(sampleItemTemplate, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByNotExistentPath()
    {
      const string PathNotExistent = "/not/existent/path";
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = PathNotExistent
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(request);
      AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByPathWithInternationalName()
    {
      const string ItemInterationalPath = "/sitecore/content/Home/Android/Folder for create items/Japanese/宇都宮";
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = ItemInterationalPath
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual("宇都宮", response.Items[0].DisplayName);
      Assert.AreEqual(ItemInterationalPath, response.Items[0].Path);
      Assert.AreEqual(sampleItemTemplate, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByInternationalPath()
    {
      const string itemInterationalPath = "/sitecore/content/Home/Android/Folder for create items/Japanese/宇都宮/ではまた明日";

      var request = new MockGetItemsByPathParameters();

      request.ItemPath = itemInterationalPath;
    
      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByPathAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual("ではまた明日", response.Items[0].DisplayName);
      Assert.AreEqual(itemInterationalPath, response.Items[0].Path);
      Assert.AreEqual(sampleItemTemplate, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemsByQuery()
    {
      const string query = "/sitecore/content/HOME/AllowED_PARent/*";

      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = query
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByQueryAsync(request);

      AssertItemsCount(2, response);
      Assert.AreEqual(sampleItemTemplate,response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByInternationalQuery()
    {
      const string queryInternational = "/sitecore/content/HOME//*[@title='宇都宮']";
      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = queryInternational
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByQueryAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual("宇都宮", response.Items[0].DisplayName);
    }

    [Test]
    public async void TestGetItemByInvalidQuery()
    {
      const string queryInvalid = "/sitecore/content/HOME/AllowED_PARent//*[@@templatekey123='sample item']";
      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = queryInvalid
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByQueryAsync(request);

      AssertItemsCount(0, response);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public async void TestGetItemByNullId()
    {
      var request = new MockGetItemsByIdParameters
      {
        ItemId = null
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByIdAsync(request);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public async void TestGetItemByNullPath()
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = null
      };

      await this.sessionWithAnonymousAccess.ReadItemByPathAsync(request);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public async void TestGetItemByNullQuery()
    {
      var request = new MockGetItemsByQueryParameters()
      {
        SitecoreQuery = null
      };

      await this.sessionWithAnonymousAccess.ReadItemByQueryAsync(request);
     }

    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public async void TestGetItemByEmptyPath()
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = ""
      };

      await this.sessionWithAnonymousAccess.ReadItemByPathAsync(request);
    }

    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public async void TestGetItemByEmptyQuery()
    {
      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = ""
      };

      await this.sessionWithAnonymousAccess.ReadItemByQueryAsync(request);
    }

    [Test]
    public async void TestGetOneHundredItemsByQuery()
    {
      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = "/sitecore/content/Home/Android/Static/100Items/*"
      };

      ScItemsResponse response = await this.sessionWithAnonymousAccess.ReadItemByQueryAsync(request);

      AssertItemsCount(100, response);

      Assert.AreEqual(sampleItemTemplate, response.Items[0].Template);
    }


    [Test]
    public async void TestGetItemByPathWithUserWithoutReadAccessToHomeItem()
    {
      SessionConfig config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "extranet\\noreadaccess", "noreadaccess");
      ScApiSession sessionWithoutAccess = new ScApiSession(config, ItemSource.DefaultSource());

      var request = new MockGetItemsByPathParameters();
      request.ItemPath = homeItemPath;

      ScItemsResponse response = await sessionWithoutAccess.ReadItemByPathAsync(request);
      AssertItemsCount(0, response);
    }

    [Test]
    [ExpectedException(typeof(System.Net.Http.HttpRequestException))]
    public async void TestGetItemByQueryWithturnedOffItemWebApi()
    {
      SessionConfig config = new SessionConfig("http://ws-elt.dk.sitecore.net:7212", "sitecore\\admin", "b");
      ScApiSession sessionWithoutAccess = new ScApiSession(config, ItemSource.DefaultSource());

      var request = new MockGetItemsByPathParameters();
      request.ItemPath = homeItemPath;

      ScItemsResponse response = await sessionWithoutAccess.ReadItemByPathAsync(request);
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionWithAnonymousAccess = null;
    }
  }
}