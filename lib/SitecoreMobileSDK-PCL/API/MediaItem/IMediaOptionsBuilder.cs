namespace Sitecore.MobileSDK.API.MediaItem
{
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface IMediaOptionsBuilder
  {
    IMediaOptionsBuilder Width(int width);
    IMediaOptionsBuilder Height(int height);
    IMediaOptionsBuilder MaxWidth(int maxWidth);
    IMediaOptionsBuilder MaxHeight(int maxHeight);
    IMediaOptionsBuilder BackgroundColor(string color);
    IMediaOptionsBuilder DisableMediaCache(bool value);
    IMediaOptionsBuilder AllowStrech(bool value);
    IMediaOptionsBuilder Scale(float scale);
    IMediaOptionsBuilder DisplayAsThumbnail(bool value);

    IDownloadMediaOptions Build();
  }
}

