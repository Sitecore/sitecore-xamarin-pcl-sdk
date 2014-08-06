namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface IDeleteItemRequestBuilder<T>
    where T : class
  {
    IDeleteItemRequestBuilder<T> Database(string database);
    IDeleteItemRequestBuilder<T> AddScope(IEnumerable<ScopeType> scope);
    IDeleteItemRequestBuilder<T> AddScope(params ScopeType[] scope);

    T Build();
  }
}
