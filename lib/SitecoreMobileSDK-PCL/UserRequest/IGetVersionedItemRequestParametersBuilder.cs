
namespace Sitecore.MobileSDK.UserRequest
{
  using System;
  using System.Collections.Generic;

  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;



  public interface IGetVersionedItemRequestParametersBuilder<T> : IGetItemRequestParametersBuilder<T>
    where T : class
  {
    IGetVersionedItemRequestParametersBuilder<T> Version(string itemVersion);

    IGetVersionedItemRequestParametersBuilder<T> Database(string sitecoreDatabase);
    IGetVersionedItemRequestParametersBuilder<T> Language(string itemLanguage);
    IGetVersionedItemRequestParametersBuilder<T> Payload(PayloadType payload);

    IGetVersionedItemRequestParametersBuilder<T> AddFields(ICollection<string> fields);
    IGetVersionedItemRequestParametersBuilder<T> AddFields(params string[] fieldParams);

    IGetVersionedItemRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope);
    IGetVersionedItemRequestParametersBuilder<T> AddScope(params ScopeType[] scope);
  }
}

