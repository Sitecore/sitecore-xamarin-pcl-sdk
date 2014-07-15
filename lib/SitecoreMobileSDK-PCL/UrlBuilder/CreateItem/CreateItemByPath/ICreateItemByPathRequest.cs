
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;

  public interface ICreateItemByPathRequest : IReadItemsByPathRequest, IBaseCreateItemRequest
  {
    ICreateItemByPathRequest DeepCopyCreateItemByPathRequest();
  }
}

