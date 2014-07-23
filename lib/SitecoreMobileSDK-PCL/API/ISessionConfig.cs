namespace Sitecore.MobileSDK.API
{
  public interface ISessionConfig 
    : IMediaLibrarySettings // TODO : remove inheritance
  {
    ISessionConfig SessionConfigShallowCopy();

    string InstanceUrl
    {
        get;
    }

    string Site      
    { 
        get; 
    }

    string ItemWebApiVersion
    {
        get;
    }
      
  }
}
