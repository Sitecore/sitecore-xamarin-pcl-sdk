
namespace Sitecore.MobileSdkUnitTest
{
  using NUnit.Framework;
  using System;

  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  [TestFixture()]
  public class DownloadMediaOptionsTest
  {

    [Test()]
    public void TestCopyEmptyOptions()
    {
      DownloadMediaOptions options = new DownloadMediaOptions();
      DownloadMediaOptions copy = (DownloadMediaOptions)options.DeepCopyMediaDownloadOptions();
      Assert.True (copy.IsEmpty);
    }

    [Test()]
    public void TestCopyWithOptions()
    {
      DownloadMediaOptions options = new DownloadMediaOptions();
      options.SetWidth(100);
      options.SetScale(1.5f);

      DownloadMediaOptions copy = (DownloadMediaOptions)options.DeepCopyMediaDownloadOptions();
      Assert.AreEqual("100", copy.Width);
      Assert.AreEqual("1.5", copy.Scale);
    }

    [Test()]
    public void TestCopyIsIndependent()
    {
      DownloadMediaOptions options = new DownloadMediaOptions();
      options.SetWidth(100);
      options.SetScale(1.5f);

      DownloadMediaOptions copy = (DownloadMediaOptions)options.DeepCopyMediaDownloadOptions();

      options.SetWidth(200);

      Assert.AreEqual("100", copy.Width);
      Assert.AreEqual("1.5", copy.Scale);
    }

    [Test()]
    public void TestBoolHasProperValue()
    {
      DownloadMediaOptions options = new DownloadMediaOptions();
      options.SetDisplayAsThumbnail(true);
      options.SetDisableMediaCache(true);
      options.SetDisableMediaCache(false);

      Assert.AreEqual("1", options.DisplayAsThumbnail);
      Assert.AreEqual("0", options.DisableMediaCache);
    }

  }
}

