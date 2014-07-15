namespace Sitecore.MobileSDK.Session
{
  public interface ISitecoreWebApiReadonlySession : 
    ISitecoreWebApiSessionState, 
    IConnectionActions         ,
    IMediaActions              ,
    IReadItemActions
  {
  }
}

