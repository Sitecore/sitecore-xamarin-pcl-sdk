

namespace Sitecore.MobileSDK.SessionSettings
{
  using System;
  using System.Diagnostics;

  public class SessionConfig : ISessionConfig, IWebApiCredentials
  {
    #region Constructor
    public static SessionConfig NewAnonymousSessionConfig(string instanceUrl, string site = null, string itemWebApiVersion = "v1")
    {
      return new SessionConfig(instanceUrl, null, null, site, itemWebApiVersion);
    }

    public static SessionConfig NewAuthenticatedSessionConfig(string instanceUrl, string login, string password, string site = null, string itemWebApiVersion = "v1")
    {
      return new SessionConfig(instanceUrl, login, password, site, itemWebApiVersion);
    }

    protected SessionConfig(string instanceUrl, string login, string password, string site = null, string itemWebApiVersion = "v1")
    {
      this.InstanceUrl = instanceUrl;
      this.UserName       = login      ;
      this.Password    = password   ;
      this.Site        = site       ;
      this.ItemWebApiVersion = itemWebApiVersion;

      this.Validate();
    }
    #endregion Constructor

    #region ICloneable
    public virtual SessionConfig ShallowCopy()
    {
      return new SessionConfig(this.InstanceUrl, this.UserName, this.Password, this.Site, this.ItemWebApiVersion);
    }

    public virtual ISessionConfig SessionConfigShallowCopy()
    {
      return this.ShallowCopy();
    }

    public virtual IWebApiCredentials CredentialsShallowCopy()
    {
      return this.ShallowCopy();
    }
    #endregion ICloneable

    #region Properties
    public string InstanceUrl
    {
      get;
      protected set;
    }

    public string Site      
    { 
      get; 
      protected set; 
    }

    public string UserName
    {
      get;
      protected set;
    }

    public string Password
    {
      get;
      protected set;
    }

    public string ItemWebApiVersion
    {
      get; 
      protected set;
    }

    public bool IsAnonymous()
    {
      return string.IsNullOrEmpty(this.UserName) && string.IsNullOrEmpty(this.Password);
    }

    private void Validate()
    {
      if (string.IsNullOrWhiteSpace(this.InstanceUrl))
      {
        throw new ArgumentNullException("SessionConfig.InstanceUrl is required");
      }
      else if (string.IsNullOrWhiteSpace(this.ItemWebApiVersion))
      {
        throw new ArgumentNullException("SessionConfig.ItemWebApiVersion is required");
      }

      bool hasLogin = !string.IsNullOrWhiteSpace(this.UserName);
      bool hasPassword = !string.IsNullOrWhiteSpace(this.Password);
      if (hasLogin && !hasPassword)
      {
        throw new ArgumentNullException("SessionConfig.Credentials : password is required for authenticated session");
      }

      if (!SessionConfigValidator.IsValidSchemeOfInstanceUrl(this.InstanceUrl))
      {
        Debug.WriteLine("[WARNING] : SessionConfig - instance URL does not have a scheme");
      }
    }

    public string MediaLybraryRoot 
    {
      get
      { 
        if (null == this.mediaLybraryRoot)
        {
          return SessionConfig.DefaultMediaLybraryRoot;
        }
        return this.mediaLybraryRoot;
      }
      set
      { 
        this.mediaLybraryRoot = value;
      }
    }
    #endregion Properties

    #region Instance Variables
    private const string DefaultMediaLybraryRoot = "/sitecore/media library";
    private string mediaLybraryRoot;
    #endregion Instance Variables
  }
}

