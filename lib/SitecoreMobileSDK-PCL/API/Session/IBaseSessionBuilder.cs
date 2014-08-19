namespace Sitecore.MobileSDK.API.Session
{
  /// <summary>
  /// Interface represents base session builder. 
  /// </summary>
  public interface IBaseSessionBuilder
  {
    /// <summary>
    /// Builds session.
    /// </summary>
    /// <returns>
    ///   <see cref="ISitecoreWebApiSession" />
    /// </returns>
    ISitecoreWebApiSession BuildSession();

    /// <summary>
    /// Builds readonly session.
    /// </summary>
    /// <returns>
    ///   <see cref="ISitecoreWebApiReadonlySession" />
    /// </returns>
    ISitecoreWebApiReadonlySession BuildReadonlySession();

    /// <summary>
    /// Specifies site parameter.
    /// For example:"/sitecore/shell"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="site">Site parameter</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder Site(string site);

    /// <summary>
    /// Specifies WebAPI version, "v1" by default.
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="webApiVersion">WebAPI version</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder WebApiVersion(string webApiVersion);

    /// <summary>
    /// Specifies item language.
    /// For example: "en"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="defaultDatabase">Database name value</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder DefaultDatabase(string defaultDatabase);

    /// <summary>
    /// Specifies item language.
    /// For example: "en"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="defaultLanguage">Language value</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder DefaultLanguage(string defaultLanguage);

    /// <summary>
    /// Specifies path to media lybrary root, '/sitecore/media library' by default.
    /// </summary>
    /// <param name="mediaLibraryRootItem">Media lybrary root.</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder MediaLibraryRoot(string mediaLibraryRootItem);

    /// <summary>
    /// Defaults the media resource extension, "ashx" by default.
    /// </summary>
    /// <param name="defaultExtension">The default extension for media requests.</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension);

    /// <summary>
    /// Prefix to build media requests, "~/media" by default.
    /// </summary>
    /// <param name="mediaPrefix">The media prefix.</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder MediaPrefix(string mediaPrefix);
  }
}

