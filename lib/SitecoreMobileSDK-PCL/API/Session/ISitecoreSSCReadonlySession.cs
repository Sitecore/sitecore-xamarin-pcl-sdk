namespace Sitecore.MobileSDK.API.Session
{
  using System;

  /// <summary>
  /// Interface represents readonly actions that can be executed on items.
  /// </summary>
  public interface ISitecoreSSCReadonlySession :
    IDisposable,
    ISitecoreSSCSessionState,
    IConnectionActions,
    IMediaActions,
    IReadItemActions,
    ISearchActions
  {
  }
}

