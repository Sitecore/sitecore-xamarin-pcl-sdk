﻿namespace Sitecore.MobileSDK.API.Session
{
  using System.Threading;
  using System.Threading.Tasks;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;

  public interface IDeleteItemActions
  {
    Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByIdRequest request, CancellationToken cancelToken = default(CancellationToken));
    Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByPathRequest request, CancellationToken cancelToken = default(CancellationToken));
    Task<ScDeleteItemsResponse> DeleteItemAsync(IDeleteItemsByQueryRequest request, CancellationToken cancelToken = default(CancellationToken));
  }
}
