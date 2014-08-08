namespace Sitecore.MobileSDK.API.Session
{
  using System;

  public interface ISitecoreWebApiSession :
    IDisposable,
    ISitecoreWebApiSessionState,
    ISitecoreWebApiReadonlySession,
    ISitecoreWebApiSessionActions
  {
  }
}

