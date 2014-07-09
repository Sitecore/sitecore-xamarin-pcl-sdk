
namespace Sitecore.MobileSDK
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UserRequest;

  public interface ICreateItemRequestParametersBuilder<T> : IGetItemRequestParametersBuilder<T>
    where T : class
  {
    ICreateItemRequestParametersBuilder<T> ItemTemplate(string template);
    ICreateItemRequestParametersBuilder<T> ItemName(string template);
    ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByName (Dictionary<string, string> fieldsRawValuesByName);
    ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByName (string fieldKey, string fieldValue);
  }
}

