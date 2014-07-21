namespace Sitecore.MobileSDK.UserRequest.DeleteRequest
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public interface IDeleteItemRequestBuilder<T>
    where T : class
  {
    IDeleteItemRequestBuilder<T> Database(string sitecoreDatabase);
    IDeleteItemRequestBuilder<T> AddScope(ICollection<ScopeType> scope);
    IDeleteItemRequestBuilder<T> AddScope(params ScopeType[] scope);

    T Build();
  }
}
