namespace Sitecore.MobileSDK.API.Session
{
  /// <summary>
  /// Interface represents CRUD actions that can be executed on items.
  /// </summary>
  public interface ISitecoreSSCSessionActions :
    IReadItemActions,
    ICreateItemActions,
    IUpdateItemActions,
    IConnectionActions,
    IDeleteItemActions,
    ISearchActions,
    IMediaActions
  {
  }
}

