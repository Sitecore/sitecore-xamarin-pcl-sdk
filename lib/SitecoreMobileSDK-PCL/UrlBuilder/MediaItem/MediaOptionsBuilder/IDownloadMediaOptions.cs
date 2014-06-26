using System;


namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
  using System.Collections.Generic;

  public interface IDownloadMediaOptions
  {
    bool IsEmpty{ get; }
    string Width{ get; }
    string Height{ get; }
    string MaxWidth{ get; }
    string MaxHeight{ get; }
    string BackgroundColor{ get; }
    string DisableMediaCache{ get; }
    string AllowStrech{ get; }
    string Scale{ get; }
    string DisplayAsThumbnail{ get; }
    Dictionary<string, string> OptionsDictionary{ get; }

    IDownloadMediaOptions ShallowCopy();
  }
}

