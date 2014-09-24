namespace Sitecore.MobileSDK.API.Request.Parameters
{
  public interface IRenderingHTMLRequestParametersBuilder<out T>
    where T : class
  {
  
    IRenderingHTMLRequestParametersBuilder<T> SourceAndRenderingDatabase(string database);

    IRenderingHTMLRequestParametersBuilder<T> SourceAndRenderingLanguage(string itemLanguage);

    IRenderingHTMLRequestParametersBuilder<T> SourceVersion(int? itemVersion);

    IRenderingHTMLRequestParametersBuilder<T> AddAdditionalParameterNameValue(string parameterName, string parameterValue);

    T Build();
  }
}

