namespace Sitecore.MobileSDK.API.Session
{
  using System;

  /// <summary>
  /// Interface represents session to work with Sitecore Mobile SDK.
  /// </summary>
  public interface ISitecoreWebApiSession :
    IDisposable,
    ISitecoreWebApiSessionState,
    ISitecoreWebApiReadonlySession,
    ISitecoreWebApiSessionActions
  {
  }
}

