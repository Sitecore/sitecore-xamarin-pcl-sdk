
namespace Sitecore.MobileSDK.API.Request
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;

  public interface IBaseChangeItemRequest : IBaseItemRequest
  {
    IDictionary<string, string> FieldsRawValuesByName { get; }
  }
}

