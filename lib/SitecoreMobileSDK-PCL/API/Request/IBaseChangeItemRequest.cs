namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;

  public interface IBaseChangeItemRequest : IBaseItemRequest
  {
    IDictionary<string, string> FieldsRawValuesByName { get; }
  }
}

