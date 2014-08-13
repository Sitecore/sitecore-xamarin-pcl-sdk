namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface ICreateItemRequestParametersBuilder<T> : IChangeItemRequestParametersBuilder<T>
  where T : class
  {
    new ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(IDictionary<string, string> fieldsRawValuesByName);
    new ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(string fieldKey, string fieldValue);

    new ICreateItemRequestParametersBuilder<T> Database(string sitecoreDatabase);
    new ICreateItemRequestParametersBuilder<T> Language(string itemLanguage);
    new ICreateItemRequestParametersBuilder<T> Payload(PayloadType payload);

    new ICreateItemRequestParametersBuilder<T> AddFields(IEnumerable<string> fields);
    new ICreateItemRequestParametersBuilder<T> AddFields(params string[] fieldParams);
  }
}

