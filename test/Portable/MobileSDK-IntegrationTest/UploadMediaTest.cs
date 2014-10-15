namespace MobileSDKIntegrationTest
{
  using System.IO;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;


  [TestFixture]
  public class UploadMediaTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiSession session;
    const string GifImagePath = "\\\\TEST24DK1\\Resources\\media\\Pictures-2.gif";


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
      using (var image = GetStreamFromUrl(GifImagePath))
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

      const string PngImagePath = "\\\\TEST24DK1\\Resources\\media\\wpapers_ru_Бамбук.png";
      using (var image = GetStreamFromUrl(PngImagePath))
      {
        const string Database = "web";
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .Database(Database)
          .ItemName("testPNG")
          .FileName("test.png")
          .ContentType("image/png")
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

      using (var image = GetStreamFromUrl(GifImagePath))
      {
        const string Database = "web";
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .Database(Database)
          .ItemName("testPNG")
          .FileName("test.png")
          .ContentType("image/png")
          .Build();
        var response = await sessionToOverride.UploadMediaResourceAsync(request);
        Assert.AreEqual(1, response.ResultCount);
        Assert.AreEqual(Database, response[0].Source.Database);
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
      const string VideoPath = "\\\\TEST24DK1\\Resources\\media\\IMG_0994.MOV";
      using (var video = GetStreamFromUrl(VideoPath))
      {
        
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
          .ItemDataStream(video)
          .ItemName("testVideo")
          .FileName("test.mov")
          .ContentType("video/mov")
          .Build();
        var response = await sessionToOverride.UploadMediaResourceAsync(request);
        Assert.AreEqual(1, response.ResultCount);
        Assert.AreEqual(Database, response[0].Source.Database);
      }
    }

    [Test]
    public void UploadMediaToNullDbDoesNotReturnException()
    {
      var image = GetStreamFromUrl(GifImagePath);
      var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
        .ItemDataStream(image)
        .Database(null)
        .ItemName("testPNG")
        .FileName("test.png")
        .ContentType("image/png")
        .Build();

      Assert.NotNull(request);
    }

    [Test]
    public async void UploadImageWithVeryLongItemName()
    {
      await this.RemoveAll();
      using (var image = GetStreamFromUrl(GifImagePath))
      {
        const string Database = "master";
        const string ItemName = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium totam rem aperiam eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo Nemo enim ipsam voluptatem quia voluptas si aspernatur aut odit aut fugit sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt neque porro quisquam est";
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId(testData.Items.UploadMediaHere.Id)
          .ItemDataStream(image)
          .Database(Database)
          .ItemName(ItemName)
          .FileName("test.png")
          .ContentType("image/png")
          .Build();
        var response = await this.session.UploadMediaResourceAsync(request);
        Assert.AreEqual(0, response.ResultCount);
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
      const string JpgImagePath = "\\\\TEST24DK1\\Resources\\media\\30X30.jpg";
      using (var image = GetStreamFromUrl(JpgImagePath))
      {
        const string ItemName = "International Слава Україні ウクライナへの栄光";
        const string Database = "master";
        string parentPath = createResponse[0].Path.Remove(0, 23);
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath(parentPath)
          .ItemDataStream(image)
          .Database(Database)
          .ItemName(ItemName)
          .ContentType("image/gif")
          .FileName("ク.gif")
          .Build();
        var response = await this.session.UploadMediaResourceAsync(request);

        Assert.AreEqual(1, response.ResultCount);
        Assert.AreEqual(ItemName, response[0].DisplayName);
        this.AssertImageUploaded(response[0].Path, Database);
      }
    }


    private static Stream GetStreamFromUrl(string url)
    {
      return File.OpenRead(url);
    }

    private async void AssertImageUploaded(string itemPath, string database)
    {
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(itemPath)
        .Database(database)
        .Build();

      using (var response = await this.session.DownloadMediaResourceAsync(request))
      using (var ms = new MemoryStream())
      {
        await response.CopyToAsync(ms);
        Assert.IsTrue(ms.Length > 0);
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
