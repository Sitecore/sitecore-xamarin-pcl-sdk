namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;

  public interface IChangeItemRequestParametersBuilder<T> : IBaseRequestParametersBuilder<T> 
    where T : class
  {
    IChangeItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(IDictionary<string, string> fieldsRawValuesByName);
    IChangeItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(string fieldKey, string fieldValue);
  }
}
