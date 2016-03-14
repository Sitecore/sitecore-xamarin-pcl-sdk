namespace Sitecore.MobileSDK.Session
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.MediaItem;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.PasswordProvider;
  using Sitecore.MobileSDK.PasswordProvider.Interface;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.Validators;

  internal class SessionBuilder : IAuthenticatedSessionBuilder, IAnonymousSessionBuilder
  {
    #region Main Logic
    public ISitecoreWebApiSession BuildSession()
    {
      string optionalMediaRoot = this.OptionalMediaRoot();
      string optionalMediaExtension = this.OptionalMediaExtension();
      string optionalMediaPrefix = this.OptionalMediaPrefix();


      ////////
      SessionConfig conf = new SessionConfig(
        this.instanceUrl,
        this.site);

      var mediaSettings = new MediaLibrarySettings(
        optionalMediaRoot,
        optionalMediaExtension,
        optionalMediaPrefix,
        this.resizingFlag);

      var itemSource = new ItemSource(
        this.itemSourceAccumulator.Database,
        this.itemSourceAccumulator.Language,
        this.itemSourceAccumulator.VersionNumber);

      var result = new ScApiSession(conf, this.credentials, mediaSettings, itemSource);
      return result;
    }

    public ISitecoreWebApiReadonlySession BuildReadonlySession()
    {
      return this.BuildSession();
    }

    private string OptionalMediaRoot()
    {
      string optionalMediaRoot = this.mediaRoot;
      if (null == optionalMediaRoot)
      {
        optionalMediaRoot = "/sitecore/media library";
      }

      return optionalMediaRoot;
    }

    private string OptionalMediaExtension()
    {
      string optionalMediaExtension = this.mediaExtension;
      if (null == optionalMediaExtension)
      {
        optionalMediaExtension = "ashx";
      }

      return optionalMediaExtension;
    }

    private string OptionalMediaPrefix()
    {
      string optionalMediaPrefix = this.mediaPrefix;
      if (null == optionalMediaPrefix)
      {
        optionalMediaPrefix = "~/media";
      }

      return optionalMediaPrefix;
    }
    #endregion Main Logic

    #region Constructor
    private SessionBuilder()
    {
    }

    public static SessionBuilder SessionBuilderWithHost(string instanceUrl)
    {
      BaseValidator.CheckForNullAndEmptyOrThrow(instanceUrl, typeof(SessionBuilder).Name + ".InstanceUrl");

      var result = new SessionBuilder
      {
        instanceUrl = instanceUrl
      };

      return result;
    }
    #endregion Constructor

    #region IAuthenticatedSessionBuilder
    public IBaseSessionBuilder Credentials(IWebApiCredentials credentials)
    {
      // @adk : won't be invoked more than once.
      // No validation needed.
      BaseValidator.CheckForNullAndEmptyOrThrow(credentials.Username, this.GetType().Name + ".Credentials.Username");
      BaseValidator.CheckForNullAndEmptyOrThrow(credentials.Password, this.GetType().Name + ".Credentials.Password");

      this.credentials = credentials.CredentialsShallowCopy();
      return this;
    }
    #endregion

    #region IAnonymousSessionBuilder
    public IBaseSessionBuilder Site(string site)
    {
      if (string.IsNullOrEmpty(site))
      {
        return this;
      }

      BaseValidator.CheckForTwiceSetAndThrow(this.site, this.GetType().Name + ".Site");
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(site, this.GetType().Name + ".Site");

      this.site = site;
      return this;
    }

    public IBaseSessionBuilder WebApiVersion(string webApiVersion)
    {
      BaseValidator.CheckForTwiceSetAndThrow(this.webApiVersion, this.GetType().Name + ".WebApiVersion");
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(webApiVersion, this.GetType().Name + ".WebApiVersion");

      this.webApiVersion = webApiVersion;
      return this;
    }

    public IBaseSessionBuilder DefaultDatabase(string defaultDatabase)
    {
      if (string.IsNullOrEmpty(defaultDatabase))
      {
        return this;
      }

      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Database,
        this.GetType().Name + ".DefaultDatabase");
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(defaultDatabase,
        this.GetType().Name + ".DefaultDatabase");

      this.itemSourceAccumulator =
        new ItemSourcePOD(
          defaultDatabase,
          this.itemSourceAccumulator.Language,
          itemSourceAccumulator.VersionNumber);

      return this;
    }

    public IBaseSessionBuilder DefaultLanguage(string defaultLanguage)
    {
      if (string.IsNullOrEmpty(defaultLanguage))
      {
        return this;
      }

      BaseValidator.CheckForTwiceSetAndThrow(this.itemSourceAccumulator.Language,
        this.GetType().Name + ".DefaultLanguage");
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(defaultLanguage,
        this.GetType().Name + ".DefaultLanguage");

      this.itemSourceAccumulator =
        new ItemSourcePOD(
          this.itemSourceAccumulator.Database,
          defaultLanguage,
          itemSourceAccumulator.VersionNumber);

      return this;
    }

    public IBaseSessionBuilder MediaLibraryRoot(string mediaLibraryRootItem)
    {
      if (string.IsNullOrEmpty(mediaLibraryRootItem))
      {
        return this;
      }

      BaseValidator.CheckForTwiceSetAndThrow(this.mediaRoot,
        this.GetType().Name + ".MediaLibraryRoot");
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(mediaLibraryRootItem,
        this.GetType().Name + ".MediaLibraryRoot");

      this.mediaRoot = mediaLibraryRootItem;
      return this;
    }

    public IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension)
    {
      if (string.IsNullOrEmpty(defaultExtension))
      {
        return this;
      }

      BaseValidator.CheckForTwiceSetAndThrow(this.mediaExtension,
        this.GetType().Name + ".DefaultMediaResourceExtension");
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(defaultExtension,
        this.GetType().Name + ".DefaultMediaResourceExtension");

      this.mediaExtension = defaultExtension;
      return this;
    }

    public IBaseSessionBuilder MediaPrefix(string mediaPrefix)
    {
      if (string.IsNullOrEmpty(mediaPrefix))
      {
        return this;
      }

      BaseValidator.CheckForTwiceSetAndThrow(this.mediaPrefix,
        this.GetType().Name + ".MediaPrefix");
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(mediaPrefix,
        this.GetType().Name + ".MediaPrefix");

      this.mediaPrefix = mediaPrefix;
      return this;
    }

    public IBaseSessionBuilder MediaResizingStrategy(DownloadStrategy resizingFlag)
    {
      this.resizingFlag = resizingFlag;
      return this;
    }
    #endregion IAnonymousSessionBuilder

    #region State
    private string instanceUrl;
    private string webApiVersion;
    private string site;
    private string mediaRoot;
    private string mediaExtension;
    private string mediaPrefix;
    DownloadStrategy resizingFlag = DownloadStrategy.Plain;

    private IWebApiCredentials credentials = null;
    private ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD(null, null, null);
    #endregion State
  }
}

