﻿

namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.Items;

  //  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  //  using System.IO;
  //  using System.Threading;
  //  using MonoTouch.UIKit;
  //  using MonoTouch.Foundation;

  [TestFixture]
  public class GetItemsTest
  {
    private TestEnvironment testData;
    private ScApiSession sessionAuthenticatedUser;

    private const string ItemWithSpacesPath = "/sitecore/content/Home/Android/Static/Test item 1";
    private const string ItemWithSpacesName = "Test item 1";

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

    //    [Test]
    //    public async void TestGetMediaItem()
    //    {
    //      // @igk !!! TEMPORARY TEST FOR CUSTOM USE, DO NOT DELETE, PLEASE !!!
    //      IDownloadMediaOptions options = new MediaOptionsBuilder()
    //        .SetDisplayAsThumbnail(true)
    //        .Build();
    //
    //      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/testname222")
    //        .DownloadOptions(options)
    //        .Database("master")
    //        .Build();
    //        
    //      var response = await this.sessionAuthenticatedUser.DownloadResourceAsync(request);
    //     
    //      byte[] data;
    //
    //      using (BinaryReader br = new BinaryReader(response))
    //      {
    //        data = br.ReadBytes((int)response.Length);
    //      }
    //
    //      UIImage image = null;
    //      image = new UIImage(NSData.FromArray(data));
    //      //string text = reader.ReadToEnd();
    //
    //      var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
    //      string jpgFilename = System.IO.Path.Combine (documentsDirectory, "Photo.jpg"); // hardcoded filename, overwrites each time
    //      NSData imgData = image.AsJPEG();
    //      NSError err = null;
    //      if (imgData.Save(jpgFilename, false, out err))
    //      {
    //        Console.WriteLine("saved as " + jpgFilename);
    //      } else {
    //        Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
    //      }
    //     
    //    }

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
    public void TestGetItemByIdWithPathInParams()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Path).Build());
      Assert.AreEqual("System.ArgumentException", exception.GetType().ToString());
      Assert.AreEqual("Item id must have curly braces '{}'", exception.Message);
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
    public void TestGetItemByNullId()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemById(null);
        await task;
      };

      Assert.Throws<ArgumentNullException>(testCode);
    }

    [Test]
    public void TestGetItemByNullPath()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemByPath(null);
        await task;
      };

      Assert.Throws<ArgumentNullException>(testCode);
    }

    [Test]
    public void TestGetItemByNullQuery()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemByQuery(null);
        await task;
      };

      Assert.Throws<ArgumentNullException>(testCode);
    }

    [Test]
    public void TestGetItemByEmptyPath()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemByPath("");
        await task;
      };

      var exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("AbstractGetItemRequestBuilder.ItemPath : The input cannot be null or empty.", exception.Message);
    }

    [Test]
    public void TestGetItemByEmptyQuery()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemByQuery("");
        await task;
      };

      var exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("AbstractGetItemRequestBuilder.ItemQuery : The input cannot be null or empty.", exception.Message);
    }

    [Test]
    public void TestGetItemByIdWithSpacesOnly()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemById(" ");
        await task;
      };

      var exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("AbstractGetItemRequestBuilder.ItemId : The input cannot be null or empty.", exception.Message);
    }

    [Test]
    public void TestGetItemByPathWithSpacesOnly()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemByPath("  ");
        await task;
      };

      var exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("AbstractGetItemRequestBuilder.ItemId : The input cannot be null or empty.", exception.Message);
    }

    //TODO: create items for test first and remove them after test
    /*
    [Test]
    public async void TestGetOneHundredItemsByQuery()
    {
      var response = await this.GetItemByQuery("/sitecore/content/Home/Android/Static/100Items/*");
      testData.AssertItemsCount(100, response);
      Assert.AreEqual(testData.Items.Home.Template, response.Items[0].Template);
    }
    */

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

    //TODO: test manually with preconfigured Sitecore instance
    /*
    [Test] 
    public void TestGetItemByQueryWithturnedOffItemWebApi()
    {
      var sessionWithoutAccess = testData.GetSession("http://ws-alr1.dk.sitecore.net:75", testData.Users.Admin.Username, testData.Users.Admin.Password);
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path).Build();

      TestDelegate testCode = async () =>
      {
        var task = sessionWithoutAccess.ReadItemAsync(request);
        await task;
      };

      Assert.Throws<RsaHandshakeException>(testCode);
    }
    */

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