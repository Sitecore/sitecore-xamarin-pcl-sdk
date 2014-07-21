namespace Sitecore.MobileSDK.UrlBuilder.DeleteItem
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public interface IBaseDeleteRequestParametersBuilder<T>
    where T : class
  {
    IBaseDeleteRequestParametersBuilder<T> Database(string sitecoreDatabase);
    IBaseDeleteRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope);
    IBaseDeleteRequestParametersBuilder<T> AddScope(params ScopeType[] scope);

    T Build();
  }
}
