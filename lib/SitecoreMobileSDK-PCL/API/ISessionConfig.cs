namespace Sitecore.MobileSDK.API
{
  public interface ISessionConfig
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

  	string MediaLybraryRoot
  	{
  		get;
  	}

    string DefaultMediaResourceExtension
    {
      get;
    }
  }
}
