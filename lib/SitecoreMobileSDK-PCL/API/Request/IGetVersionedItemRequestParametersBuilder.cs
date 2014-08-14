namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Interface represents basic flow for creation of requets that reads versioned items.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface IGetVersionedItemRequestParametersBuilder<T> : IScopedRequestParametersBuilder<T>
    where T : class
  {
    IGetVersionedItemRequestParametersBuilder<T> Version(int? itemVersion);

    new IGetVersionedItemRequestParametersBuilder<T> Database(string sitecoreDatabase);
    new IGetVersionedItemRequestParametersBuilder<T> Language(string itemLanguage);
    new IGetVersionedItemRequestParametersBuilder<T> Payload(PayloadType payload);

    new IGetVersionedItemRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields);
    new IGetVersionedItemRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams);

    new IGetVersionedItemRequestParametersBuilder<T> AddScope(IEnumerable<ScopeType> scope);
    new IGetVersionedItemRequestParametersBuilder<T> AddScope(params ScopeType[] scope);
  }
}

