
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;
  using System.Collections.Generic;

  public interface IBaseCreateItemRequest : IBaseGetItemRequest
  {
    string ItemName{ get; }
    string ItemTemplate{ get; }
    Dictionary<string, string> FieldsRawValuesByName{ get; }
  }
}

