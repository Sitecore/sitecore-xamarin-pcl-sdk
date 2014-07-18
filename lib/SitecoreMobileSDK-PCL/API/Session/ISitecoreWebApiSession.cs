

namespace Sitecore.MobileSDK.API.Session
{
    using Sitecore.MobileSDK.Session;

    public interface ISitecoreWebApiSession : 
    ISitecoreWebApiReadonlySession,
    ISitecoreWebApiSessionState   , 
    ISitecoreWebApiSessionActions
  {
  }
}

