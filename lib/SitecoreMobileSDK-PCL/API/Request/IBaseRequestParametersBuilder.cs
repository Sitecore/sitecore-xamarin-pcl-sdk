namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public interface IBaseRequestParametersBuilder<T>
  where T : class
  {
    IBaseRequestParametersBuilder<T> Database(string sitecoreDatabase);
    IBaseRequestParametersBuilder<T> Language(string itemLanguage);
    IBaseRequestParametersBuilder<T> Payload(PayloadType payload);

    IBaseRequestParametersBuilder<T> AddFieldsToRead(IEnumerable<string> fields);
    IBaseRequestParametersBuilder<T> AddFieldsToRead(params string[] fieldParams);

    T Build();
  }
}

