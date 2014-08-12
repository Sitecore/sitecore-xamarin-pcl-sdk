namespace Sitecore.MobileSDK.UserRequest
{
  using System;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.SessionSettings;


  public class SessionConfigMerger
  {
    public SessionConfigMerger(ISessionConfig defaultSessionConfig)
    {
      this.defaultSessionConfig = defaultSessionConfig;

      this.Validate();
    }

    public ISessionConfig FillSessionConfigGaps(ISessionConfig userConfig)
    {
      if (null == userConfig)
      {
        return this.defaultSessionConfig;
      }

      var result = new SessionConfigPOD();
      result.InstanceUrl = userConfig.InstanceUrl ?? this.defaultSessionConfig.InstanceUrl;
      result.Site = userConfig.Site ?? this.defaultSessionConfig.Site;
      result.ItemWebApiVersion = userConfig.ItemWebApiVersion ?? this.defaultSessionConfig.ItemWebApiVersion;

      return result;
    }

    private void Validate()
    {
      if (null == this.defaultSessionConfig)
      {
        throw new ArgumentNullException("SessionConfigMerger.defaultSessionConfig cannot be null");
      }
      else if (null == this.defaultSessionConfig.InstanceUrl)
      {
        throw new ArgumentNullException("SessionConfigMerger.defaultSessionConfig.InstanceUrl cannot be null");
      }
      else if (null == this.defaultSessionConfig.ItemWebApiVersion)
      {
        throw new ArgumentNullException("SessionConfigMerger.defaultSessionConfig.ItemWebApiVersion cannot be null");
      }
    }

    private readonly ISessionConfig defaultSessionConfig;
  }
}

