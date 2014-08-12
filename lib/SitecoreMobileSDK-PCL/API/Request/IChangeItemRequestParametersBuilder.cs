namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;

  public interface IChangeItemRequestParametersBuilder<T> : IBaseRequestParametersBuilder<T> 
    where T : class
  {
    IChangeItemRequestParametersBuilder<T> AddFieldsRawValuesByName(IDictionary<string, string> fieldsRawValuesByName);
    IChangeItemRequestParametersBuilder<T> AddFieldsRawValuesByName(string fieldKey, string fieldValue);
  }
}
