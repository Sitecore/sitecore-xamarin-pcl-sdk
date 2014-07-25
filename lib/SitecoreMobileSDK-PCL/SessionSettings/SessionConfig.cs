

namespace Sitecore.MobileSDK.SessionSettings
{
  using System;
  using System.Diagnostics;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.Validators;


  public class SessionConfig : ISessionConfig, IWebApiCredentials
  {
    #region Constructor
    public static SessionConfig NewAnonymousSessionConfig(
      string instanceUrl, 
      string site = null, 
      string itemWebApiVersion = "v1")
    {
      return new SessionConfig(
        instanceUrl, 
        null, 
        null, 
        site, 
        itemWebApiVersion, 
        "/sitecore/media library",
        "ashx",
        "~/media");
    }

    public static SessionConfig NewAuthenticatedSessionConfig(
      string instanceUrl, 
      string login, 
      string password, 
      string site = null, 
      string itemWebApiVersion = "v1")
    {
      return new SessionConfig(
        instanceUrl, 
        login, password, 
        site, 
        itemWebApiVersion,
        "/sitecore/media library",
        "ashx",
        "~/media");
    }

    public static SessionConfig NewSessionConfig(
      string instanceUrl, 
      string login, 
      string password, 
      string site, 
      string itemWebApiVersion,
      string mediaLibraryRoot,
      string defaultMediaResourceExtension,
      string mediaPrefix)
    {
      return new SessionConfig(
        instanceUrl, 
        login, 
        password, 
        site, 
        itemWebApiVersion,
        mediaLibraryRoot,
        defaultMediaResourceExtension,
        mediaPrefix);
    }

    protected SessionConfig(
      string instanceUrl, 
      string login, 
      string password, 
      string site, 
      string itemWebApiVersion,
      string mediaLibraryRoot,
      string defaultMediaResourceExtension,
      string mediaPrefix)
    {
      this.InstanceUrl = instanceUrl;
      this.UserName    = login;
      this.Password    = password;
      this.Site        = site;
      this.ItemWebApiVersion = itemWebApiVersion;

      this.MediaLibraryRoot = mediaLibraryRoot;
      this.DefaultMediaResourceExtension = defaultMediaResourceExtension;
      this.MediaPrefix = mediaPrefix;

      this.Validate();
    }
    #endregion Constructor

    #region ICloneable
    public virtual SessionConfig ShallowCopy()
    {
      SessionConfig result = new SessionConfig(
        this.InstanceUrl, 
        this.UserName, 
        this.Password, 
        this.Site, 
        this.ItemWebApiVersion,
        this.MediaLibraryRoot,
        this.DefaultMediaResourceExtension,
        this.MediaPrefix);

      return result;
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
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage
      (
        this.InstanceUrl,
        "SessionConfig.InstanceUrl is required"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage
      (
        this.ItemWebApiVersion,
        "SessionConfig.ItemWebApiVersion is required"
      );

      bool hasLogin = !string.IsNullOrWhiteSpace(this.UserName);
      bool hasPassword = !string.IsNullOrWhiteSpace(this.Password);
      if (!hasLogin && hasPassword)
      {
        throw new ArgumentException("SessionConfig.Credentials : login is required for authenticated session");
      }

      if (hasLogin && !hasPassword)
      {
        throw new ArgumentException("SessionConfig.Credentials : password is required for authenticated session");
      }

      if (!SessionConfigValidator.IsValidSchemeOfInstanceUrl(this.InstanceUrl))
      {
        Debug.WriteLine("[WARNING] : SessionConfig - instance URL does not have a scheme");
      }
    }

    #endregion Properties

    #region Media Properties

    public string MediaLibraryRoot
    {
      get;
      private set;
    }

    public string DefaultMediaResourceExtension
    {
      get;
      private set;
    }

    public string MediaPrefix
    {
      get;
      private set;
    }

    #endregion Media Properties

    #region Instance Variables
    private const string DefaultMediaLibraryRoot = "/sitecore/media library";
    private string mediaLibraryRoot;
    #endregion Instance Variables
  }
}

