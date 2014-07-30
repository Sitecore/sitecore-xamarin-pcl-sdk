namespace Sitecore.MobileSDK.API.Session
{
  using System;
  using Sitecore.MobileSDK.Session;

  public interface ISitecoreWebApiSession : 
    IDisposable,
    ISitecoreWebApiSessionState,
    ISitecoreWebApiReadonlySession, 
    ISitecoreWebApiSessionActions 
  {
  }
}

