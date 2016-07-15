namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class GetItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreSSCReadonlySession sessionAuthenticatedUser;

    private const string ItemWithSpacesPath = "/sitecore/content/Home/Android/Static/Test item 1";
    private const string ItemWithSpacesName = "Test item 1";

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      this.sessionAuthenticatedUser =
        SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          .BuildReadonlySession();
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionAuthenticatedUser.Dispose();
      this.sessionAuthenticatedUser = null;
      this.testData = null;
    }

    //    [Test]
    //    public async void TestGetMediaItem()
    //    {
    //      // @igk !!! TEMPORARY TEST FOR CUSTOM USE, DO NOT DELETE, PLEASE !!!
    //      IDownloadMediaOptions options = new MediaOptionsBuilder().Set
    //        .SetDisplayAsThumbnail(true)
    //        .Build();
    //
    //      var request = ItemSSCRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/testname222")
    //        .DownloadOptions(options)
    //        .Database("master")
    //        .Build();
    //        
    //      var response = await this.sessionAuthenticatedUser.DownloadMediaResourceAsync(request);
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
      testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
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
    public void TestGetItemByIdWithPathInParamsReturnsError()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemSSCRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Path).Build());
      Assert.AreEqual("ReadItemByIdRequestBuilder.ItemId : Item id must have curly braces '{}'", exception.Message);
    }

    [Test]
    public async void TestGetItemByPath()
    {
      var response = await GetItemByPath(testData.Items.Home.Path);

      testData.AssertItemsCount(1, response);
      testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
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
      testData.AssertItemsAreEqual(expectedItem, response[0]);
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
      testData.AssertItemsAreEqual(expectedItem, response[0]);

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
      testData.AssertItemsAreEqual(expectedItem, response[0]);
    }

    [Test]
    public async void TestGetItemByInternationalPath()
    {
      const string ItemInterationalPath = "/sitecore/content/Home/Android/Static/Japanese/宇都宮/ではまた明日";
      var response = await GetItemByPath(ItemInterationalPath);
      var expectedItem = new TestEnvironment.Item {
        DisplayName = "ではまた明日",
        Path = ItemInterationalPath,
        Template = testData.Items.Home.Template
      };
      testData.AssertItemsAreEqual(expectedItem, response[0]);
    }

    [Test]
    public void TestGetItemByNullIdReturnsError()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemById(null);
        await task;
      };

      var exception = Assert.Throws<ArgumentNullException>(testCode);
      Assert.IsTrue(exception.Message.Contains("ReadItemByIdRequestBuilder.ItemId"));
    }

    [Test]
    public void TestGetItemByNullPathReturnsError()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemByPath(null);
        await task;
      };

      var exception = Assert.Throws<ArgumentNullException>(testCode);
      Assert.IsTrue(exception.Message.Contains("ReadItemByPathRequestBuilder.ItemPath"));
    }

    [Test]
    public void TestGetItemByEmptyPathReturnsError()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemByPath("");
        await task;
      };

      var exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("ReadItemByPathRequestBuilder.ItemPath : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestGetItemByIdWithSpacesOnlyReturnsError()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemById(" ");
        await task;
      };

      var exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("ReadItemByIdRequestBuilder.ItemId : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestGetItemByPathWithSpacesOnlyReturnsError()
    {
      TestDelegate testCode = async () =>
      {
        var task = this.GetItemByPath("  ");
        await task;
      };

      var exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("ReadItemByPathRequestBuilder.ItemPath : The input cannot be empty.", exception.Message);
    }

    [Test]
    public async void TestGetItemByPathWithUserWithoutReadAccessToHomeItem()
    {
      var sessionWithoutAccess =
        SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.NoReadUserExtranet)
          .BuildReadonlySession();

      var request = ItemSSCRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path).Build();
      var response = await sessionWithoutAccess.ReadItemAsync(request);

      testData.AssertItemsCount(0, response);
    }

    private async Task<ScItemsResponse> GetItemById(string id)
    {
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithId(id).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);
      return response;
    }

    private async Task<ScItemsResponse> GetItemByPath(string itemPath)
    {
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithPath(itemPath).Build();
      var response = await this.sessionAuthenticatedUser.ReadItemAsync(request);
      return response;
    }

  }
}