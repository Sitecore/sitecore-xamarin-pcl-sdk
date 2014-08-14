namespace Sitecore.MobileSDK.API.Session
{
  /// <summary>
  /// Interface represents base builder for sessions. 
  /// </summary>
  public interface IBaseSessionBuilder
  {
    /// <summary>
    /// Builds session. 
    /// </summary>
    /// <returns><see cref="ISitecoreWebApiSession"/></returns>
    ISitecoreWebApiSession BuildSession();
    /// <summary>
    /// Builds readonly session. 
    /// </summary>
    /// <returns><see cref="ISitecoreWebApiReadonlySession"/></returns>
    ISitecoreWebApiReadonlySession BuildReadonlySession();

    /// <summary>
    /// Specifies site parameter.
    /// </summary>
    /// <param name="site">Site parameter, for example '/sitecore/shell'</param>
    /// <returns>this</returns>
    IBaseSessionBuilder Site(string site);

    /// <summary>
    /// Specifies WebAPI vertion.
    /// </summary>
    /// <param name="webApiVersion">WebAPI version, 'v1' by default</param>
    /// <returns>this</returns>
    IBaseSessionBuilder WebApiVersion(string webApiVersion);

    /// <summary>
    /// Specifies default database.
    /// </summary>
    /// <param name="defaultDatabase">Database, for example 'web'</param>
    /// <returns>this</returns>
    IBaseSessionBuilder DefaultDatabase(string defaultDatabase);

    /// <summary>
    /// Specifies default language.
    /// </summary>
    /// <param name="defaultLanguage">Language, for example 'en'</param>
    /// <returns>this</returns>
    IBaseSessionBuilder DefaultLanguage(string defaultLanguage);

    /// <summary>
    /// Specifies path to media lybrary root.
    /// </summary>
    /// <param name="mediaLibraryRootItem">Media lybrary root, '/sitecore/media library' by default</param>
    /// <returns>this</returns>
    IBaseSessionBuilder MediaLibraryRoot(string mediaLibraryRootItem);

    /// <summary>
    /// Default extension for media resources.
    /// </summary>
    /// <param name="defaultExtension"Default extension, 'ashx' by default</param>
    /// <returns>this</returns>
    IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension);

    /// <summary>
    /// Hook to build request for resource download.
    /// </summary>
    /// <param name="mediaPrefix"Media library CMS hook, '~/media' by default</param>
    /// <returns>this</returns>
    IBaseSessionBuilder MediaPrefix(string mediaPrefix);
  }
}

