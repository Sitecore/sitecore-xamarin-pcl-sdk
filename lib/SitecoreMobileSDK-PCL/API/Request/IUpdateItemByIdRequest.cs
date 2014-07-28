

namespace Sitecore.MobileSDK.UrlBuilder.UpdateItem
{
  using System;
  using Sitecore.MobileSDK.API.Request;

  public interface IUpdateItemByIdRequest : IReadItemsByIdRequest, IBaseUpdateItemRequest
  {
    IUpdateItemByIdRequest DeepCopyUpdateItemByIdRequest();
  }
}

