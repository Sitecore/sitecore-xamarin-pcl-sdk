namespace Sitecore.MobileSDK.API.Request.Parameters
{
  /// <summary>
  /// Enum represents scope parameter that specifies the set of items that you are working with.
  /// </summary>
  public enum ScopeType
  {
    /// <summary>
    /// self - only requested item will be returned from the server.
    /// </summary>
    Self,

    /// <summary>
    /// children - children of the requested item will be returned from the server.
    /// </summary>
    Children,
    
    /// <summary>
    /// parent - parent of the requested item will be returned from the server.
    /// </summary>
    Parent,

    /// <summary>
    /// The default is <see cref="Self"/>
    /// </summary>
    Default = Self
  }
}

