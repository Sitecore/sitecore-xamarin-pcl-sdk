
namespace Sitecore.MobileSDK.UserRequest
{
  using System;
  using System.Collections.Generic;

  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public interface IGetItemRequestParametersBuilder<T>
    where T : class
  {
    IGetItemRequestParametersBuilder<T> Database(string sitecoreDatabase);
    IGetItemRequestParametersBuilder<T> Language(string itemLanguage);
    IGetItemRequestParametersBuilder<T> Payload(PayloadType payload);

    IGetItemRequestParametersBuilder<T> AddFields(ICollection<string> fields);
    IGetItemRequestParametersBuilder<T> AddFields(params string[] fieldParams);

    IGetItemRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope);
    IGetItemRequestParametersBuilder<T> AddScope(params ScopeType[] scope);

    T Build();
  }
}

