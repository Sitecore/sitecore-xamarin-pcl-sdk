namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface IUpdateItemRequestParametersBuilder<T> : IChangeItemRequestParametersBuilder<T>
    where T : class
  {
    IUpdateItemRequestParametersBuilder<T> Version(int? itemVersion);
    new IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(IDictionary<string, string> fieldsRawValuesByName);
    new IUpdateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(string fieldKey, string fieldValue);

    new IUpdateItemRequestParametersBuilder<T> Database(string sitecoreDatabase);
    new IUpdateItemRequestParametersBuilder<T> Language(string itemLanguage);
    new IUpdateItemRequestParametersBuilder<T> Payload(PayloadType payload);

    new IUpdateItemRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields);
    new IUpdateItemRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams);
  }
}


