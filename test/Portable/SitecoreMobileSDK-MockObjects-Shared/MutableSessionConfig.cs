namespace Sitecore.MobileSDK.MockObjects
{
  using Sitecore.MobileSDK.SessionSettings;

  public class MutableSessionConfig : SessionConfig
  {
    public MutableSessionConfig(
      string instanceUrl, 
      string site = null, 
      string itemWebApiVersion = "v1")
    : base(instanceUrl, site)
    {
    }

    public override SessionConfig ShallowCopy()
    {
      var result = new MutableSessionConfig(
        "mock instance", 
        "mock site", 
        "v1");

      // @adk : skipping validation
      result.SetInstanceUrl(this.InstanceUrl);
      result.SetSite(this.Site);

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

    public void SetItemWebApiVersion(string value)
    {
    }
  }
}

