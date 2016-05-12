
namespace Sitecore.MobileSDK.API.Session
{
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;

  public interface ISearchActions
  {

    Task<ScItemsResponse> RunStoredQuerryAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken));

//    Task<ScItemsResponse> RunSitecoreSearchAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken));
//
//    Task<ScItemsResponse> RunStoredSitecoreSearchAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken));

  }
}

