
namespace Sitecore.MobileSDK.API.Request
{
    using System.Collections.Generic;
    using Sitecore.MobileSDK.API.Request.Parameters;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    public interface IGetVersionedItemRequestParametersBuilder<T> : IBaseRequestParametersBuilder<T>
    where T : class
  {
    IGetVersionedItemRequestParametersBuilder<T> Version(string itemVersion);

    new IGetVersionedItemRequestParametersBuilder<T> Database(string sitecoreDatabase);
    new IGetVersionedItemRequestParametersBuilder<T> Language(string itemLanguage);
    new IGetVersionedItemRequestParametersBuilder<T> Payload(PayloadType payload);

    new IGetVersionedItemRequestParametersBuilder<T> AddFields(ICollection<string> fields);
    new IGetVersionedItemRequestParametersBuilder<T> AddFields(params string[] fieldParams);

    new IGetVersionedItemRequestParametersBuilder<T> AddScope(ICollection<ScopeType> scope);
    new IGetVersionedItemRequestParametersBuilder<T> AddScope(params ScopeType[] scope);
  }
}

