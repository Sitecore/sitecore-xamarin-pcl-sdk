
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;

  public interface ICreateItemByPathRequest : IBaseCreateItemRequest
  {
    string ItemPath { get; }
  }
}

