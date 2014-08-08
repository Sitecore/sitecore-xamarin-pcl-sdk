namespace Sitecore.MobileSDK.Media
{
  using Sitecore.MobileSDK.API.MediaItem;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using System;

  public class MediaOptionsBuilderImpl : IMediaOptionsBuilder
  {
    public MediaOptionsBuilderImpl()
    {
      this.options = new DownloadMediaOptions();
    }

    // before fix - as described in Wikipedia http://ru.wikipedia.org/wiki/Fluent_interface
    public IMediaOptionsBuilder Width(int width)
    {
      this.options.SetWidth(width);
      return this;
    }

    public IMediaOptionsBuilder Height(int height)
    {
      this.options.SetHeight(height);
      return this;
    }

    public IMediaOptionsBuilder MaxWidth(int maxWidth)
    {
      this.options.SetMaxWidth(maxWidth);
      return this;
    }

    public IMediaOptionsBuilder MaxHeight(int maxHeight)
    {
      this.options.SetMaxHeight(maxHeight);
      return this;
    }

    public IMediaOptionsBuilder BackgroundColor(string color)
    {
      this.options.SetBackgroundColor(color);
      return this;
    }

    public IMediaOptionsBuilder DisableMediaCache(bool value)
    {
      this.options.SetDisableMediaCache(value);
      return this;
    }

    public IMediaOptionsBuilder AllowStrech(bool value)
    {
      this.options.SetAllowStrech(value);
      return this;
    }

    public IMediaOptionsBuilder Scale(float scale)
    {
      this.options.SetScale(scale);
      return this;
    }

    public IMediaOptionsBuilder DisplayAsThumbnail(bool value)
    {
      this.options.SetDisplayAsThumbnail(value);
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

