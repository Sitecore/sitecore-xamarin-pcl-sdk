namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface IScopedRequestParametersBuilder<T> where T : class
  {
    IScopedRequestParametersBuilder<T> Database(string sitecoreDatabase);
    IScopedRequestParametersBuilder<T> Language(string itemLanguage);
    IScopedRequestParametersBuilder<T> Payload(PayloadType payload);

    IScopedRequestParametersBuilder<T> AddFields(IEnumerable<string> fields);
    IScopedRequestParametersBuilder<T> AddFields(params string[] fieldParams);

    IScopedRequestParametersBuilder<T> AddScope(IEnumerable<ScopeType> scope);
    IScopedRequestParametersBuilder<T> AddScope(params ScopeType[] scope);

    T Build();
  }
}
