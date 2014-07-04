namespace MobileSDKIntegrationTest
{
  using System;
  using System.Globalization;
  using System.IO;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  [TestFixture]
  public class GetMediaTest
  {
    private TestEnvironment testData;
    private ScApiSession session;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.session = null;
    }

    [Test]
    public async void TestGetMediaWithScale()
    {
      var options = new MediaOptionsBuilder()
       .SetScale(0.5f)
        .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/testname222")
        .DownloadOptions(options)
        .Build();
      var response = await this.session.DownloadResourceAsync(request);

      var ms = new MemoryStream();
      response.CopyTo(ms);
      Assert.IsTrue(8000 > ms.Length);
    }

    [Test]
    public async void TestGetMediaAsThumbnail()
    {
      var options = new MediaOptionsBuilder()
        .SetDisplayAsThumbnail(true)
        .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/butterfly2_large")
        .DownloadOptions(options)
        .Build();

      var response = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);
      Assert.IsTrue(43000 > ms.Length);
    }

    [Test]
    public async void TestGetMediaWithHeightAndWidthAndAllowSrtech()
    {
      const int Height = 200;
      const int Width = 300;

      var options = new MediaOptionsBuilder()
        .SetHeight(Height)
        .SetWidth(Width)
        .SetAllowStrech(true)
        .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/kirkorov")
        .DownloadOptions(options)
        .Build();

      var response = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);
      Assert.IsTrue(14300 > ms.Length);
    }

    [Test]
    public async void TestGetMediaWithPngExtension()
    {
      const string MediaPath = "/sitecore/media library/Images/mouse-icon";
      const string Db = "master";

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest(MediaPath)
        .Database(Db)
        .Build();
      var response = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);

      var expectedItem = await this.GetItemByPath(MediaPath, Db);
      Assert.AreEqual(expectedItem.FieldWithName("size").RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public void TestGetMediaWithEmptyPath()
    {
      TestDelegate testCode = () => ItemWebApiRequestBuilder.ReadMediaItemRequest("");
      var exception = Assert.Throws<ArgumentNullException>(testCode);
      Assert.True(exception.GetBaseException().ToString().Contains("Media path cannot be null or empty"));
    }

    [Test]
    public void TestGetMediaWithNullPath()
    {
      TestDelegate testCode = () => ItemWebApiRequestBuilder.ReadMediaItemRequest(null);
      var exception = Assert.Throws<ArgumentNullException>(testCode);
      Assert.True(exception.Message.Contains("Media path cannot be null or empty"));
    }

    [Test]
    public void TestGetMediaWithNotExistentPath()
    {
      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/images/not existent")
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = this.session.DownloadResourceAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<LoadDataFromNetworkException>(testCode);
      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("System.Net.Http.HttpRequestException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Response status code does not indicate success: 404 (Not Found)"));
    }

    [Test]
    public void TestGetMediaWithPathBeginsWithoutSlash()
    {
      TestDelegate testCode = () => ItemWebApiRequestBuilder.ReadMediaItemRequest("sitecore/media library/images/kirkorov");
      Exception exception = Assert.Throws<ArgumentException>(testCode);
      Assert.True(exception.Message.Contains("Media path should begin with '/' or '~'"));
    }

    [Test]
    public void TestGetMediaWithNegativeScaleValue()
    {
      TestDelegate testCode = () =>
      {
        var options = new MediaOptionsBuilder()
         .SetScale(-2.0f)
         .Build();
      };
      Exception exception = Assert.Throws<ArgumentException>(testCode);
      Assert.True(exception.Message.Contains("scale must be > 0"));
    }

    [Test]
    public void TestGetMediaWithNegativeMaxWidthValue()
    {
      TestDelegate testCode = () =>
      {
        var options = new MediaOptionsBuilder()
         .SetMaxWidth(-55)
         .Build();
      };
      Exception exception = Assert.Throws<ArgumentException>(testCode);
      Assert.True(exception.Message.Contains("maxWidth must be > 0"));
    }

    [Test]
    public void TestGetMediaWithNegativeHeightValue()
    {
      TestDelegate testCode = () =>
      {
        var options = new MediaOptionsBuilder()
           .SetHeight(-55)
           .Build();
      };
      Exception exception = Assert.Throws<ArgumentException>(testCode);
      Assert.True(exception.Message.Contains("height must be > 0"));
    }

    [Test]
    public void TestGetMediaWithZeroWidthValue()
    {
      TestDelegate testCode = () =>
      {
        var options = new MediaOptionsBuilder()
         .SetWidth(0)
         .Build();
      };
      Exception exception = Assert.Throws<ArgumentException>(testCode);
      Assert.True(exception.Message.Contains("width must be > 0"));
    }

    [Test]
    public void TestGetMediaFromUploadedImageWithError()
    {
      var options = new MediaOptionsBuilder()
        .SetHeight(100)
        .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/nexus")
       .DownloadOptions(options)
       .Build();

      TestDelegate testCode = async () =>
      {
        var task = this.session.DownloadResourceAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<LoadDataFromNetworkException>(testCode);
      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("System.Net.Http.HttpRequestException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Response status code does not indicate success: 404 (Not Found)"));
    }

    [Test]
    public async void TestMediaWithoutAccessToFolder()
    {
      const string MediaPath = "/sitecore/media library/Images/kirkorov";
      var sessionNoReadAccess = testData.GetSession(testData.InstanceUrl, testData.Users.NoReadAccess.Username, testData.Users.NoReadAccess.Password);
      var options = new MediaOptionsBuilder()
        .SetScale(1)
        .Build();
      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest(MediaPath)
        .DownloadOptions(options)
        .Build();

      var response = await sessionNoReadAccess.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);
      var expectedItem = await this.GetItemByPath(MediaPath);
      Assert.AreEqual(expectedItem.FieldWithName("size").RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestGetMediaWithAbsolutePath()
    {
      const string MediaPath = "/sitecore/media library/Images/testname222";
      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest(MediaPath)
        .Build();
      var response = await this.session.DownloadResourceAsync(request);

      var ms = new MemoryStream();
      response.CopyTo(ms);
      var expectedItem = await this.GetItemByPath(MediaPath);
      Assert.AreEqual(expectedItem.FieldWithName("size").RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestGetMediaWithRelativePath()
    {
      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/Images/green_mineraly1.jpg")
        .Build();
      var response = await this.session.DownloadResourceAsync(request);

      var ms = new MemoryStream();
      response.CopyTo(ms);

      var expectedItem = await this.GetItemByPath("/sitecore/media library/Images/green_mineraly1");
      Assert.AreEqual(expectedItem.FieldWithName("size").RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestGetItemWithTildaInPath()
    {
      var options = new MediaOptionsBuilder()
        .SetDisplayAsThumbnail(false)
        .SetAllowStrech(true)
        .SetHeight(150)
        .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("~/media/Images/green_mineraly1")
        .DownloadOptions(options)
        .Build();
      var response = await this.session.DownloadResourceAsync(request);

      var ms = new MemoryStream();
      response.CopyTo(ms);
      Assert.AreEqual(16284, ms.Length);
    }

    [Test]
    public async void TestGetMediaWithPdfExtension()
    {
      const string MediaPath = "/sitecore/media library/Files/pdf example.pdf";
      const string Db = "master";
      var options = new MediaOptionsBuilder()
         .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest(MediaPath)
        .DownloadOptions(options)
        .Database(Db)
        .Build();

      var response = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);
      var expectedItem = await this.GetItemByPath(MediaPath, Db);
      Assert.AreEqual(expectedItem.FieldWithName("size").RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestGetMediaItemWithMp4Extension()
    {
      const string MediaPath = "/sitecore/media library/Files/Video_01.mp4";
      const string Db = "master";

      var options = new MediaOptionsBuilder()
      .SetHeight(50)
         .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest(MediaPath)
        .DownloadOptions(options)
        .Database(Db)
        .Build();

      var response = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);
      var expectedItem = await this.GetItemByPath(MediaPath, Db);
      Assert.AreEqual(expectedItem.FieldWithName("size").RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestGetMediaFromDifferentDb()
    {
      const string Path = "/sitecore/media library/images/mouse-icon";
      var requestFromMasterDb = ItemWebApiRequestBuilder.ReadMediaItemRequest(Path)
        .Database("master")
        .Build();

      var responseFromMasterDb = await this.session.DownloadResourceAsync(requestFromMasterDb);
      var ms = new MemoryStream();
      responseFromMasterDb.CopyTo(ms);

      Assert.IsTrue(187000 > ms.Length);

      var requestFromWebDb = ItemWebApiRequestBuilder.ReadMediaItemRequest(Path)
       .Database("web")
       .Build();

      TestDelegate testCode = async () =>
      {
        var task = this.session.DownloadResourceAsync(requestFromWebDb);
        await task;
      };
      Exception exception = Assert.Throws<LoadDataFromNetworkException>(testCode);
      Assert.True(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("System.Net.Http.HttpRequestException", exception.InnerException.GetType().ToString());
      Assert.True(exception.InnerException.Message.Contains("Response status code does not indicate success: 404 (Not Found)"));
    }

    [Test]
    public async void TestGetMediaWithInternationalPath()
    {
      var options = new MediaOptionsBuilder()
        .SetWidth(50)
        .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("~/media/files/では/flowers")
        .DownloadOptions(options)
        .Database("master")
        .Build();
      var response = await this.session.DownloadResourceAsync(request);

      var ms = new MemoryStream();
      response.CopyTo(ms);
      Assert.AreEqual(7654, ms.Length);
    }

    [Test]
    public async void TestGetMediaWithLanguageAndVersion()
    {
      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/images/test image")
        .Language("en")
        .Version("1")
        .Build();
      var response = await this.session.DownloadResourceAsync(request);

      var ms = new MemoryStream();
      response.CopyTo(ms);

      var expectedItem = await this.GetItemByPath("/sitecore/media library/images/test image");
      Assert.AreEqual(expectedItem.FieldWithName("size").RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestMediaFromSrcAndMediapathInField()
    {
      var z = await this.GetMediaFieldAsStringArray("/sitecore/content/Home/Test fields");

      // z[5]: src="~/media/4F20B519D5654472B01891CB6103C667.ashx"
      var requestWithSrcParameter = ItemWebApiRequestBuilder.ReadMediaItemRequest(z[5])
          .Build();
      var responseWithSrcParameter = await this.session.DownloadResourceAsync(requestWithSrcParameter);
      var msWithSrcParameter = new MemoryStream();
      responseWithSrcParameter.CopyTo(msWithSrcParameter);

      // z[3]: mediapath="/Images/test image"
      var requestWithMediapathParameter = ItemWebApiRequestBuilder.ReadMediaItemRequest(z[3])
         .Build();
      var responseWithMediapathParameter = await this.session.DownloadResourceAsync(requestWithMediapathParameter);
      var msWithMediapathParameter = new MemoryStream();
      responseWithMediapathParameter.CopyTo(msWithMediapathParameter);

      Assert.AreEqual(msWithSrcParameter, msWithMediapathParameter);
    }

    [Test]
    public async void TestMediaFromField()
    {
      var z = await this.GetMediaFieldAsStringArray("/sitecore/content/Home/Test fields");

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest(z[3])   // z[3]: mediapath="/Images/test image"
         .Build();
      var responseWithMediapathParameter = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      responseWithMediapathParameter.CopyTo(ms);

      Assert.AreEqual(5257, ms.Length);
    }

    private async Task<string[]> GetMediaFieldAsStringArray(string path)
    {
      var expectedItem = await this.GetItemByPath(path);
      var str = expectedItem.FieldWithName("image").RawValue;
      var z = str.Split(new char[]
      {
        '\"'
      }, StringSplitOptions.RemoveEmptyEntries);
      return z;
    }

    private async Task<ISitecoreItem> GetItemByPath(string path, string db = null)
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(path).Payload(PayloadType.Content).Database(db).Build();
      var response = await this.session.ReadItemAsync(request);
      var item = response.Items[0];
      return item;
    }
  }
}    
