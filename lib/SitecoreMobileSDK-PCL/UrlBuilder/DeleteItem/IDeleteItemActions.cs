namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.Items;

  public interface IDeleteItemActions
  {
    Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken));
    Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken));
    Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}
