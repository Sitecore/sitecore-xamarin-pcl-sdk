using NUnit.Framework;
using System;
using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.UrlBuilder.MediaItem;

namespace Sitecore.MobileSdkUnitTest
{
    using Sitecore.MobileSDK.API;
    using Sitecore.MobileSDK.UrlBuilder.Rest;

  [TestFixture]
  public class ResourseUrlBuilderTest
  {
    MediaItemUrlBuilder builder;

    [SetUp]
    public void SetUp()
    {
      var restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();

      var sessionConfig = SessionConfig.NewAuthenticatedSessionConfig("http://test.host", "a", "b");

      ItemSource itemSource = ItemSource.DefaultSource();
      builder = new MediaItemUrlBuilder(restGrammar, sessionConfig, itemSource);
    }

    [TearDown]
    public void TearDown()
    {
      this.builder = null;
    }

    [Test]
    public void RequestBuilderTest()
    {

      IDownloadMediaOptions options = new MediaOptionsBuilder()
        .SetWidth(100)
        .SetHeight(200)
        .SetBackgroundColor("white")
        .SetDisplayAsThumbnail(true)
        .Build();

      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/1.png")
        .DownloadOptions(options)
        .Database("master")
        .Build();
      //      request.ItemSource.Database = "web";

      //ALR: Where is asserts?
    }

    [Test]
    public void TestInvalidPathException()
    {
      TestDelegate action = () => builder.BuildUrlStringForPath("sitecore/media library/1.png", null);
      var exception = Assert.Throws<ArgumentException>(action);
      Assert.True(exception.Message.Contains("Media path should begin with '/' or '~'"));
    }

    [Test]
    public void TestNullPathException()
    {
      TestDelegate action = () => builder.BuildUrlStringForPath(null, null);
      var exception = Assert.Throws<ArgumentNullException>(action);
      Assert.True(exception.Message.Contains("Media path cannot be null or empty"));
    }

    [Test]
    public void AbsolutePathWithoutOptionsTest()
    {
      string result = builder.BuildUrlStringForPath("/sitecore/media library/2/1.png", null);
      const string Expected = "http://test.host/~/media/2/1.png.ashx?db=web&la=en";

      Assert.AreEqual(Expected, result);
    }

    [Test]
    public void RelativePathTest()
    {
      string result = builder.BuildUrlStringForPath("/mediaXYZ/1.png", null);
      const string Expected = "http://test.host/~/media/mediaXYZ/1.png?db=web&la=en";

      Assert.AreEqual(Expected, result);
    }

    [Test]
    public void RelativePathAndExtensionTest()
    {
      string result = builder.BuildUrlStringForPath("/mediaXYZ/1.png.ashx", null);
      const string Expected = "http://test.host/~/media/mediaXYZ/1.png.ashx?db=web&la=en";

      Assert.AreEqual(Expected, result);
    }

    [Test]
    public void PathContaignMediaHookTest()
    {
      string result = builder.BuildUrlStringForPath("~/media/1", null);
      const string Expected = "http://test.host/~/media/1.ashx?db=web&la=en";

      Assert.AreEqual(Expected, result);
    }

    [Test]
    public void PathContaignMediaHookAndExtensionTest()
    {
      string result = builder.BuildUrlStringForPath("~/media/1.ashx", null);
      const string Expected = "http://test.host/~/media/1.ashx?db=web&la=en";

      Assert.AreEqual(Expected, result);
    }

    [Test]
    public void PathProperlyEscapedTest()
    {
      string result = builder.BuildUrlStringForPath("~/media/Images/test image", null);
      const string Expected = "http://test.host/~/media/Images/test%20image.ashx?db=web&la=en";

      Assert.AreEqual(Expected, result);
    }


    [Test]
    public void ResourceNameIsCaseSensitiveTest()
    {
      string result = builder.BuildUrlStringForPath("~/media/Images/SoMe ImAGe", null);
      const string Expected = "http://test.host/~/media/Images/SoMe%20ImAGe.ashx?db=web&la=en";

      Assert.AreEqual(Expected, result);
    }

    [Test]
    public void EmptyDownloadOptionsTest()
    {
      var options = new DownloadMediaOptions();

      string result = builder.BuildUrlStringForPath("~/media/1", options);
      const string Expected = "http://test.host/~/media/1.ashx?db=web&la=en";

      Assert.AreEqual(Expected, result);
    }

    [Test]
    public void CorrectDownloadOptionsTest()
    {
      var options = new DownloadMediaOptions();
      options.SetWidth(100);
      options.SetBackgroundColor("white");
      options.SetDisplayAsThumbnail(true);

      string result = builder.BuildUrlStringForPath("~/media/1/2", options);
      const string Expected = "http://test.host/~/media/1/2.ashx?w=100&bc=white&thn=1&db=web&la=en";

      Assert.AreEqual(Expected, result);
    }

    [Test]
    public void CorrectDownloadOptionsWithNullValuesTest()
    {
      var options = new DownloadMediaOptions();
      options.SetWidth(100);
      options.SetBackgroundColor("white");
      options.SetBackgroundColor(null);

      options.SetDisplayAsThumbnail(true);
      options.SetDisplayAsThumbnail(false);

      string result = builder.BuildUrlStringForPath("~/media/1", options);
      const string Expected = "http://test.host/~/media/1.ashx?w=100&db=web&la=en";

      Assert.AreEqual(Expected, result);
    }
    [Test]
    public void CorrectDownloadOptionsWithAllParamsTest()
    {
      var options = new DownloadMediaOptions();
      options.SetWidth(10);
      options.SetHeight(10);
      options.SetBackgroundColor("3F0000");
      options.SetAllowStrech(false);
      options.SetDisableMediaCache(false);
      options.SetDisplayAsThumbnail(true);
      options.SetMaxHeight(10);
      options.SetMaxWidth(10);
      options.SetScale(2.5f);

      string result = builder.BuildUrlStringForPath("~/media/1.png", options);
      const string Expected = "http://test.host/~/media/1.png?w=10&h=10&mw=10&mh=10&bc=3F0000&dmc=0&as=0&sc=2.5&thn=1&db=web&la=en";

      Assert.AreEqual(Expected, result);
    }

    [Test]
    public void CorrectDownloadOptionsWithScaleAndMaxWidthTest()
    {
      var options = new DownloadMediaOptions();
      options.SetScale(3.0005f);
      options.SetMaxWidth(10);


      string result = builder.BuildUrlStringForPath("~/media/1", options);
      const string Expected = "http://test.host/~/media/1.ashx?mw=10&sc=3.0005&db=web&la=en";

      Assert.AreEqual(Expected, result);
    }

    [Test]
    public void DownloadOptionsWithNegativeHeightTest()
    {
      var options = new DownloadMediaOptions();

      TestDelegate action = () => options.SetHeight(-4);
      var exception = Assert.Throws<ArgumentException>(action);
      Assert.True(exception.Message.Contains("height must be > 0"));
    }

    [Test]
    public void DownloadOptionsWithZeroMaxWidthTest()
    {
      var options = new DownloadMediaOptions();

      TestDelegate action = () => options.SetMaxWidth(0);
      var exception = Assert.Throws<ArgumentException>(action);
      Assert.True(exception.Message.Contains("maxWidth must be > 0"));
    }

    [Test]
    public void DownloadOptionsWithInvalidScaleTest()
    {
      var options = new DownloadMediaOptions();

      TestDelegate action = () => options.SetScale(-0.00f);
      var exception = Assert.Throws<ArgumentException>(action);
      Assert.True(exception.Message.Contains("scale must be > 0"));
    }

  }
}

