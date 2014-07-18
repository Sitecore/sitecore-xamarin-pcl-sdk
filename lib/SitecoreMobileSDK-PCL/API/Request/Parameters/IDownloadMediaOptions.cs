namespace Sitecore.MobileSDK.API.Request.Parameters
{
    using System.Collections.Generic;

    public interface IDownloadMediaOptions
  {
    IDownloadMediaOptions DeepCopyMediaDownloadOptions();

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
  }
}

