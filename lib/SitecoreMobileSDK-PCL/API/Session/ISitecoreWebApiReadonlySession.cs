namespace Sitecore.MobileSDK.API.Session
{
    using Sitecore.MobileSDK.Session;

    public interface ISitecoreWebApiReadonlySession : 
    ISitecoreWebApiSessionState, 
    IConnectionActions         ,
    IMediaActions              ,
    IReadItemActions
  {
  }
}

