namespace Sitecore.MobileSDK.API.MediaItem
{
  using System;

  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;


  public class MediaOptionsBuilder
  {
    public MediaOptionsBuilder()
    {
      options = new DownloadMediaOptions();
    }

    public MediaOptionsBuilder Width(int width)
    {
      options.SetWidth(width);
      return this;
    }

    public MediaOptionsBuilder Height(int height)
    {
      options.SetHeight(height);
      return this;
    }

    public MediaOptionsBuilder MaxWidth(int maxWidth)
    {
      options.SetMaxWidth(maxWidth);
      return this;
    }

    public MediaOptionsBuilder MaxHeight(int maxHeight)
    {
      options.SetMaxHeight(maxHeight);
      return this;
    }

    public MediaOptionsBuilder BackgroundColor(string color)
    {
      options.SetBackgroundColor(color);
      return this;
    }

    public MediaOptionsBuilder DisableMediaCache(bool value)
    {
      options.SetDisableMediaCache(value);
      return this;
    }

    public MediaOptionsBuilder AllowStrech(bool value)
    {
      options.SetAllowStrech(value);
      return this;
    }

    public MediaOptionsBuilder Scale(float scale)
    {
      options.SetScale(scale);
      return this;
    }

    public MediaOptionsBuilder DisplayAsThumbnail(bool value)
    {
      options.SetDisplayAsThumbnail(value);
      return this;
    }

    public IDownloadMediaOptions Build()
    {
      if (null == this.options)
      {
        throw new InvalidOperationException("[MediaOptionsBuilder] Nothing to build since no options have been specified by the user");
      }

      return this.options;
    }

    private DownloadMediaOptions options;
  }
}

