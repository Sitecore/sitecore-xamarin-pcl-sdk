namespace Sitecore.MobileSDK.API.MediaItem
{
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Media;

  /// <summary>
  /// A placeholder class to make the fluent interface more human friendly.
  /// Returns an instance of <seealso cref="IDownloadMediaOptions" /> that accumulates the user's input.
  /// 
  /// </summary>
  public class MediaOptionsBuilder
  {
    /// <summary>
    /// A stub method returning an actual implementation of <seealso cref="IDownloadMediaOptions" />.
    /// </summary>
    /// <value>
    /// The actual builder.
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

