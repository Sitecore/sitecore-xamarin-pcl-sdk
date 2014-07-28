namespace Sitecore.MobileSDK.API.Session
{
  public interface IBaseSessionBuilder
  {
    ISitecoreWebApiSession BuildSession();
    ISitecoreWebApiReadonlySession BuildReadonlySession();

    IBaseSessionBuilder Site(string site);
    IBaseSessionBuilder WebApiVersion(string webApiVersion);
    IBaseSessionBuilder DefaultDatabase(string defaultDatabase);
    IBaseSessionBuilder DefaultLanguage(string defaultLanguage);
    IBaseSessionBuilder MediaLibraryRoot(string mediaLibraryRootItem);
    IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension);
    IBaseSessionBuilder MediaPrefix(string mediaPrefix);
  }
}

