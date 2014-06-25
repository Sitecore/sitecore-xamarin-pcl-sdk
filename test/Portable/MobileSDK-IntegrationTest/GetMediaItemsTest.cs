namespace IntegrationTest_Desktop_NUnitLite
{
  using System;
  using System.IO;
  using NUnit.Framework;

  using System.Drawing;
  using MobileSDKIntegrationTest;
  using Sitecore.MobileSDK;
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
      const int Height = 1800;

      var options = new MediaOptionsBuilder()
       .SetScale(0.5f)
        .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/testname222")
        .DownloadOptions(options)
        .Build();

      using (Stream response = await this.session.DownloadResourceAsync(request))
      {
        using (Image image = Image.FromStream(response))
        {
          Assert.AreEqual(Height, image.Height);
        }
      }
      // //@elt - get bytes array from stream
      //Stream response = await this.session.DownloadResourceAsync(request);
      //MemoryStream ms = new MemoryStream();
      //response.CopyTo(ms);
      //byte[] data = ms.ToArray();
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
        //.Database("master")
        .Build();

      using (Stream response = await this.session.DownloadResourceAsync(request))
      {
        using (Image image = Image.FromStream(response))
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

      IDownloadMediaOptions options = new MediaOptionsBuilder()
        .SetHeight(Height)
        .SetWidth(Width)
        .SetAllowStrech(true)
        .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/kirkorov")
        .DownloadOptions(options)
        // .Database("master")
        .Build();

      using (Stream response = await this.session.DownloadResourceAsync(request))
      {
        using (Image image = Image.FromStream(response))
        {
          Assert.AreEqual(Height, image.Height);
          Assert.AreEqual(Width, image.Width);
        }
      }
    }

    [Test]
    public async void TestGetMediaItemWithPngExtension()
    {
      const int ExpectedHeight = 256;

      var options = new MediaOptionsBuilder()
       .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/mouse-icon")
        .DownloadOptions(options) //wili be failed until fix bug
        .Database("master")
        .Build();

      using (Stream response = await this.session.DownloadResourceAsync(request))
      {
        using (Image image = Image.FromStream(response))
        {
          Assert.AreEqual(ExpectedHeight, image.Height);
        }
      }
    }

    [Test]
    public async void TestGetMediaItemWithEmptyPath()
    {
      var options = new MediaOptionsBuilder()
        .SetAllowStrech(true)
        .Build();

      TestDelegate testCode = async () =>
      {
        var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("")
          .DownloadOptions(options)
          .Database("master");
        //.Build();
      };
      Assert.Throws<ArgumentNullException>(testCode);
    }

    [Test]
    public async void TestGetMediaItemWithNullPath()
    {
      var options = new MediaOptionsBuilder()
        .SetAllowStrech(true)
        .Build();

      TestDelegate testCode = async () =>
      {
        var request = ItemWebApiRequestBuilder.ReadMediaItemRequest(null)
          .DownloadOptions(options)
          .Database("master");
       // .Build();
      };
      Assert.Throws<ArgumentNullException>(testCode);
    }
  }
}
