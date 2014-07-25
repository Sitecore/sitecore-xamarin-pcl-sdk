
namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;

  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;


  public interface IUpdateItemActions
  {
    Task<ScItemsResponse> UpdateItemAsync(IUpdateItemByIdRequest   request, CancellationToken cancelToken = default(CancellationToken));
    //Task<ScItemsResponse> CreateItemAsync(IUpdateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}

