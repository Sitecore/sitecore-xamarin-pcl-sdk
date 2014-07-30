
namespace Sitecore.MobileSDK.API.Session
{
  using Sitecore.MobileSDK.Session;

    public interface ISitecoreWebApiSessionActions : 
    IReadItemActions  , 
    ICreateItemActions, 
    IUpdateItemActions,
    IConnectionActions,
    IDeleteItemActions,
    IMediaActions   
  {
  }
}

