namespace Sitecore.MobileSDK.SessionSettings
{
  using System;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.MediaItem;

  public class SessionConfigPOD : ISessionConfig, IMediaLibrarySettings
  {
    private SessionConfigPOD SessionConfigPODCopy()
    {
      SessionConfigPOD result = new SessionConfigPOD();
      result.InstanceUrl = this.InstanceUrl;
      result.ItemSSCVersion = this.ItemSSCVersion;
      result.Site = this.Site;
      result.MediaLibraryRoot = this.MediaLibraryRoot;
      result.DefaultMediaResourceExtension = this.DefaultMediaResourceExtension;
      result.MediaPrefix = this.MediaPrefix;
      result.MediaDownloadStrategy = this.MediaDownloadStrategy;

      return result;
    }

    public virtual ISessionConfig SessionConfigShallowCopy()
    {
      return this.SessionConfigPODCopy();
    }

    public virtual IMediaLibrarySettings MediaSettingsShallowCopy()
    {
      return this.SessionConfigPODCopy();
    }

    #region ISessionConfig
    public string InstanceUrl { get; set; }

    public string ItemSSCVersion { get; set; }

    public string Site
    {
      get
      {
        return this.site;
      }
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          this.site = null;
        }
        else
        {
          string separator = "/";
          string siteValue = value;
          bool firstSymbolIsNotSlash = !siteValue.StartsWith(separator);
          if (firstSymbolIsNotSlash)
          {
            throw new ArgumentException("SessionConfigPOD.Site : site must starts with '/'");
          }
          else
          {
            this.site = value;
          }
        }
      }
    }

    public string MediaLibraryRoot { get; set; }

    public string DefaultMediaResourceExtension
    {
      get;
      set;
    }

    public string MediaPrefix
    {
      get;
      set;
    }

    public DownloadStrategy MediaDownloadStrategy
    {
      get;
      set;
    }

    #endregion ISessionConfig


    #region Comparator
    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals(this, obj))
      {
        return true;
      }

      SessionConfigPOD other = (SessionConfigPOD)obj;
      if (null == other)
      {
        return false;
      }


      bool isUrlEqual = object.Equals(this.InstanceUrl, other.InstanceUrl);
      bool isVersionEqual = object.Equals(this.ItemSSCVersion, other.ItemSSCVersion);
      bool isSiteEqual = object.Equals(this.Site, other.Site);

      return isUrlEqual && isVersionEqual && isSiteEqual;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode() + this.InstanceUrl.GetHashCode() + this.ItemSSCVersion.GetHashCode() + this.Site.GetHashCode();
    }
    #endregion Comparator

    private string site;
  }
}
