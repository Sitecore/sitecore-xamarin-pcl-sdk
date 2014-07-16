

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

    public override SessionConfig ShallowCopy()
    {
      var result = new MutableSessionConfig("mock instance", "mock login", "mock password", "mock site", "v1");
      result.SetInstanceUrl(this.InstanceUrl);
      result.SetLogin(this.Login);
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

