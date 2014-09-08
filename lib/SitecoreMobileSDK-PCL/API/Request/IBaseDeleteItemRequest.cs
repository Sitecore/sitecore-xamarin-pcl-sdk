namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Interface represents basic delete item request parameters.
  /// </summary>
  public interface IBaseDeleteItemRequest
  {
    /// <summary>
    /// Gets the session configuration.
    /// <seealso cref="ISessionConfig"/>
    /// </summary>
    /// <returns>
    /// The session configuration.
    /// </returns>>
    ISessionConfig SessionConfig { get; }

    /// <summary>
    /// Gets the scope parameters.
    /// <seealso cref="IScopeParameters"/>
    /// </summary>
    /// <returns>>
    /// The scope parameters.
    /// </returns>>
    IScopeParameters ScopeParameters { get; }

    /// <summary>
    /// Returns database name.
    /// For example: "web"
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <returns>
    /// The database name.
    /// </returns>
    string Database { get; }
  }
}