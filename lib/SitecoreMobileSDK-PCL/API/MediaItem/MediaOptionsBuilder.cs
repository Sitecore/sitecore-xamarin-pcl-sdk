

namespace Sitecore.MobileSDK.API.MediaItem
{
  using System;

  using Sitecore.MobileSDK.Media;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;


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

