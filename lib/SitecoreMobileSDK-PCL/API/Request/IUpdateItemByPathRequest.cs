
namespace Sitecore.MobileSDK.API.Request
{
  using System;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;

  public interface IUpdateItemByPathRequest : IReadItemsByPathRequest, IBaseUpdateItemRequest
  {
    IUpdateItemByPathRequest DeepCopyUpdateItemByPathRequest();
  }
}

