using Sitecore.MobileSDK.Validators;

namespace Sitecore.MobileSDK.Session
{
  using System;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  internal class SessionBuilder : IAuthenticatedSessionBuilder, IAnonymousSessionBuilder
  {
    #region Main Logic
    public ISitecoreWebApiSession BuildSession()
    {
      string optionalWebApiVersion = this.OptionalWebApiVersion();
      string optionalMediaRoot = this.OptionalMediaRoot();
      string optionalMediaExtension = this.OptionalMediaExtension();
      string optionalMediaPrefix = OptionalMediaPrefix();


      ////////
      SessionConfig conf = SessionConfig.NewSessionConfig(
        this.instanceUrl,
        //@adk : TODO : do not store credentials in variables
        this.credentials.Username,
        this.credentials.Password,

        this.site,
        optionalWebApiVersion,
        optionalMediaRoot,
        optionalMediaExtension,
        optionalMediaPrefix);


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

    private string OptionalWebApiVersion()
    {
      string optionalWebApiVersion = this.webApiVersion;
      if (null == optionalWebApiVersion)
      {
        optionalWebApiVersion = "v1";
      }

      return optionalWebApiVersion;
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
      if (string.IsNullOrEmpty(instanceUrl))
      {
        throw new ArgumentException(typeof(SessionBuilder).Name + ".InstanceUrl : The input cannot be null or empty.");
      }

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
      if (string.IsNullOrEmpty(credentials.Username))
      {
        throw new ArgumentException(this.GetType().Name + ".Credentials.Username : The input cannot be null or empty.");
      }

      if (string.IsNullOrEmpty(credentials.Password))
      {
        throw new ArgumentException(this.GetType().Name + ".Credentials.Password : The input cannot be null or empty.");
      }

      this.credentials = credentials.CredentialsShallowCopy();
      return this;
    }
    #endregion

    #region IAnonymousSessionBuilder
    public IBaseSessionBuilder Site(string site)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.site,
        this.GetType().Name + ".Site property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        site,
        this.GetType().Name + ".Site : The input cannot be null or empty"
      );

      this.site = site;
      return this;
    }

    public IBaseSessionBuilder WebApiVersion(string webApiVersion)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.webApiVersion,
        this.GetType().Name + ".WebApiVersion property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        webApiVersion,
        this.GetType().Name + ".WebApiVersion : The input cannot be null or empty"
      );


      this.webApiVersion = webApiVersion;
      return this;
    }

    public IBaseSessionBuilder DefaultDatabase(string defaultDatabase)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.itemSourceAccumulator.Database,
        this.GetType().Name + ".DefaultDatabase property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        defaultDatabase,
        this.GetType().Name + ".DefaultDatabase : The input cannot be null or empty"
      );


      this.itemSourceAccumulator =
        new ItemSourcePOD(
          defaultDatabase,
          this.itemSourceAccumulator.Language,
          itemSourceAccumulator.Version);

      return this;
    }

    public IBaseSessionBuilder DefaultLanguage(string defaultLanguage)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.itemSourceAccumulator.Language,
        this.GetType().Name + ".DefaultLanguage property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        defaultLanguage,
        this.GetType().Name + ".DefaultLanguage : The input cannot be null or empty"
      );


      this.itemSourceAccumulator =
        new ItemSourcePOD(
          this.itemSourceAccumulator.Database,
          defaultLanguage,
          itemSourceAccumulator.Version);

      return this;
    }

    public IBaseSessionBuilder MediaLibraryRoot(string mediaLibraryRootItem)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.mediaRoot,
        this.GetType().Name + ".MediaLibraryRoot property cannot be assigned twice"
      );
      MediaPathValidator.ValidateMediaRoot(mediaLibraryRootItem, this.GetType().Name + ".MediaLibraryRoot");

      this.mediaRoot = mediaLibraryRootItem;
      return this;
    }

    public IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.mediaExtension,
         this.GetType().Name + ".DefaultMediaResourceExtension property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        defaultExtension,
        this.GetType().Name + ".DefaultMediaResourceExtension : The input cannot be null or empty"
      );

      this.mediaExtension = defaultExtension;
      return this;
    }

    public IBaseSessionBuilder MediaPrefix(string mediaPrefix)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.mediaPrefix,
        this.GetType().Name + ".MediaPrefix property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        mediaPrefix,
        this.GetType().Name + ".MediaPrefix : The input cannot be null or empty"
      );

      this.mediaPrefix = mediaPrefix;
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

    private IWebApiCredentials credentials = new WebApiCredentialsPOD(null, null);
    private ItemSourcePOD itemSourceAccumulator = new ItemSourcePOD(null, null, null);
    #endregion State
  }
}

