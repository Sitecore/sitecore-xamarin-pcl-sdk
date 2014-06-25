

namespace SitecoreMobileSDKMockObjects
{
  using System;
  using Sitecore.MobileSDK.SessionSettings;

  public class MutableSessionConfig : SessionConfig
  {
    public MutableSessionConfig(string instanceUrl, string login, string password, string site = null, string itemWebApiVersion = "v1")
      : base(instanceUrl, login, password, site, itemWebApiVersion)
    {
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
      this.Login = value;
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

