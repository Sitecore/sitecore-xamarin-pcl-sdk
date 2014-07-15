
namespace Sitecore.MobileSDK.UrlBuilder.UpdateItem
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;

  public interface IUpdateItemByIdRequest : IReadItemsByIdRequest, IBaseUpdateItemRequest
  {
    IUpdateItemByIdRequest DeepCopyUpdateItemByIdRequest();
  }
}

