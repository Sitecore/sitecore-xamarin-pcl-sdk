namespace Sitecore.MobileSDK.API.Session
{
    using System.Threading;
    using System.Threading.Tasks;
    using Sitecore.MobileSDK.API.Items;
    using Sitecore.MobileSDK.API.Request;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.CreateItem;

    public interface ICreateItemActions
  {
    Task<ScItemsResponse> CreateItemAsync(ICreateItemByIdRequest   request, CancellationToken cancelToken = default(CancellationToken));
    Task<ScItemsResponse> CreateItemAsync(ICreateItemByPathRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}

