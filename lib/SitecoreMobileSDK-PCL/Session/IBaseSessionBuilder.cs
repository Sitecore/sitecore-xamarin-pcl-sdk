namespace Sitecore.MobileSDK.Session
{
  public interface IBaseSessionBuilder
  {
    ISitecoreWebApiSession Build();

    IBaseSessionBuilder WebApiVersion(string webApiVersion);
    IBaseSessionBuilder DefaultDatabase(string defaultDatabase);
    IBaseSessionBuilder DefauldLanguage(string defaultLanguage);
    IBaseSessionBuilder MediaLibraryRoot(string mediaLibraryRootItem);
    IBaseSessionBuilder DefaultMediaResourceExtension(string defaultExtension);
  }
}

