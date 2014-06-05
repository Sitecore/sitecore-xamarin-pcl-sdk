using MobileSDKUnitTest.Mock;

namespace MobileSDKIntegrationTest
{
  using System;
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
      var request = new MockGetItemsByIdParameters
      {
        ItemId = this.testData.Items.Home.Id
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
    }

    [Test]
    public async void TestGetItemByInvalidId()
    {
      const string ItemInvalidId = "{4%75_B3E2 D050FA|cF4E1}";
      var request = new MockGetItemsByIdParameters
      {
        ItemId = ItemInvalidId
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);
      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByNotExistentId()
    {
      const string NotExistentId = "{3D6658D8-QQQQ-QQQQ-B3E2-D050FABCF4E1}";
      var request = new MockGetItemsByIdParameters
      {
        ItemId = NotExistentId
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);
      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByPath()
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = this.testData.Items.Home.Path
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response.Items[0]);
    }

    [Test]
    public async void TestGetItemByPathWithSpaces()
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = ItemWithSpacesPath
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

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
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = ItemWithSpacesPath
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

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
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = PathNotExistent
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);
      testData.AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByPathWithInternationalName()
    {
      const string ItemInterationalPath = "/sitecore/content/Home/Android/Folder for create items/Japanese/宇都宮";
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = ItemInterationalPath
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

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

      var request = new MockGetItemsByPathParameters
      {
        ItemPath = ItemInterationalPath
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      testData.AssertItemsCount(1, response);
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

      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = Query
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

      testData.AssertItemsCount(2, response);
      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByInternationalQuery()
    {
      const string QueryInternational = "/sitecore/content/HOME//*[@title='宇都宮']";
      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = QueryInternational
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

      testData.AssertItemsCount(1, response);
      Assert.AreEqual("宇都宮", response.Items[0].DisplayName);
    }

    [Test]
    public async void TestGetItemByInvalidQuery()
    {
      const string QueryInvalid = "/sitecore/content/HOME/AllowED_PARent//*[@@templatekey123='sample item']";
      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = QueryInvalid
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

      testData.AssertItemsCount(0, response);
    }

    [Test]
   public async void TestGetItemByNullId()
    {
      var request = new MockGetItemsByIdParameters
      {
        ItemId = null
      };

      try
      {
        await sessionAuthenticatedUser.ReadItemByIdAsync(request);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.ScConnectionException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Value cannot be empty or null"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
 public async void TestGetItemByNullPath()
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = null
      };

      try
      {
        await sessionAuthenticatedUser.ReadItemByPathAsync(request);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.ScConnectionException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Value cannot be empty or null"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
   public async void TestGetItemByNullQuery()
    {
      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = null
      };

      try
      {
        await sessionAuthenticatedUser.ReadItemByQueryAsync(request);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.ScConnectionException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Value cannot be empty or null"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetItemByEmptyPath()
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = ""
      };

      try
      {
        await sessionAuthenticatedUser.ReadItemByPathAsync(request);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.ScConnectionException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Value cannot be empty or null"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
   public async void TestGetItemByEmptyQuery()
    {
      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = ""
      };

      try
      {
        await sessionAuthenticatedUser.ReadItemByQueryAsync(request);
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.ScConnectionException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Value cannot be empty or null"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

    [Test]
    public async void TestGetOneHundredItemsByQuery()
    {
      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = "/sitecore/content/Home/Android/Static/100Items/*"
      };

      var response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

      testData.AssertItemsCount(100, response);

      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }


    [Test]
    public async void TestGetItemByPathWithUserWithoutReadAccessToHomeItem()
    {
      var config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "extranet\\noreadaccess", "noreadaccess");
      var sessionWithoutAccess = new ScApiSession(config, ItemSource.DefaultSource());

      var request = new MockGetItemsByPathParameters
      {
        ItemPath = this.testData.Items.Home.Path
      };

      ScItemsResponse response = await sessionWithoutAccess.ReadItemByPathAsync(request);
      testData.AssertItemsCount(0, response);
    }

    [Test]        //this case should be changed for another instance
   public async void TestGetItemByQueryWithturnedOffItemWebApi()
    {

      var config = new SessionConfig("http://ws-elt.dk.sitecore.net:7212", "sitecore\\admin", "b");   //this string should be deleted
      var sessionWithoutAccess = new ScApiSession(config, ItemSource.DefaultSource());                //this string should be deleted

      var request = new MockGetItemsByPathParameters
      {
        ItemPath = this.testData.Items.Home.Path
      };
  
       try
      {
       // await sessionAuthenticatedUser.ReadItemByPathAsync(request);  // this string should be uncommented
        await sessionWithoutAccess.ReadItemByPathAsync(request);                                      //this string should be deleted
      }
      catch (Exception exception)
      {
        Assert.AreEqual("Sitecore.MobileSDK.ScConnectionException", exception.GetType().ToString());
        Assert.True(exception.Message.Contains("Response status code does not indicate success: 404 (Not Found)"));

        return;
      }

      Assert.Fail("Exception not thrown");
    }

  }
}