
namespace SitecoreMobileSDKMockObjects
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

  // TODO : subclass from interface
  // and introduce write methods in sub-class only
  public class MockMutableMediaOptions : DownloadMediaOptions
  {
    public override IDownloadMediaOptions DeepCopyMutableMediaDownloadOptions()
    {
      ++this.copyConstructorInvocationCount;

      MockMutableMediaOptions result = new MockMutableMediaOptions();

      result.width = this.width;
      result.height = this.height;
      result.maxWidth = this.maxWidth;
      result.maxHeight = this.maxHeight;
      result.backgroundColor = this.backgroundColor;
      result.disableMediaCache = this.disableMediaCache;
      result.allowStrech = this.allowStrech;
      result.scale = this.scale;
      result.displayAsThumbnail = this.displayAsThumbnail;

      return result;
    }

    public int CopyConstructorInvocationCount 
    { 
      get
      {
        return this.copyConstructorInvocationCount;
      }
    }
    private int copyConstructorInvocationCount = 0;
  }
}

