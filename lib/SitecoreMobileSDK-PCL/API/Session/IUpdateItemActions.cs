
namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;

  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;
  using Sitecore.MobileSDK.API.Request;


  public interface IUpdateItemActions
  {
    Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByIdRequest request, CancellationToken cancelToken = default(CancellationToken));
    Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}

