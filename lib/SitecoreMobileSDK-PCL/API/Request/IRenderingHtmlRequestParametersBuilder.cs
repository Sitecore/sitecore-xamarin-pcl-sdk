
namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System.Collections.Generic;

  /// <summary>
  /// Interface represents basic flow for creation of requests that read rendering html.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface IRenderingHtmlRequestParametersBuilder<out T>
    where T : class
  {
    /// <summary>
    /// Specifies database for both source and rendering items.
    /// For example: "web"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="sitecoreDatabase">The Sitecore database.</param>
    /// <returns>
    /// this
    /// </returns>
    IRenderingHtmlRequestParametersBuilder<T> SourceAndRenderingDatabase(string database);

    /// <summary>
    /// Specifies language for both source and rendering items.
    /// For example: "en"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="itemLanguage">The item language.</param>
    /// <returns>
    /// this
    /// </returns>
    IRenderingHtmlRequestParametersBuilder<T> SourceAndRenderingLanguage(string itemLanguage);

    /// <summary>
    /// Specifies version for source item only. It is a positive integer number.
    /// A null value stands for the "latest" version.
    /// 
    /// For example: 1
    /// </summary>
    /// <param name="itemVersion">The item version.</param>
    /// <returns>
    /// this
    /// </returns>
    IRenderingHtmlRequestParametersBuilder<T> SourceVersion(int? itemVersion);

    /// <summary>
    /// Adds custom parameter for rendering.
    /// key   - must contain parmeter name, case sensitive.
    /// value - must contain new parameter value, case sensitive.
    /// </summary>
    /// <param name="parameterName"> Parameter name</param>
    /// <param name="parameterValue"> Parameter value</param>
    /// <returns>this</returns>
    IRenderingHtmlRequestParametersBuilder<T> AddRenderingParameterNameValue(string parameterName, string parameterValue);

    /// <summary>
    /// Adds custom parameters list for rendering.
    /// key   - must contain parmeter name, case sensitive.
    /// value - must contain new parameter value, case sensitive.
    /// </summary>
    /// <param name="parametersValuesByName"> Parameter and parameter value pairs</param>
    /// <returns>this</returns>
    IRenderingHtmlRequestParametersBuilder<T> AddRenderingParameterNameValue(IDictionary<string, string> parametersValuesByName);

    /// <summary>
    /// Builds request with specified parameters.
    /// </summary>
    /// <returns>request</returns>
    T Build();
  }
}

