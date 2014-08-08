namespace Sitecore.MobileSDK.API.Session
{
  using System;

  public interface ISitecoreWebApiReadonlySession :
    IDisposable,
    ISitecoreWebApiSessionState,
    IConnectionActions,
    IMediaActions,
    IReadItemActions
  {
  }
}

