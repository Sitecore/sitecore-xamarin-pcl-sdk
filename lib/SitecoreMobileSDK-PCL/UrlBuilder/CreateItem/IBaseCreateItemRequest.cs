
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using System.Collections.Generic;

  public interface IBaseCreateItemRequest : IBaseItemRequest
  {
    CreateItemParameters CreateParameters{ get; }
  }
}

