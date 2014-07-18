

namespace Sitecore.MobileSDK.API.Session
{
    using System.Threading;
    using System.Threading.Tasks;
    using Sitecore.MobileSDK.API.Items;
    using Sitecore.MobileSDK.API.Request;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;

    public interface IReadItemActions
  {
    Task<ScItemsResponse> ReadItemAsync(IReadItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken));
    Task<ScItemsResponse> ReadItemAsync(IReadItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken));
    Task<ScItemsResponse> ReadItemAsync(IReadItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}

