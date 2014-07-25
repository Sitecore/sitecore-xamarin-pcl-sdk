
namespace Sitecore.MobileSDK.SessionSettings
{
  using System;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder;

  public class SessionConfigPOD : ISessionConfig
  {
    public virtual ISessionConfig SessionConfigShallowCopy()
    {
      SessionConfigPOD result = new SessionConfigPOD();
      result.InstanceUrl = this.InstanceUrl;
      result.ItemWebApiVersion = this.ItemWebApiVersion;
      result.Site = this.Site;
      result.MediaLibraryRoot = this.MediaLibraryRoot;
      result.DefaultMediaResourceExtension = this.DefaultMediaResourceExtension;
      result.MediaPrefix = this.MediaPrefix;

      return result;
    }

    #region ISessionConfig
    public string InstanceUrl { get; set; }

    public string ItemWebApiVersion { get; set; }

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
          bool firstSymbolIsNotSlash = !siteValue.StartsWith (separator);
          if (firstSymbolIsNotSlash)
          {
            throw new ArgumentException ("SessionConfigPOD.Site : site must starts with '/'"); 
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

    #endregion ISessionConfig


    #region Comparator
    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals (this, obj))
      {
        return true;
      }

      SessionConfigPOD other = (SessionConfigPOD)obj;
      if (null == other)
      {
        return false;
      }


      bool isUrlEqual = object.Equals (this.InstanceUrl, other.InstanceUrl);
      bool isVersionEqual = object.Equals (this.ItemWebApiVersion, other.ItemWebApiVersion);
      bool isSiteEqual = object.Equals (this.Site, other.Site);

      return isUrlEqual && isVersionEqual && isSiteEqual;
    }

    public override int GetHashCode ()
    {
      return base.GetHashCode() + this.InstanceUrl.GetHashCode () + this.ItemWebApiVersion.GetHashCode () + this.Site.GetHashCode ();
    }
    #endregion Comparator

    private string site;
  }
}
