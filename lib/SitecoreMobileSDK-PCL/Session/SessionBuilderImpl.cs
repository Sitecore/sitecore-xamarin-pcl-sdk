namespace Sitecore.MobileSDK.Session
{
  using System;

  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  internal class SessionBuilderImpl : IAuthenticatedSessionBuilder, IAnonymousSessionBuilder
  {
    public ISitecoreWebApiSession BuildSession()
    {
      SessionConfig conf = SessionConfig.NewAuthenticatedSessionConfig(
        this.instanceUrl, 

        //@adk : TODO : do not store credentials in variables
        this.credentials.Login,
        this.credentials.Password,

        this.site, 
        this.webApiVersion);
      conf.MediaLybraryRoot = this.mediaRoot;

      var itemSource = new ItemSource(
        this.itemSourceAccumulator.Database,
        this.itemSourceAccumulator.Language,
        this.itemSourceAccumulator.Version);

      var result = new ScApiSession(conf, itemSource);
      return result;
    }

    public ISitecoreWebApiReadonlySession BuildReadonlySession()
    {
      return this.BuildSession();
    }

    #region Constructor
    private SessionBuilderImpl()
    {
    }

    public static SessionBuilderImpl SessionBuilderWithHost(string instanceUrl)
    {
      SessionBuilderImpl result = new SessionBuilderImpl();
      result.instanceUrl = instanceUrl;

      return result;
    }
    #endregion Constructor

    #region IAuthenticatedSessionBuilder
    public IBaseSessionBuilder Credentials(IWebApiCredentials credentials)
    {
      // @adk : won't be invoked more than once.
      // No calidation needed.

      this.credentials = credentials.CredentialsShallowCopy();
      return this;
    }
    #endregion


    #region IAnonymousSessionBuilder
    public IBaseSessionBuilder Site(string site)
    {
      if (null != this.site)
      {
        throw new InvalidOperationException("[IBaseSessionBuilder.Site] the property cannot be assigned twice");
      }

      this.site = site;
      return this;
    }

    public IBaseSessionBuilder WebApiVersion(string webApiVersion)
    {
      if (null != this.webApiVersion)
      {
        throw new InvalidOperationException("[IBaseSessionBuilder.WebApiVersion] the property cannot be assigned twice");
      }

      this.webApiVersion = webApiVersion;
      return this;
    }

    public IBaseSessionBuilder DefaultDatabase(string defaultDatabase)
    {
      if (null != this.itemSourceAccumulator.Database)
      {
        throw new InvalidOperationException("[IBaseSessionBuilder.DefaultDatabase] the property cannot be assigned twice");
      }

      this.itemSourceAccumulator = 
        new ItemSourcePOD(
          defaultDatabase, 
          this.itemSourceAccumulator.Language, 
          itemSourceAccumulator.Version);

      return this;
    }

    public IBaseSessionBuilder DefauldLanguage(string defaultLanguage)
    {
      if (null != this.itemSourceAccumulator.Language)
      {
        throw new InvalidOperationException("[IBaseSessionBuilder.DefauldLanguage] the property cannot be assigned twice");
      }

      this.itemSourceAccumulator = 
        new ItemSourcePOD(
          this.itemSourceAccumulator.Database, 
          defaultLanguage, 
          itemSourceAccumulator.Version);

      return this;
    }

    public IBaseSessionBuilder MediaLibraryRoot(string mediaLibraryRootItem)
    {
      if (null != this.mediaRoot)
      {
        throw new InvalidOperationException("[IBaseSessionBuilder.MediaLibraryRoot] the property cannot be assigned twice");
      }

      this.mediaRoot = mediaLibraryRootItem;
      return this;
    }

    public IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension)
    {
      if (null != this.mediaExtension)
      {
        throw new InvalidOperationException("[IBaseSessionBuilder.DefaultMediaResourceExtension] the property cannot be assigned twice");
      }

      this.mediaExtension = defaultExtension;
      return this;
    }
    #endregion IAnonymousSessionBuilder

    #region State
    private string             instanceUrl   ;
    private string             webApiVersion ;
    private string             site          ;
    private string             mediaRoot     ;
    private string             mediaExtension;

    private IWebApiCredentials credentials           = new WebApiCredentialsPOD(null, null);
    private ItemSourcePOD      itemSourceAccumulator = new ItemSourcePOD       (null, null, null);
    #endregion State
  }
}

