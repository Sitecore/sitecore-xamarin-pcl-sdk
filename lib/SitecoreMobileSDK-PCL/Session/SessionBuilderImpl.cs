namespace Sitecore.MobileSDK.Session
{
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  internal class SessionBuilderImpl : IAuthenticatedSessionBuilder, IAnonymousSessionBuilder
  {
    private SessionBuilderImpl()
    {
    }

    public static SessionBuilderImpl SessionBuilderWithHost(string instanceUrl)
    {
      SessionBuilderImpl result = new SessionBuilderImpl();
      result.instanceUrl = instanceUrl;

      return result;
    }

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
    public ISitecoreWebApiSession Build()
    {
      return null;
    }

    public IBaseSessionBuilder WebApiVersion(string webApiVersion)
    {
      this.webApiVersion = webApiVersion;
      return this;
    }

    public IBaseSessionBuilder DefaultDatabase(string defaultDatabase)
    {
      this.itemSourceAccumulator = 
        new ItemSourcePOD(
          defaultDatabase, 
          this.itemSourceAccumulator.Language, 
          itemSourceAccumulator.Version);

      return this;
    }

    public IBaseSessionBuilder DefauldLanguage(string defaultLanguage)
    {
      this.itemSourceAccumulator = 
        new ItemSourcePOD(
          this.itemSourceAccumulator.Database, 
          defaultLanguage, 
          itemSourceAccumulator.Version);

      return this;
    }

    public IBaseSessionBuilder MediaLibraryRoot(string mediaLibraryRootItem)
    {
      this.mediaRoot = mediaLibraryRootItem;
      return this;
    }

    public IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension)
    {
      this.mediaExtension = defaultExtension;
      return this;
    }
    #endregion IAnonymousSessionBuilder

    private string             instanceUrl   ;
    private string             webApiVersion ;
    private string             mediaRoot     ;
    private string             mediaExtension;
    private IWebApiCredentials credentials   ;
    private ItemSourcePOD      itemSourceAccumulator = new ItemSourcePOD(null, null, null);
  }
}

