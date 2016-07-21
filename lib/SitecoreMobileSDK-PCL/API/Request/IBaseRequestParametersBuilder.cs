namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Interface represents basic flow for creation of requets.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface IBaseRequestParametersBuilder<T>
  where T : class
  {
    /// <summary>
    /// Specifies item database.
    /// For example: "web"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <returns>
    /// this
    /// </returns>
    IBaseRequestParametersBuilder<T> Database(string sitecoreDatabase);

    /// <summary>
    /// Specifies item language.
    /// For example: "en"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <returns>
    /// this
    /// </returns>
    IBaseRequestParametersBuilder<T> Language(string itemLanguage);

    IBaseRequestParametersBuilder<T> IcludeStanderdTemplateFields(bool include);

    /// <summary>
    /// Builds request with specified parameters.
    /// </summary>
    /// <returns>request</returns>
    T Build();
  }
}

