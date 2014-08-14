namespace Sitecore.MobileSDK.API.MediaItem
{
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Media;

  /// <summary>
  /// Builder class for constructing <see cref="IDownloadMediaOptions" />
  /// </summary>
  public class MediaOptionsBuilder
  {
    /// <summary>
    /// Gets builder for <see cref="IDownloadMediaOptions" />.
    /// </summary>
    /// <value>
    /// The builder.
    /// </value>
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

