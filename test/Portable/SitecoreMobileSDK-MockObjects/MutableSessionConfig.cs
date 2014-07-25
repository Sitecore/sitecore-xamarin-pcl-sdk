

namespace SitecoreMobileSDKMockObjects
{
  using System;
  using Sitecore.MobileSDK.SessionSettings;

  public class MutableSessionConfig : SessionConfig
  {
    public MutableSessionConfig(
      string instanceUrl, 
      string login, 
      string password, 
      string site = null, 
      string itemWebApiVersion = "v1",
      string mediaLibraryRoot = "/sitecore/media library",
      string defaultMediaResourceExtension = "ashx",
      string mediaPrefix = "~/media")
    : base(instanceUrl, login, password, site, itemWebApiVersion, mediaLibraryRoot, defaultMediaResourceExtension, mediaPrefix)
    {
    }

    public override SessionConfig ShallowCopy()
    {
      var result = new MutableSessionConfig(
        "mock instance", 
        "mock login", 
        "mock password", 
        "mock site", 
        "v1", 
        this.MediaLibraryRoot, 
        this.DefaultMediaResourceExtension, 
        this.MediaPrefix);

      // @adk : skipping validation
      result.SetInstanceUrl(this.InstanceUrl);
      result.SetLogin(this.UserName);
      result.SetPassword(this.Password);
      result.SetSite(this.Site);
      result.SetItemWebApiVersion(this.ItemWebApiVersion);


      return result;
    }

    public void SetInstanceUrl(string value)
    {
      this.InstanceUrl = value;
    }

    public void SetSite(string value)
    {
      this.Site = value;
    }

    public void SetLogin(string value)
    {
      this.UserName = value;
    }

    public void SetPassword(string value)
    {
      this.Password = value;
    }

    public void SetItemWebApiVersion(string value)
    {
      this.ItemWebApiVersion = value;
    }
  }
}

