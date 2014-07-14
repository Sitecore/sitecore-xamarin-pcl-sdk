
namespace Sitecore.MobileSDK.UserRequest
{
  using System;
  using System.Collections.Generic;

  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public interface IBaseItemRequestParametersBuilder<T>
    where T : class
  {
    IBaseItemRequestParametersBuilder<T> Database(string sitecoreDatabase);
    IBaseItemRequestParametersBuilder<T> Language(string itemLanguage);
    IBaseItemRequestParametersBuilder<T> Payload(PayloadType payload);

    IBaseItemRequestParametersBuilder<T> AddFields(ICollection<string> fields);
    IBaseItemRequestParametersBuilder<T> AddFields(params string[] fieldParams);

    IBaseItemRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope);
    IBaseItemRequestParametersBuilder<T> AddScope(params ScopeType[] scope);

    T Build();
  }
}

