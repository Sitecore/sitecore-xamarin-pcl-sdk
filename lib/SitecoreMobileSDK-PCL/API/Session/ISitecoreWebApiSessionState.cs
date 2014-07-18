namespace Sitecore.MobileSDK.API.Session
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;

    public interface ISitecoreWebApiSessionState
  {
    IItemSource        DefaultSource {get;}
    ISessionConfig     Config        {get;}
    IWebApiCredentials Credentials   {get;}
  }
}

