
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

      return base.DeepCopyMutableMediaDownloadOptions();
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

