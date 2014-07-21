using Sitecore.MobileSDK.Validators;

namespace Sitecore.MobileSDK.Session
{
  using System;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  internal class SessionBuilderImpl : IAuthenticatedSessionBuilder, IAnonymousSessionBuilder
  {
    public ISitecoreWebApiSession BuildSession()
    {
      string localWebApiVersion = this.webApiVersion;
      if (null == localWebApiVersion)
      {
        localWebApiVersion = "v1";
      }

      SessionConfig conf = SessionConfig.NewAuthenticatedSessionConfig(
        this.instanceUrl, 
        //@adk : TODO : do not store credentials in variables
        this.credentials.UserName,
        this.credentials.Password,

        this.site, 
        localWebApiVersion);


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
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.site,
        "[IBaseSessionBuilder.Site] the property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        site, 
        "[SessionBuilder.Site] the value cannot be null or empty"
      );

      this.site = site;
      return this;
    }

    public IBaseSessionBuilder WebApiVersion(string webApiVersion)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.webApiVersion,
        "[IBaseSessionBuilder.WebApiVersion] the property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        webApiVersion, 
        "[SessionBuilder.WebApiVersion] the value cannot be null or empty"
      );


      this.webApiVersion = webApiVersion;
      return this;
    }

    public IBaseSessionBuilder DefaultDatabase(string defaultDatabase)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.itemSourceAccumulator.Database,
        "[IBaseSessionBuilder.DefaultDatabase] the property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        defaultDatabase, 
        "[SessionBuilder.DefaultDatabase] the value cannot be null or empty"
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
        "[IBaseSessionBuilder.DefaultLanguage] the property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        defaultLanguage, 
        "[SessionBuilder.DefaultLanguage] the value cannot be null or empty"
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
        "[IBaseSessionBuilder.MediaLibraryRoot] the property cannot be assigned twice"
      );
      MediaPathValidator.ValidateMediaRoot(mediaLibraryRootItem); 

      this.mediaRoot = mediaLibraryRootItem;
      return this;
    }

    public IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension)
    {
      WebApiParameterValidator.ValidateWriteOnceDestinationWithErrorMessage(
        this.mediaExtension,
        "[IBaseSessionBuilder.DefaultMediaResourceExtension] the property cannot be assigned twice"
      );
      WebApiParameterValidator.ValidateParameterAndThrowErrorWithMessage(
        defaultExtension, 
        "[SessionBuilder.DefaultMediaResourceExtension] the value cannot be null or empty"
      );

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

