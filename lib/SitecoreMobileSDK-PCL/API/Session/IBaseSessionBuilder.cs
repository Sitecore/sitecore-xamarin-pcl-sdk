using System.Net.Http;

namespace Sitecore.MobileSDK.API.Session
{
  using Sitecore.MobileSDK.API.MediaItem;


  /// <summary>
  /// Interface represents base session builder. 
  /// </summary>
  public interface IBaseSessionBuilder
  {
    /// <summary>
    /// Builds session.
    /// </summary>
    /// <returns>
    ///   <seealso cref="ISitecoreWebApiSession" />
    /// </returns>
    ISitecoreWebApiSession BuildSession();

    /// <summary>
    /// Builds session which contain read only operations.
    /// </summary>
    /// <returns>
    ///   <seealso cref="ISitecoreWebApiReadonlySession" />
    /// </returns>
    ISitecoreWebApiReadonlySession BuildReadonlySession();

    /// <summary>
    /// Specifies site parameter.
    /// This parameter value will be used if appropriate parameter will be missed in request object.
    /// For example: "/sitecore/shell"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="site">Site parameter.</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder Site(string site);

    /// <summary>
    /// Specifies Item Web API version, 'v1' by default.
    /// This parameter value will be used if appropriate parameter will be missed in request object.
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="webApiVersion">Item Web API version</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder WebApiVersion(string webApiVersion);

    /// <summary>
    /// Specifies default database.
    /// This parameter value will be used if appropriate parameter will be missed in request object.
    /// For example: "web".
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="defaultDatabase">Database</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder DefaultDatabase(string defaultDatabase);

    /// <summary>
    /// Specifies default language.
    /// This parameter value will be used if appropriate parameter will be missed in request object.
    /// For example: "en".
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="defaultLanguage">Language</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder DefaultLanguage(string defaultLanguage);

    /// <summary>
    /// Specifies path to media lybrary root, "/sitecore/media library" by default.
    /// </summary>
    /// <param name="mediaLibraryRootItem">Media lybrary root</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder MediaLibraryRoot(string mediaLibraryRootItem);

    /// <summary>
    /// Specifies the media resource extension, "ashx" by default.
    /// </summary>
    /// <param name="defaultExtension">The default extension.</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension);

    /// <summary>
    /// Prefix to build request for resource download, "~/media" by default.
    /// </summary>
    /// <param name="mediaPrefix">The media prefix.</param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder MediaPrefix(string mediaPrefix);

    /// <summary>
    /// Due to security issue bugfix image resizing procedure has changed for Sitecore CMS 7.5 and newer versions.
    /// They require hashed URLs for resized media images. 
    /// 
    /// For this reason the SDK provides two primary image loading strategies 
    /// * Plain - for legacy versions of the CMS
    /// * Hashed - for CMS 7.5 and newer
    /// 
    /// </summary>
    /// <param name="resizingFlag">The media resizing strategy. <see cref="DownloadStrategy"/> 
    /// If omitted, the "Plain" strategy will be used.
    /// </param>
    /// <returns>
    /// this
    /// </returns>
    IBaseSessionBuilder MediaResizingStrategy(DownloadStrategy resizingFlag);

    /// <summary>
    /// Sets the HttpMessageHandler to be used for the underlying requests
    /// </summary>
    /// <param name="httpMessageHandler">Concrete HttpMessageHandler to be used. (null == default)</param>
    /// <returns>this</returns>
    IBaseSessionBuilder MessageHandler(HttpMessageHandler httpMessageHandler);
  }
}

