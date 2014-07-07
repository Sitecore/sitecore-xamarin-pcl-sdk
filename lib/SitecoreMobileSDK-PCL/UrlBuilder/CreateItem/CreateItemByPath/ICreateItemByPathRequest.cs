
namespace Sitecore.MobileSDK.UrlBuilder.CreateItemByPath
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;

  public interface ICreateItemByPathRequest : IReadItemsByPathRequest
  {
    string ItemName{ get; }
    string ItemTemplate{ get; }
    Dictionary<string, string> FieldsRawValuesByName{ get; }
  }
}

