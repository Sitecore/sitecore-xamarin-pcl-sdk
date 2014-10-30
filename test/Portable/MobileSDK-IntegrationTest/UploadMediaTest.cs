namespace MobileSDKIntegrationTest
{
  using System;
  using System.IO;
  using System.Net;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class UploadMediaTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiSession session;

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          .BuildSession();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.session.Dispose();
      this.session = null;
    }

    [Test]
    public async void UploadGifFileToMasterDatabaseAsSitecoreAdminForParentPath()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        const string ItemName = "testGif";
        const string Database = "master";

        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath(testData.Items.UploadMediaHere.Path)
          .ItemDataStream(image)
          .Database(Database)
          .ItemName(ItemName)
          .FileName("test.gif")
          .ContentType("image/jpg")
          .ItemTemplatePath("System/Media/Unversioned/Jpeg")
          .Build();

        var response = await this.session.UploadMediaResourceAsync(request);

        Assert.AreEqual(1, response.ResultCount);
        Assert.AreEqual(ItemName, response[0].DisplayName);
        Assert.AreEqual(Database, response[0].Source.Database);
        this.AssertImageUploaded(response[0].Path, Database);
      }
    }

    [Test]
    public async void UploadPngImageToWebDatabaseAsSitecoreAdminForParentId()
    {
      await this.RemoveAll();

      using (var image = GetStreamFromUrl(TestEnvironment.Images.Png.Bambuk))
      {
        const string Database = "web";
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .Database(Database)
          .ItemName("testPNG")
          .FileName("test.png")
          .Build();
        var response = await this.session.UploadMediaResourceAsync(request);
        Assert.AreEqual(1, response.ResultCount);
        Assert.AreEqual(Database, response[0].Source.Database);
        this.AssertImageUploaded(response[0].Path, Database);
      }
    }

    [Test]
    public async void OverrideDatabaseInUploadRequest()
    {
      await this.RemoveAll();

      var sessionToOverride =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          .DefaultDatabase("master")
          .BuildSession();

      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        const string Database = "web";
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .Database(Database)
          .ItemName("testPNG")
          .FileName("test.png")
          .Build();
        var response = await sessionToOverride.UploadMediaResourceAsync(request);
        Assert.AreEqual(1, response.ResultCount);
        Assert.AreEqual(Database, response[0].Source.Database);
      }
    }

    [Test]
    public async void TryToUploadImageAsAnonymousUser()
    {
      await this.RemoveAll();

      var sessionToOverride =
        SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(this.testData.InstanceUrl)
          .BuildSession();

      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .ItemName("testPNG")
          .FileName("test.png")
          .Build();
        TestDelegate testCode = async () =>
        {
          var task = sessionToOverride.UploadMediaResourceAsync(request);
          await task;
        };

        Exception exception = Assert.Throws<ParserException>(testCode);
        Assert.AreEqual("[Sitecore Mobile SDK] Data from the internet has unexpected format", exception.Message);
        Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
        Assert.IsTrue(exception.InnerException.Message.Contains("Access denied"));
      }
    }

    [Test]
    public async void UploadBigSizeVideoToDbSpecifiedInSession()
    {
      await this.RemoveAll();
      const string Database = "master";
      var sessionToOverride =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          .DefaultDatabase(Database)
          .BuildSession();

      using (var video = GetStreamFromUrl(TestEnvironment.Videos.IMG_0994_MOV))
      {
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
          .ItemDataStream(video)
          .ItemName("testVideo")
          .FileName("test.mov")
          .Build();
        var response = await sessionToOverride.UploadMediaResourceAsync(request);
        Assert.AreEqual(1, response.ResultCount);
        Assert.AreEqual(Database, response[0].Source.Database);
        this.AssertImageUploaded(response[0].Path, Database);
      }
    }


    [Test]
    public void UploadMediaToNullDbDoesNotReturnException()
    {
      var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2);
      var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
        .ItemDataStream(image)
        .Database(null)
        .ItemName("testPNG")
        .FileName("test.png")
        .Build();

      Assert.NotNull(request);
    }

    [Test]
    public async void UploadImageWithVeryLongItemName()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        const string Database = "master";
        const string ItemName = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium totam rem aperiam eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo Nemo enim ipsam voluptatem quia voluptas si aspernatur aut odit aut fugit sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt neque porro quisquam est";
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .Database(Database)
          .ItemName(ItemName)
          .FileName("test.png")
          .Build();
        var response = await this.session.UploadMediaResourceAsync(request);
        Assert.AreEqual(0, response.ResultCount);
      }
    }

    [Test]
    public async void UploadImageToNotExistentFolder()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath("/not existent/folder")
          .ItemDataStream(image)
          .ItemName("test")
          .FileName("test.png")
          .Build();

        TestDelegate testCode = async () =>
        {
          var task = this.session.UploadMediaResourceAsync(request);
          await task;
        };

        Exception exception = Assert.Throws<ParserException>(testCode);
        Assert.AreEqual("[Sitecore Mobile SDK] Data from the internet has unexpected format", exception.Message);
        Assert.AreEqual("Sitecore.MobileSDK.API.Exceptions.WebApiJsonErrorException", exception.InnerException.GetType().ToString());
        Assert.AreEqual("The specified location not found.", exception.InnerException.Message);
      }
    }


    [Test]
    public async void UploadImageToNullParentItemIdReturnsNullReferenceException()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(null)
          .ItemDataStream(image)
          .ItemName("test")
          .FileName("test.png")
          .Build());
        Assert.IsTrue(exception.Message.Contains("ParentId"));
      }
    }

    [Test]
    public async void UploadImageToInvalidParentItemIdReturnsArgumentException()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentId("invalid-id")
          .ItemDataStream(image)
          .ItemName("test")
          .FileName("test.png")
          .Build());
        Assert.AreEqual("UploadMediaItemByParentIdRequestBuilder.ParentId : Item id must have curly braces '{}'", exception.Message);
      }
    }

    [Test]
    public async void UploadImageToEmptyParentPathDoesNotReturnException()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath("")
          .ItemDataStream(image)
          .ItemName("test")
          .FileName("test.png")
          .Build();
        Assert.NotNull(request);
      }
    }

    [Test]
    public async void UploadImageToEmptyParentIdReturnsArgumentException()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(" ")
          .ItemDataStream(image)
          .ItemName("test")
          .FileName("test.png")
          .Build());

        Assert.AreEqual("UploadMediaItemByParentIdRequestBuilder.ParentId : The input cannot be empty.", exception.Message);
      }
    }

    [Test]
    public async void UploadJpgFileWithInternationalItemNameAndParentPath()
    {
      await this.RemoveAll();

      //create international parent
      var createRequest = ItemWebApiRequestBuilder.CreateItemRequestWithParentId(this.testData.Items.UploadMediaHere.Id)
        .ItemTemplatePath(testData.Items.Home.Template)
        .ItemName("युक्रेन")
        .Database("master")
        .Build();
      var createResponse = await session.CreateItemAsync(createRequest);
      //Assert.AreEqual(1, createResponse.ResultCount);
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Jpeg._30x30))
      {
        const string ItemName = "International Слава Україні ウクライナへの栄光";
        const string Database = "master";
        string parentPath = createResponse[0].Path.Remove(0, 23);
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath(parentPath)
          .ItemDataStream(image)
          .Database(Database)
          .ItemName(ItemName)
          .FileName("ク.gif")
          .Build();
        var response = await this.session.UploadMediaResourceAsync(request);

        Assert.AreEqual(1, response.ResultCount);
        Assert.AreEqual(ItemName, response[0].DisplayName);
        this.AssertImageUploaded(response[0].Path, Database);
      }
    }

    [Test]
    public async void UploadImageWithEmptyItemNameReturnsArgumentException()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(this.testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .ItemName("")
          .FileName("test.png")
          .Build());
        Assert.AreEqual("UploadMediaItemByParentIdRequestBuilder.ItemName : The input cannot be empty.", exception.Message);
      }
    }

    [Test]
    public async void UploadImageWithNullItemDataStreamReturnsArgumentException()
    {
      await this.RemoveAll();

      var exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(this.testData.Items.UploadMediaHere.Id)
        .ItemDataStream(null)
        .ItemName("test")
        .FileName("test.png")
        .Build());
      Assert.IsTrue(exception.Message.Contains("ItemDataStream"));
    }

    [Test]
    public async void UploadImageToWith2ItemDataStreamsReturnsInvalidOperationException()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(this.testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .ItemDataStream(image)
          .ItemName("test")
          .FileName("test.png")
          .Build());
        Assert.AreEqual("UploadMediaItemByParentIdRequestBuilder.ItemDataStream : Property cannot be assigned twice.", exception.Message);
      }
    }

    [Test]
    public async void UploadImageToWith2ItemNamesReturnsInvalidOperationException()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath(this.testData.Items.UploadMediaHere.Path)
          .ItemDataStream(image)
          .ItemName("test")
          .ItemName("test2")
          .FileName("test.png")
          .Build());
        Assert.AreEqual("UploadMediaItemByParentPathRequestBuilder.ItemName : Property cannot be assigned twice.", exception.Message);
      }
    }

    [Test]
    public async void UploadImageToWith2ItemTemplatesReturnsInvalidOperationException()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(this.testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .ItemName("test")
          .FileName("test.png")
          .ItemTemplatePath("path1")
          .ItemTemplatePath("path2")
          .Build());
        Assert.AreEqual("UploadMediaItemByParentIdRequestBuilder.ItemTemplatePath : Property cannot be assigned twice.", exception.Message);
      }
    }

    [Test]
    public async void UploadImageToWith2FileNamesReturnsInvalidOperationException()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(this.testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .ItemName("test")
          .FileName("test1.png")
          .FileName("test2.png")
          .Build());
        Assert.AreEqual("UploadMediaItemByParentIdRequestBuilder.FileName : Property cannot be assigned twice.", exception.Message);
      }
    }

    [Test]
    public async void UploadImageToWith2DatabasesReturnsInvalidOperationException()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(TestEnvironment.Images.Gif.Pictures_2))
      {
        var exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath(this.testData.Items.UploadMediaHere.Path)
          .ItemDataStream(image)
          .ItemName("test")
          .FileName("test.png")
          .Database("db1")
          .Database("db2")
          .Build());
        Assert.AreEqual("UploadMediaItemByParentPathRequestBuilder.Database : Property cannot be assigned twice.", exception.Message);
      }
    }

    private Stream GetStreamFromUrl(string url)
    {

      WebRequest req = WebRequest.Create(url);
      return req.GetResponse().GetResponseStream();
    }

    private async void AssertImageUploaded(string itemPath, string database)
    {
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(itemPath)
        .Database(database)
        .Build();

      using (var response = await this.session.DownloadMediaResourceAsync(request))
      {
        Assert.IsTrue(response.Length > 0);
      }
    }

    private async Task<ScDeleteItemsResponse> DeleteAllItems(string database)
    {
      var request = ItemWebApiRequestBuilder.DeleteItemRequestWithId(this.testData.Items.UploadMediaHere.Id)
        .AddScope(ScopeType.Children)
        .Database(database)
        .Build();
      return await this.session.DeleteItemAsync(request);
    }

    private async Task<ScDeleteItemsResponse> RemoveAll()
    {
      await this.DeleteAllItems("master");
      return await this.DeleteAllItems("web");
    }
  }
}
