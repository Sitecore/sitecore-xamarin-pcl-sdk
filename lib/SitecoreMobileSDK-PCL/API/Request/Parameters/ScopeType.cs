namespace Sitecore.MobileSDK.API.Request.Parameters
{
  /// <summary>
  /// Enum represents scope parameter that specifies the set of items that you are working with.
  /// </summary>
  public enum ScopeType
  {
    /// <summary>
    /// the requested item will be returned from the server.
    /// </summary>
    Self,

    /// <summary>
    /// children of the requested item will be returned from the server.
    /// </summary>
    Children,
    
    /// <summary>
    /// parent of the requested item will be returned from the server.
    /// </summary>
    Parent,

    /// <summary>
    /// The default is <seealso cref="Self"/>
    /// </summary>
    Default = Self
  }
}

