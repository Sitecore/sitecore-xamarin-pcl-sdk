namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface IBaseRequestParametersBuilder<T>
  where T : class
  {
    IBaseRequestParametersBuilder<T> Database(string sitecoreDatabase);
    IBaseRequestParametersBuilder<T> Language(string itemLanguage);
    IBaseRequestParametersBuilder<T> Payload(PayloadType payload);

    IBaseRequestParametersBuilder<T> AddFields(IEnumerable<string> fields);
    IBaseRequestParametersBuilder<T> AddFields(params string[] fieldParams);

    IBaseRequestParametersBuilder<T> AddScope(IEnumerable<ScopeType> scope);
    IBaseRequestParametersBuilder<T> AddScope(params ScopeType[] scope);

    T Build();
  }
}

