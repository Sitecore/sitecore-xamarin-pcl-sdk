using System.Diagnostics;

namespace Sitecore.MobileSDK.SessionSettings
{
  using System;

  public class SessionConfig : ISessionConfig, IWebApiCredentials
  {
    public SessionConfig(string instanceUrl, string login, string password, string site = null, string itemWebApiVersion = "v1")
    {
      this.InstanceUrl = instanceUrl;
      this.Login       = login      ;
      this.Password    = password   ;
      this.Site        = site       ;
      this.ItemWebApiVersion = itemWebApiVersion;

      this.Validate ();
    }

    public string InstanceUrl
    {
      get;
      private set;
    }

    public string Site      
    { 
      get; 
      private set; 
    }

    public string Login
    {
      get;
      private set;
    }

    public string Password
    {
      get;
      private set;
    }

    public string ItemWebApiVersion
    {
      get; 
      private set;
    }

    public bool IsAnonymous()
    {
      return string.IsNullOrEmpty(this.Login) && string.IsNullOrEmpty(this.Password);
    }

    private void Validate()
    {
      if (string.IsNullOrEmpty(this.InstanceUrl))
      {
        throw new ArgumentNullException("SessionConfig.InstanceUrl is required");
      }
      else if (!SessionConfigValidator.IsValidSchemeOfInstanceUrl(this.InstanceUrl))
      {
        Debug.WriteLine("[WARNING] : SessionConfig - instance URL does not have a scheme");
      }
      else if (string.IsNullOrEmpty (this.ItemWebApiVersion))
      {
        throw new ArgumentNullException ("SessionConfig.ItemWebApiVersion is required");
      }
    }
  }
}

