namespace Sitecore.MobileSDK.API.MediaItem
{
  using System;

  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;


  public class MediaOptionsBuilder
  {
    public MediaOptionsBuilder()
    {
      this.options = new DownloadMediaOptions();
    }


    public MediaOptionsBuilder Set
    {
      // @adk : as suggested in the sample
      // http://stackoverflow.com/questions/1622662/creating-api-that-is-fluent

      get
      {
        return this;
      }
    }


    // before fix - as described in Wikipedia http://ru.wikipedia.org/wiki/Fluent_interface
    public MediaOptionsBuilder Width(int width)
    {
      this.options.SetWidth(width);
      return this;
    }

    public MediaOptionsBuilder Height(int height)
    {
      this.options.SetHeight(height);
      return this;
    }

    public MediaOptionsBuilder MaxWidth(int maxWidth)
    {
      this.options.SetMaxWidth(maxWidth);
      return this;
    }

    public MediaOptionsBuilder MaxHeight(int maxHeight)
    {
      this.options.SetMaxHeight(maxHeight);
      return this;
    }

    public MediaOptionsBuilder BackgroundColor(string color)
    {
      this.options.SetBackgroundColor(color);
      return this;
    }

    public MediaOptionsBuilder DisableMediaCache(bool value)
    {
      this.options.SetDisableMediaCache(value);
      return this;
    }

    public MediaOptionsBuilder AllowStrech(bool value)
    {
      this.options.SetAllowStrech(value);
      return this;
    }

    public MediaOptionsBuilder Scale(float scale)
    {
      this.options.SetScale(scale);
      return this;
    }

    public MediaOptionsBuilder DisplayAsThumbnail(bool value)
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

