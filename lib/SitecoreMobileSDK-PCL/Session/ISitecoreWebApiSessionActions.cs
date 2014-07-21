
namespace Sitecore.MobileSDK.Session
{
  using Sitecore.MobileSDK.UrlBuilder.DeleteItem;

  public interface ISitecoreWebApiSessionActions : 
    IReadItemActions  , 
    ICreateItemActions, 
    IConnectionActions,
    IDeleteItemActions,
    IMediaActions   
  {
  }
}

