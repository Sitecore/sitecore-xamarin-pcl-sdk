namespace Sitecore.MobileSDK.API.Session
{
  using System;
  using Sitecore.MobileSDK.Session;

  public interface ISitecoreWebApiReadonlySession : 
    IDisposable,  
    ISitecoreWebApiSessionState, 
    IConnectionActions,
    IMediaActions,
    IReadItemActions
  {
  }
}

