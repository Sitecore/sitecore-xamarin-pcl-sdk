namespace Sitecore.MobileSDK.API.Request.Parameters
{
  public interface IRenderingHtmlRequestParametersBuilder<out T>
    where T : class
  {
  
    IRenderingHtmlRequestParametersBuilder<T> SourceAndRenderingDatabase(string database);

    IRenderingHtmlRequestParametersBuilder<T> SourceAndRenderingLanguage(string itemLanguage);

    IRenderingHtmlRequestParametersBuilder<T> SourceVersion(int? itemVersion);

    IRenderingHtmlRequestParametersBuilder<T> AddAdditionalParameterNameValue(string parameterName, string parameterValue);

    T Build();
  }
}

