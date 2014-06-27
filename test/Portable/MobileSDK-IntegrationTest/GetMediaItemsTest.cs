namespace IntegrationTest_Desktop_NUnitLite
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Net.Configuration;
  using NUnit.Framework;

  using System.Drawing;
  using MobileSDKIntegrationTest;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Exceptions;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  [TestFixture]
  public class GetMediaItemsTest
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
    public async void TestGetMediaItemWithScaleValue()
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
     // byte[] data = ms.ToArray();
      Assert.IsTrue(8000> ms.Length);
    }

    [Test]
    public async void TestGetMediaItemAsThumbnail()
    {
      const int HeightAsThumbnail = 150;
      const int WidthAsThumbnail = 150;

      var options = new MediaOptionsBuilder()
        .SetDisplayAsThumbnail(true)
        .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/butterfly2_large")
        .DownloadOptions(options)
        .Build();

      using (var response = await this.session.DownloadResourceAsync(request))
      {
        using (var image = Image.FromStream(response))
        {
          Assert.AreEqual(HeightAsThumbnail, image.Height);
          Assert.AreEqual(WidthAsThumbnail, image.Width);
        }
      }
    }

    [Test]
    public async void TestGetMediaItemWithHeightAndWidth()
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

      using (var response = await this.session.DownloadResourceAsync(request))
      {
        using (var image = Image.FromStream(response))
        {
          Assert.AreEqual(Height, image.Height);
          Assert.AreEqual(Width, image.Width);
        }
      }
    }

    [Test]
    public async void TestGetMediaItemWithPngExtension()
    {
    var options = new MediaOptionsBuilder()
       .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/mouse-icon")
        .DownloadOptions(options)
        .Database("master")
        .Build();

      var response = await this.session.DownloadResourceAsync(request);
      var ms = new MemoryStream();
      response.CopyTo(ms);
      Assert.IsTrue(15000 > ms.Length);
    }

    [Test]
    public void TestGetMediaItemWithEmptyPath()
    {
      var options = new MediaOptionsBuilder()
        .SetAllowStrech(true)
        .Build();

      TestDelegate testCode = () =>
      {
        var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("")
         .DownloadOptions(options)
         .Database("master");
      };
      var exception = Assert.Throws<ArgumentNullException>(testCode);
      Assert.True(exception.GetBaseException().ToString().Contains("Media path cannot be null or empty"));
    }

    [Test]
    public void TestGetMediaItemWithNullPath()
    {
      var options = new MediaOptionsBuilder()
        .SetAllowStrech(true)
        .Build();

      TestDelegate testCode = () =>
      {
        var request = ItemWebApiRequestBuilder.ReadMediaItemRequest(null)
          .DownloadOptions(options)
          .Database("master")
          .Build();
      };
      var exception = Assert.Throws<ArgumentNullException>(testCode);
      Assert.True(exception.Message.Contains("Media path cannot be null or empty"));
    }

    [Test]
    public void TestGetMediaItemWithNotExistentPath()
    {
      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/images/not existent")
        .Database("master")
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
    public void TestGetMediaItemWithPathBeginsWithoutSlash()
    {
      var options = new MediaOptionsBuilder()
         .SetHeight(100)
         .Build();

      TestDelegate testCode = () =>
      {
        var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("sitecore/media library/images/kirkorov")
          .DownloadOptions(options)
          .Database("master");
      };
      Exception exception = Assert.Throws<ArgumentException>(testCode);
      Assert.True(exception.Message.Contains("Media path should begin with '/' or '~'"));
    }

    [Test]
    public void TestGetMediaItemWithNegativeScaleValue()
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
    public void TestGetMediaItemWithNegativeMaxWidthValue()
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
    public void TestGetMediaItemWithNegativeHeightValue()
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
    public void TestGetMediaItemWithZeroWidthValue()
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
    public void TestGetMediaItemFromUploadedImageWithError()
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
    public async void TestMediaItemWithoutAccessToFolder()
    {
      const int Height = 100;
      var sessionNoReadAccess = testData.GetSession(testData.InstanceUrl, testData.Users.NoReadAccess.Username, testData.Users.NoReadAccess.Password);
      var options = new MediaOptionsBuilder()
        .SetHeight(Height)
        .Build();
      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/kirkorov")
        .DownloadOptions(options)
        .Build();

      var response = await sessionNoReadAccess.DownloadResourceAsync(request);
       var ms = new MemoryStream();
      response.CopyTo(ms);
      Assert.IsTrue(5000>ms.Length);
    }

    [Test]
    public async void TestMediaItemFromField()
    {
      string str = "Extension methods have all the capabilities of regular static methods.";
      int first = str.IndexOf("methods");
    }

  }
}
