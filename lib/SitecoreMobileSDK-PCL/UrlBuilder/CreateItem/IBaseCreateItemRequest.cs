
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using System.Collections.Generic;

  public interface IBaseCreateItemRequest : IBaseGetItemRequest
  {
    CreateItemParameters CreateParameters{ get; }
  }
}

