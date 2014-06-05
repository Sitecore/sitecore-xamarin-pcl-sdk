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
    private TestEnvironment testData;
    private ScApiSession sessionAuthenticatedUser;

    // for this scenario we should created two the same items with path /sitecore/content/T E S T/i t e m
    private const string itemWithSpacesPath = "/sitecore/content/T E S T/i t e m";
    private const string itemWithSpacesName = "i t e m";

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
    }

    [Test]
    public async void TestGetItemById()
    {
      var request = new MockGetItemsByIdParameters
      {
        ItemId = this.testData.Items.Home.Id
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual(testData.Items.Home.Name, response.Items[0].DisplayName);
      Assert.AreEqual(testData.Items.Home.Id, response.Items[0].Id);
      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }

    private static void AssertItemsCount(int itemCount, ScItemsResponse response)
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

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);
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

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByIdAsync(request);
      AssertItemsCount(0, response);
    }

    [Test]
    public async void TestGetItemByPath()
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = this.testData.Items.Home.Path
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual(testData.Items.Home.Name, response.Items[0].DisplayName);
      Assert.AreEqual(testData.Items.Home.Path, response.Items[0].Path);
      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByPathWithSpaces()
    // for this scenario we should create item with path /sitecore/content/T E S T/i t e m
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = itemWithSpacesPath
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual(itemWithSpacesName, response.Items[0].DisplayName);
      Assert.AreEqual(itemWithSpacesPath, response.Items[0].Path);
      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByPathForTwoItemsWithTheSamePathExist()
    // for this scenario we should create two the same items with path /sitecore/content/T E S T/i t e m
    {
      var request = new MockGetItemsByPathParameters
      {
        ItemPath = itemWithSpacesPath
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual(itemWithSpacesName, response.Items[0].DisplayName);
      Assert.AreEqual(itemWithSpacesPath, response.Items[0].Path);
      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
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

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual("宇都宮", response.Items[0].DisplayName);
      Assert.AreEqual(ItemInterationalPath, response.Items[0].Path);
      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByInternationalPath()
    {
      const string itemInterationalPath = "/sitecore/content/Home/Android/Folder for create items/Japanese/宇都宮/ではまた明日";

      var request = new MockGetItemsByPathParameters
      {
        ItemPath = itemInterationalPath
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByPathAsync(request);

      AssertItemsCount(1, response);
      Assert.AreEqual("ではまた明日", response.Items[0].DisplayName);
      Assert.AreEqual(itemInterationalPath, response.Items[0].Path);
      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemsByQuery()
    {
      const string query = "/sitecore/content/HOME/AllowED_PARent/*";

      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = query
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

      AssertItemsCount(2, response);
      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }

    [Test]
    public async void TestGetItemByInternationalQuery()
    {
      const string queryInternational = "/sitecore/content/HOME//*[@title='宇都宮']";
      var request = new MockGetItemsByQueryParameters
      {
        SitecoreQuery = queryInternational
      };

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

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

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

      AssertItemsCount(0, response);
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
      var request = new MockGetItemsByQueryParameters()
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

      ScItemsResponse response = await this.sessionAuthenticatedUser.ReadItemByQueryAsync(request);

      AssertItemsCount(100, response);

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
      AssertItemsCount(0, response);
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