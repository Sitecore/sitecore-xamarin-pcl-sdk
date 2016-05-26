
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

    Task<ScItemsResponse> RunSitecoreSearchAsync(ISitecoreSearchRequest request, CancellationToken cancelToken = default(CancellationToken));

    Task<ScItemsResponse> RunStoredSearchAsync(ISitecoreStoredSearchRequest request, CancellationToken cancelToken = default(CancellationToken));

  }
}

