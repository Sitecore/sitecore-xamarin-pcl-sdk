namespace Sitecore.MobileSDK.Session
{
  public interface IBaseSessionBuilder
  {
    ISitecoreWebApiSession BuildSession();
    ISitecoreWebApiReadonlySession BuildReadonlySession();

    IBaseSessionBuilder WebApiVersion(string webApiVersion);
    IBaseSessionBuilder DefaultDatabase(string defaultDatabase);
    IBaseSessionBuilder DefauldLanguage(string defaultLanguage);
    IBaseSessionBuilder MediaLibraryRoot(string mediaLibraryRootItem);
    IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension);
  }
}

