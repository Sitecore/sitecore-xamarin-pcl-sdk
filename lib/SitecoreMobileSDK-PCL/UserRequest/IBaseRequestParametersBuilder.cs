namespace Sitecore.MobileSDK.UserRequest
{
  using System.Collections.Generic;

  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public interface IBaseRequestParametersBuilder<T>
    where T : class
  {
    IBaseRequestParametersBuilder<T> Database(string sitecoreDatabase);
    IBaseRequestParametersBuilder<T> Language(string itemLanguage);
    IBaseRequestParametersBuilder<T> Payload(PayloadType payload);

    IBaseRequestParametersBuilder<T> AddFields(ICollection<string> fields);
    IBaseRequestParametersBuilder<T> AddFields(params string[] fieldParams);

    IBaseRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope);
    IBaseRequestParametersBuilder<T> AddScope(params ScopeType[] scope);

    T Build();
  }
}

