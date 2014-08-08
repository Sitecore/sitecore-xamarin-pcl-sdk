namespace Sitecore.MobileSDK.API.MediaItem
{
  using Sitecore.MobileSDK.Media;

  public class MediaOptionsBuilder
  {
    public IMediaOptionsBuilder Set
    {
      // @adk : as suggested in the sample
      // http://stackoverflow.com/questions/1622662/creating-api-that-is-fluent

      get
      {
        return new MediaOptionsBuilderImpl();
      }
    }
  }
}

