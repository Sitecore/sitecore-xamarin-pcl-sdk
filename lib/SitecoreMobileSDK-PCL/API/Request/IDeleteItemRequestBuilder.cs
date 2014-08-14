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
    /// Specifies sitecore database.
    /// </summary>
    /// <param name="database">The sitecore database.</param>
    /// <returns>
    /// this
    /// </returns>
    IDeleteItemRequestBuilder<T> Database(string database);

    /// <summary>
    /// Adds the scopes.
    /// </summary>
    /// <param name="scope">The scopes.</param>
    /// <returns>
    /// this
    /// </returns>
    IDeleteItemRequestBuilder<T> AddScope(IEnumerable<ScopeType> scope);

    /// <summary>
    /// Adds the scope.
    /// </summary>
    /// <param name="scope">The scopes.</param>
    /// <returns>
    /// this
    /// </returns>
    IDeleteItemRequestBuilder<T> AddScope(params ScopeType[] scope);

    /// <summary>
    /// Builds request with specified parameters.
    /// </summary>
    /// <returns>
    /// request
    /// </returns>
    T Build();
  }
}
