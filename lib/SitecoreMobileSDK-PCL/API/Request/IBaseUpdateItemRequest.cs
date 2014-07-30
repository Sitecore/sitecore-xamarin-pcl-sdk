
namespace Sitecore.MobileSDK.UrlBuilder.UpdateItem
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;

  public interface IBaseUpdateItemRequest : IBaseItemRequest
  {

    //TODO: igk we need only 
    //public Dictionary<string, string> FieldsRawValuesByName{ get; private set; }
    //fix this!!!
    CreateItemParameters CreateParameters{ get; }
  }
}

