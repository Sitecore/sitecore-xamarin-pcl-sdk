namespace Sitecore.MobileSDK.API.Request.Parameters
{
  public interface IRenderingHTMLRequestParametersBuilder<out T>
    where T : class
  {
  
    IGetMediaItemRequestParametersBuilder<T> Database(string database);

    IGetMediaItemRequestParametersBuilder<T> Language(string itemLanguage);

    IGetMediaItemRequestParametersBuilder<T> Version(int? itemVersion);

    new ICreateItemRequestParametersBuilder<T> AddFieldsRawValuesByNameToSet(string fieldName, string fieldValue);

    T Build();
  }
}

