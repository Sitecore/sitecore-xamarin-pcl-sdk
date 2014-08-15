namespace MobileSDKIntegrationTest
{
  using System;
  using System.Globalization;
  using System.IO;
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API.MediaItem;

  [TestFixture]
  public class GetMediaItemsTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiReadonlySession session;

    const string SitecoreMouseIconPath = "/sitecore/media library/images/mouse-icon";

    [SetUp]
    public void Setup()
    {
      this.testData = TestEnvironment.DefaultTestEnvironment();
      this.session =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          .BuildReadonlySession();
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
      var options = new MediaOptionsBuilder().Set
       .Scale(0.5f)
       .Build();

      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("/sitecore/media library/Images/testname222")
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
      var options = new MediaOptionsBuilder().Set
        .DisplayAsThumbnail(true)
        .Build();

      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("/sitecore/media library/Images/butterfly2_large")
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

      var options = new MediaOptionsBuilder().Set
        .Height(Height)
        .Width(Width)
        .AllowStrech(true)
        .Build();

      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("/sitecore/media library/Images/kirkorov")
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
      const string MediaPath = SitecoreMouseIconPath;
      const string Db = "master";

      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(MediaPath)
        .Database(Db)
        .Build();
      var response = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);

      var expectedItem = await this.GetItemByPath(MediaPath, Db);
      Assert.AreEqual(expectedItem["size"].RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public void TestGetMediaWithEmptyPathReturnsError()
    {
      TestDelegate testCode = () => ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("");
      var exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("ReadMediaItemRequestBuilder.MediaPath : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestGetMediaWithNullPathReturnsError()
    {
      TestDelegate testCode = () => ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(null);
      var exception = Assert.Throws<ArgumentNullException>(testCode);
      Assert.IsTrue(exception.Message.Contains("ReadMediaItemRequestBuilder.MediaPath"));
    }

    [Test]
    public void TestGetMediaWithNotExistentPathReturnsError()
    {
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("/sitecore/media library/images/not existent")
        .Build();

      TestDelegate testCode = async () =>
      {
        var task = this.session.DownloadResourceAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<LoadDataFromNetworkException>(testCode);
      Assert.IsTrue(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("System.Net.Http.HttpRequestException", exception.InnerException.GetType().ToString());

      // Windows : "Response status code does not indicate success: 404 (Not Found)"
      // iOS     : "404 (Not Found)"
      Assert.IsTrue(exception.InnerException.Message.Contains("Not Found"));
    }

    [Test]
    [Ignore]
    public void TestGetMediaWithPathBeginsWithoutSlashReturnsError()
    {
      TestDelegate testCode = () => ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("sitecore/media library/images/kirkorov");
      Exception exception = Assert.Throws<ArgumentException>(testCode);

      // @adk : unable to assert since session settings and "~/media" value should be shared.
      // Solutions
      // 1. Construct a builder using a session.
      // 2. Not validate this particular value at this stage.
      Assert.AreEqual("ReadMediaItemRequestBuilder.Path : Media path should begin with '/' or '~'", exception.Message);
    }

    [Test]
    public void TestGetMediaWithNegativeScaleValueReturnsError()
    {
      TestDelegate testCode = () => new MediaOptionsBuilder().Set
        .Scale(-2.0f)
        .Build();
      Exception exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("DownloadMediaOptions.Scale : scale must be > 0", exception.Message);
    }

    [Test]
    public void TestGetMediaWithNegativeMaxWidthValueReturnsError()
    {
      TestDelegate testCode = () => new MediaOptionsBuilder().Set
        .MaxWidth(-55)
        .Build();
      Exception exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("DownloadMediaOptions.MaxWidth : maxWidth must be > 0", exception.Message);
    }

    [Test]
    public void TestGetMediaWithNegativeHeightValueReturnsError()
    {
      TestDelegate testCode = () => new MediaOptionsBuilder().Set
        .Height(-55)
        .Build();
      Exception exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("DownloadMediaOptions.Height : height must be > 0", exception.Message);
    }

    [Test]
    public void TestGetMediaWithZeroWidthValueReturnsError()
    {
      TestDelegate testCode = () => new MediaOptionsBuilder().Set
        .Width(0)
        .Build();
      Exception exception = Assert.Throws<ArgumentException>(testCode);
      Assert.AreEqual("DownloadMediaOptions.Width : width must be > 0", exception.Message);
    }

    [Test]
    public void TestGetMediaFromInvalidImageReturnsError()
    {
      var options = new MediaOptionsBuilder().Set
        .Height(100)
        .Build();

      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("/sitecore/media library/Images/nexus")
       .DownloadOptions(options)
       .Build();

      TestDelegate testCode = async () =>
      {
        var task = this.session.DownloadResourceAsync(request);
        await task;
      };
      Exception exception = Assert.Throws<LoadDataFromNetworkException>(testCode);
      Assert.IsTrue(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("System.Net.Http.HttpRequestException", exception.InnerException.GetType().ToString());

      // Windows : "Response status code does not indicate success: 404 (Not Found)"
      // iOS     : "404 (Not Found)"
      Assert.IsTrue(exception.InnerException.Message.Contains("Not Found"));
    }

    [Test]
    public async void TestMediaWithoutAccessToFolder()
    {
      const string MediaPath = "/sitecore/media library/Images/kirkorov";
      var sessionNoReadAccess =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.NoReadAccess)
          .BuildReadonlySession();

      var options = new MediaOptionsBuilder().Set
        .Scale(1)
        .Build();
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(MediaPath)
        .DownloadOptions(options)
        .Build();

      var response = await sessionNoReadAccess.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);
      var expectedItem = await this.GetItemByPath(MediaPath);
      Assert.AreEqual(expectedItem["size"].RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestGetMediaWithAbsolutePath()
    {
      const string MediaPath = "/sitecore/media library/Images/testname222";
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(MediaPath)
        .Build();
      var response = await this.session.DownloadResourceAsync(request);

      var ms = new MemoryStream();
      response.CopyTo(ms);
      var expectedItem = await this.GetItemByPath(MediaPath);
      Assert.AreEqual(expectedItem["size"].RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestGetMediaWithRelativePath()
    {
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("/Images/green_mineraly1")
        .Build();
      var response = await this.session.DownloadResourceAsync(request);

      var ms = new MemoryStream();
      response.CopyTo(ms);

      var expectedItem = await this.GetItemByPath("/sitecore/media library/Images/green_mineraly1");
      Assert.AreEqual(expectedItem["size"].RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestGetItemWithTildaInPath()
    {
      var options = new MediaOptionsBuilder().Set
        .DisplayAsThumbnail(false)
        .AllowStrech(true)
        .Height(150)
        .Build();

      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/Images/green_mineraly1")
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
      const string ItemPath = "/sitecore/media library/Images/Files/pdf example";
      const string MediaPath = "~/media/Images/Files/pdf example.pdf";
      const string Db = "master";

      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(MediaPath)
        .Database(Db)
        .Build();

      var response = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);
      var expectedItem = await this.GetItemByPath(ItemPath, Db);
      Assert.AreEqual(expectedItem["size"].RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestGetMediaItemWithMp4Extension()
    {
      const string ItemPath = "/sitecore/media library/Images/Files/Video_01";
      const string MediaPath = "~/media/Images/Files/Video_01.mp4";
      const string Db = "master";

      var options = new MediaOptionsBuilder().Set
        .Height(50)
        .Build();

      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(MediaPath)
        .DownloadOptions(options)
        .Database(Db)
        .Build();

      var response = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);
      var expectedItem = await this.GetItemByPath(ItemPath, Db);
      Assert.AreEqual(expectedItem["size"].RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestGetMediaFromDifferentDb()
    {

      const string Path = SitecoreMouseIconPath;
      var requestFromMasterDb = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(Path)
        .Database("master")
        .Build();

      var responseFromMasterDb = await this.session.DownloadResourceAsync(requestFromMasterDb);
      var ms = new MemoryStream();
      responseFromMasterDb.CopyTo(ms);

      // @adk : changed since different size has been received 
      // * Mac OS
      // * IOS Simulator
      Assert.IsTrue(141750 == ms.Length);

      var requestFromWebDb = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(Path)
       .Database("web")
       .Build();

      TestDelegate testCode = async () =>
      {
        var task = this.session.DownloadResourceAsync(requestFromWebDb);
        await task;
      };

      Exception exception = Assert.Throws<LoadDataFromNetworkException>(testCode);
      Assert.IsTrue(exception.Message.Contains("Unable to download data from the internet"));
      Assert.AreEqual("System.Net.Http.HttpRequestException", exception.InnerException.GetType().ToString());

      // Windows : "Response status code does not indicate success: 404 (Not Found)"
      // iOS     : "404 (Not Found)"
      Assert.IsTrue(exception.InnerException.Message.Contains("Not Found"));
    }

    [Test]
    public async void TestGetMediaWithInternationalPath()
    {
      var options = new MediaOptionsBuilder().Set
        .Width(50)
        .Build();

      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/Images/files/では/flowers")
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
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("/images/test image")
        .Database("web")
        .Language("en")
        .Version(1)
        .Build();
      var response = await this.session.DownloadResourceAsync(request);

      var ms = new MemoryStream();
      response.CopyTo(ms);

      var expectedItem = await this.GetItemByPath("/sitecore/media library/images/test image");
      Assert.AreEqual(expectedItem["size"].RawValue, ms.Length.ToString(CultureInfo.InvariantCulture));
    }

    [Test]
    public async void TestMediaFromSrcAndMediapathInField()
    {
      var z = await this.GetMediaFieldAsStringArray("/sitecore/content/Home/Test fields");

      // z[5]: src="~/media/4F20B519D5654472B01891CB6103C667.ashx"
      var requestWithSrcParameter = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(z[5])
          .Build();
      var responseWithSrcParameter = await this.session.DownloadResourceAsync(requestWithSrcParameter);
      var msWithSrcParameter = new MemoryStream();
      responseWithSrcParameter.CopyTo(msWithSrcParameter);

      // z[3]: mediapath="/Images/test image"
      var requestWithMediapathParameter = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(z[3])
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

      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(z[3])   // z[3]: mediapath="/Images/test image"
         .Build();
      var responseWithMediapathParameter = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      responseWithMediapathParameter.CopyTo(ms);

      Assert.AreEqual(5257, ms.Length);
    }

    [Test] //ALR: Argument exception should appear
    public void TestGetMediaWithEmptyDatabaseDoNotReturnsException()
    {
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/test")
        .Database("")
        .Build();
      Assert.IsNotNull (request);
    }

    [Test] //ALR: Argument exception should appear
    public void TestGetMediaWithNullDatabaseDoNotReturnsException()
    {
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/test")
        .Database(null)
        .Build();
      Assert.IsNotNull (request);
    }

    [Test] //ALR: Argument exception should appear
    public void TestGetMediaWithSpacesInLanguageReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/test").Language("  "));
      Assert.AreEqual("ReadMediaItemRequestBuilder.Language : The input cannot be empty.", exception.Message);
    }

    [Test] //ALR: Argument exception should appear
    public void TestGetMediaWithNullLanguageDoNotReturnsException()
    {
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/test")
        .Language(null)
        .Build();
      Assert.IsNotNull(request);
    }

    [Test] //ALR: Argument exception should appear
    public void TestGetMediaWithZeroVersionReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/test").Version(0));
      Assert.AreEqual("ReadMediaItemRequestBuilder.Version : Positive number expected", exception.Message);
    }

    [Test] //ALR: Argument exception should appear
    public void TestGetMediaWithNegativeVersionReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentException>(() => ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/test").Version(-1));
      Assert.AreEqual("ReadMediaItemRequestBuilder.Version : Positive number expected", exception.Message);
    }


    [Test] //ALR: Argument exception should appear
    public void TestGetMediaWithNullVersionReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentNullException>(() => ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/test").Version(null));
      Assert.IsTrue(exception.Message.Contains("ReadMediaItemRequestBuilder.Version"));
    }

    [Test]
    public void TestGetMediaWithOverridenVersionReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/test")
        .Version(2)
        .Version(1)
        .Build());
      Assert.AreEqual("ReadMediaItemRequestBuilder.Version : Property cannot be assigned twice.", exception.Message);
    }

    [Test]
    public void TestGetMediaWithOverridenLanguageReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/test")
        .Language("en")
        .Language("da")
        .Build());
      Assert.AreEqual("ReadMediaItemRequestBuilder.Language : Property cannot be assigned twice.", exception.Message);
    }

    [Test]
    public void TestGetMediaWithOverridenDatabaseReturnsException()
    {
      Exception exception = Assert.Throws<InvalidOperationException>(() => ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("~/media/test")
        .Database("master")
        .Database("web")
        .Build());
      Assert.AreEqual("ReadMediaItemRequestBuilder.Database : Property cannot be assigned twice.", exception.Message);
    }

    //TODO: add tests for MediaOptions (null, empty, override)

    private async Task<string[]> GetMediaFieldAsStringArray(string path)
    {
      var expectedItem = await this.GetItemByPath(path);
      var str = expectedItem["image"].RawValue;
      var z = str.Split(new char[]
      {
        '\"'
      }, StringSplitOptions.RemoveEmptyEntries);
      return z;
    }

    private async Task<ISitecoreItem> GetItemByPath(string path, string db = null)
    {
      var requestBuilder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(path).Payload(PayloadType.Content);
      if (db != null)
      {
        requestBuilder.Database(db);
      }
      var request = requestBuilder.Build();
      var response = await this.session.ReadItemAsync(request);
      var item = response[0];
      return item;
    }
  }
}
