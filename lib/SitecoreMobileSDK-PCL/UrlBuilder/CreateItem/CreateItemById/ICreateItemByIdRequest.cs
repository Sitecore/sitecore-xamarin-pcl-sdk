using System;

namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  public interface ICreateItemByIdRequest : IBaseCreateItemRequest
  {
    string ItemId { get; }
  }
}

