namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Interface represents basic flow for creation of requets that delete items.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface IDeleteItemRequestBuilder<T>
    where T : class
  {
    /// <summary>
    /// Specifies item database.
    /// For example: "web"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <param name="database">The Sitecore database.</param>
    /// <returns>
    /// this
    /// </returns>
    IDeleteItemRequestBuilder<T> Database(string database);

    /// <summary>
    /// Builds request with specified parameters.
    /// </summary>
    /// <returns>
    /// request
    /// </returns>
    T Build();
  }
}
